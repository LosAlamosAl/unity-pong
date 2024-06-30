using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _speed = 200.0f;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AddStartingForce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddStartingForce() {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range( 0.5f,  1.0f);

        _rb.AddForce(new Vector2(x, y) * _speed);
    }
}
