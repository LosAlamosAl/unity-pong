using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour {
    private Rigidbody2D _rb;
    private readonly float _speed = 200.0f;
    private int _framesSince;
    private Vector3 _lastPos;
    private bool _doRaycast = true;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        _framesSince = 0;
        AddStartingForce();
        _lastPos = transform.position;
    }

    void FixedUpdate() {
        if (_framesSince != 0 && _doRaycast) {
            DrawProjectedPath();
            _doRaycast = false;
        }
        _framesSince++;
        _lastPos = transform.position;
    }

    private void AddStartingForce() {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range( 0.5f,  1.0f);

        _rb.AddForce(new Vector2(x, y) * _speed);
    }

    private void DrawProjectedPath() {
        var dir = (transform.position - _lastPos).normalized;  // initial direction
        var origin = _lastPos + dir;  // initial ray origin (fudged outside ball)
        RaycastHit2D hit;
        do {
            hit = Physics2D.Raycast(origin, dir);
            if (hit.collider == null) break; 
            PrintHitInfo(hit);
            Debug.DrawLine(origin, hit.point, Color.yellow, 5);
            dir = Vector2.Reflect(dir, hit.normal);
            origin = hit.point + ((Vector2)dir * .01f);  // fudge (fix this)
        } while (hit.collider.name != "LeftWall");
    }

    private void PrintHitInfo(RaycastHit2D hit) {
        var debug = "HIT!\n";
        debug += $"{hit.collider.name}\n";
        debug += $"{hit.point}\n";
        debug += $"{hit.normal}\n";
        print(debug);
    }
}
