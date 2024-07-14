using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour {
    private Rigidbody2D _rb;
    private readonly float _speed = 200.0f;
    private int _framesSince;
    private bool _doRaycast = true;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        _framesSince = 0;
        AddStartingForce();
    }

    void FixedUpdate() {
        if (_framesSince != 0 && _doRaycast) {
            DrawProjectedPath();
            _doRaycast = false;
        }
        _framesSince++;
    }

    private void AddStartingForce() {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range( 0.5f,  1.0f);

        _rb.AddForce(new Vector2(x, y) * _speed);
    }

    private void DrawProjectedPath() {
        var dir = _rb.velocity.normalized;  // initial direction
        // Don't have to fudge the Ball's initial position because it's assigned to
        // the "Ignore Raycast" layer.
        var origin = _rb.position;
        RaycastHit2D hit;
        do {
            hit = Physics2D.Raycast(origin, dir);  // hit point of ray/collider
            if (hit.collider == null) break; 
            PrintHitInfo(hit);
            Debug.DrawLine(origin, hit.point, Color.yellow, 5);
            origin = NextOrigin(hit, dir);
            dir = Vector2.Reflect(dir, hit.normal);
        } while (hit.collider.name != "LeftWall");
    }

    private void PrintHitInfo(RaycastHit2D hit) {
        var debug = "HIT!\n";
        debug += $"{hit.collider.name}\n";
        debug += $"{hit.point}\n";
        debug += $"{hit.normal}\n";
        print(debug);
    }

    private Vector2 NextOrigin(RaycastHit2D hit, Vector2 dir) {
        var axis = hit.collider.name switch {
            "LeftWall" or "RightWall" or "Paddle" => new Vector2(0, Mathf.Sign(dir.y)),
            "TopWall" or "BottomWall" => new Vector2(Mathf.Sign(dir.x), 0),
            _ => throw new System.ArgumentException("Impossible collider name!"),
        };
        var theta = Vector2.Angle(axis, dir);
        var halfSize = GetComponent<BoxCollider2D>().bounds.size.y / 2.0f;
        var dist = halfSize / Mathf.Sin(theta * Mathf.Deg2Rad);
        var nextOrigin = hit.point + (dir * -1).normalized * dist;
        print($"axis: {axis}");
        print($"theta: {theta}");
        print($"halfSize: {halfSize}");
        print($"dist: {dist}");
        print($"origin: {hit.point}");
        print($"nextOrigin: {nextOrigin}");
        return nextOrigin;
    }
}
