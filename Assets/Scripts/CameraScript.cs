using UnityEngine;

public class CameraScript : MonoBehaviour {
	public Transform target;

	private void Update() {
		var targetX = target.transform.position.x;
		var targetY = target.transform.position.y;
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}