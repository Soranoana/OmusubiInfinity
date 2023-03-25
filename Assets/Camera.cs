using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public GameObject omusubiObject;
	public GameObject cameraObject;

	private Vector3 omusubiStartPosition;
	private Vector3 cameraStartPosition;

	void Start() {
		if (omusubiObject == null) {
			Debug.LogError("Omusubi Object is null.");
		} else if (cameraObject == null) {
			Debug.LogError("Camera Object is null.");
		}
		omusubiStartPosition = omusubiObject.transform.position;
		cameraStartPosition = cameraObject.transform.position;
	}

	void Update() {
		cameraObject.transform.position = new Vector3(
			cameraStartPosition.x,
			cameraStartPosition.y + ( omusubiObject.transform.position.y - omusubiStartPosition.y ),
			cameraStartPosition.z + ( omusubiObject.transform.position.z - omusubiStartPosition.z )
		);
	}
}
