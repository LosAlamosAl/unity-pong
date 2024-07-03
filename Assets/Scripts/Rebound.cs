using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D col) {
        Debug.Log($"num contacts: {col.contactCount}");
        Debug.Log($"contact 0: {JsonUtility.ToJson(col.GetContact(0).point, true)}");
    }
}
