using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D col) {
        for (int i=0; i<col.contactCount; i++) {
            var debug = $"collision #{i}---------\n";
            debug += $"collider.name: {col.collider.name}\n";
            debug += $"col.name: {col.gameObject.name}\n";
            debug += $"world contact: {col.GetContact(i).point}\n";
            var _trans = gameObject.transform;
            debug += $"local contact:{_trans.InverseTransformPoint(col.GetContact(i).point)}\n";
            debug += $"local normal: {col.GetContact(i).normal}\n";
            Debug.Log(debug);
            Debug.DrawRay(col.contacts[i].point, col.contacts[i].normal, Color.green, 1, false);
            Debug.DrawRay(col.contacts[i].point, col.transform.position - transform.position * 10, Color.red, 1, false);
        }
    }
}
