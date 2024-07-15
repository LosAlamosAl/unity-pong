using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Rebound : MonoBehaviour {

    private Ball _ball;

    private void Awake() {
        _ball = gameObject.GetComponent<Ball>();
    }

    private void OnCollisionEnter2D(Collision2D col) {
        for (int i=0; i<col.contactCount; i++) {
            // PrintCollisionInfo(col, i);
            if (col.collider.name == "LeftWall" || col.collider.name == "PaddleLeft") {
                _ball.ResetRaycast();
                _ball.ResetGeom();
            }
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
