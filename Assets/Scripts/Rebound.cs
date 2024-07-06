using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D col) {
        var debug = "";
        debug += $"this.name: {gameObject.name}\n";
        debug += $"col.name: {col.gameObject.name}\n";
        debug += $"num contacts: {col.contactCount}\n";
        debug += $"world contact 0: {col.GetContact(0).point}\n";
        var _trans = col.gameObject.transform;
        debug += $"local contact 0:{_trans.InverseTransformPoint(col.GetContact(0).point)}\n";
        debug += $"local normal 0: {col.GetContact(0).normal}\n";
        Debug.Log(debug);
    }
}
