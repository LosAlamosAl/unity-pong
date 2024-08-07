using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour {
    private Rigidbody2D _rb;
    private readonly float _speed = 200.0f;
    private int _framesSince;
    private bool _doRaycast;
    [SerializeField] GameObject _boxPrefab;
    [SerializeField] GameObject _ballerPrefab;
    List<GameObject> _boxes;
    List<GameObject> _ballers;
    List<ContactPoint2D> _contacts;

    public void ResetRaycast() {
        _framesSince = 0;
        _doRaycast = true;
    }

    public void ResetGeom() {
        foreach (var box in _boxes) {
            GameObject.Destroy(box);
        }
        foreach (var baller in _ballers) {
            GameObject.Destroy(baller);
        }
        _boxes.RemoveAll(box => true);
        _boxes.RemoveAll(baller => true);
    }

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _boxes = new List<GameObject>();
        _ballers = new List<GameObject>();
        _contacts = new List<ContactPoint2D>();
    }

    void Start() {
        ResetRaycast();
        AddStartingForce();
    }

    void FixedUpdate() {
        if (_framesSince > 0 && _doRaycast) {
            DrawProjectedPath();
            _doRaycast = false;
        }
        _framesSince++;
    }

    private void AddStartingForce() {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range( 0.5f,  1.0f);

        _rb.AddForce(new Vector2(x, y).normalized * _speed);
    }

    // Draws the projected path of the ball until its projection
    // hits the paddle or the left wall. A little bit of work
    // needs to be done to ensure that the path follows the
    // ball's center.
    private void DrawProjectedPath() {
        var dir = _rb.velocity.normalized;  // initial direction
        // Don't have to fudge the Ball's initial position because it's assigned to
        // the "Ignore Raycast" layer.
        var origin = _rb.position;
        RaycastHit2D hit;
        do {
            hit = Physics2D.Raycast(origin, dir);  // hit point of ray/collider
            if (hit.collider == null) break;       // this should never happen ;-)
            //PrintHitInfo(hit);
            // Until we switch to actual line geometey this line is drawn
            // to the hit point (with the walls or paddle) and not to the
            // projectd center of thge ball (which would be located a bit
            // above the surface of the collider).
            Debug.DrawLine(origin, hit.point, Color.yellow, 8);
            origin = NextOrigin(hit, dir);
            GameObject box = Instantiate(_boxPrefab, origin, Quaternion.identity);
            _boxes.Add(box);
            dir = Vector2.Reflect(dir, hit.normal);
        } while (hit.collider.name != "LeftWall" && hit.collider.name != "PaddleLeft");
    }

    private void PrintHitInfo(RaycastHit2D hit) {
        var debug = "HIT!\n";
        debug += $"{hit.collider.name}\n";
        debug += $"{hit.point}\n";
        debug += $"{hit.normal}\n";
        print(debug);
    }

    //  Find the origin of the next ray in the sequence along the
    //  ball's projected path. The parameter, 'hit', is the point
    //  of the previous ray's intersection while 'dir' is the
    //  direction of the previous ray.
    private Vector2 NextOrigin(RaycastHit2D hit, Vector2 dir) {
        var axis = hit.collider.name switch {
            "LeftWall" or "RightWall" or "PaddleLeft" => new Vector2(0, Mathf.Sign(dir.y)),
            "TopWall" or "BottomWall" => new Vector2(Mathf.Sign(dir.x), 0),
            _ => throw new System.ArgumentException($"WRONG collider: {hit.collider.name}"),
        };
        var theta = Vector2.Angle(axis, dir);
        var halfSize = GetComponent<BoxCollider2D>().bounds.size.y / 2.0f;
        var dist = halfSize / Mathf.Sin(theta * Mathf.Deg2Rad);
        var nextOrigin = hit.point + (dir * -1).normalized * dist;
        //print($"axis: {axis}");
        //print($"theta: {theta}");
        //print($"halfSize: {halfSize}");
        //print($"dist: {dist}");
        //print($"origin: {hit.point}");
        //print($"nextOrigin: {nextOrigin}");
        return nextOrigin;
    }

    private void OnCollisionEnter2D(Collision2D col) {
        GameObject baller = Instantiate(_ballerPrefab, gameObject.transform.position, Quaternion.identity);
        _ballers.Add(baller);
        // PrintCollisionInfo(col, i);
        if (col.collider.name == "LeftWall" || col.collider.name == "PaddleLeft") {
            ResetRaycast();
            ResetGeom();
        }
    }

    private void PrintCollisionInfo(Collision2D col, int i) {
            var debug = $"collision #{i}---------\n";
            debug += $"collider.name: {col.collider.name}\n";
            debug += $"this.name: {gameObject.name}\n";
            debug += $"world contact: {col.GetContact(i).point}\n";
            var _trans = gameObject.transform;
            debug += $"local contact:{_trans.InverseTransformPoint(col.GetContact(i).point)}\n";
            debug += $"local normal: {col.GetContact(i).normal}\n";
            Debug.Log(debug);
    }
}
