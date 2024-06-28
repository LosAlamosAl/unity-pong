using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _inputVeritcal;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _speed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _inputVeritcal = Input.GetAxisRaw("Vertical");

        _rb.AddForce(new Vector2(0f, _inputVeritcal * _speed));
    }
}
