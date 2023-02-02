using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public Transform target;
    void Update() {
        float targetX = target.transform.position.x;
        float targetY = target.transform.position.y;
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
