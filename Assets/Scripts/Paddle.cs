using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        _speed = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        // _inputVeritcal = Input.GetAxisRaw("Mouse Y");
        _inputVeritcal = Input.mouseScrollDelta.y;

        if (_inputVeritcal != 0) {
            Debug.Log($"scroll delta = {_inputVeritcal}");
            _rb.AddForce(new Vector2(0f, _inputVeritcal * _speed));
        }
    }
}
