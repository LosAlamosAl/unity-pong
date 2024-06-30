using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PaddleLeft : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _inputVeritcal;
    [SerializeField] private float _speed;

    private void Awake() {
       _rb = gameObject.GetComponent<Rigidbody2D>();
        _speed = 6.0f;
    }

    private void Start()
    {
    }

    private void Update() {
        // Maybe use a switch instead?
        // https://stackoverflow.com/a/57127246
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            _inputVeritcal = Vector2.up;
        } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            _inputVeritcal = Vector2.down;
        } else {
            _inputVeritcal = Vector2.zero;
        }
    }

    private void FixedUpdate() {
        if (_inputVeritcal.sqrMagnitude != 0) {
            _rb.AddForce(_inputVeritcal * _speed);
        }
    }
}
