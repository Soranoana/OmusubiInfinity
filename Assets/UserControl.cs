// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserControl : MonoBehaviour {
	[SerializeField] private GameObject omusubiObject;
	[SerializeField] private GameObject omusubiStartPosition;
	private FieldInfo fieldInfo;
	[SerializeField] private Text text1;
	[SerializeField] private Text text2;

	private int frame = -1;
	private float?[] range = new float?[60];

	void Start() {
		fieldInfo = GetComponent<FieldInfo>();
		if (fieldInfo == null) {
			Debug.LogError("not set \"FieldInfo\" Object.");
		}
	}

	void Update() {
		if (Input.GetMouseButton(0)) {
			MoveOmusubi();
		}
		SpeedManage();
	}

	private void MoveOmusubi() {
		if (Input.GetMouseButton(0)) {
			float omusubiX = omusubiObject.transform.position.x;
			float omusubiStartX = omusubiStartPosition.transform.position.x;
			float courseWidth = fieldInfo.courseWidth;
			float mouseX = Input.GetAxis("Mouse X");
			if (omusubiX + mouseX >= omusubiStartX + courseWidth) {
				omusubiObject.transform.position = new Vector3(omusubiStartX + courseWidth, omusubiObject.transform.position.y, omusubiObject.transform.position.z);
			} else if (omusubiX + mouseX <= omusubiStartX - courseWidth) {
				omusubiObject.transform.position = new Vector3(omusubiStartX - courseWidth, omusubiObject.transform.position.y, omusubiObject.transform.position.z);
			} else {
				omusubiObject.transform.position += new Vector3(mouseX, 0, 0);
			}
		}
	}

	private void SpeedManage() {
		SpeedTest1();
		SpeedTest2();
	}

	//ベクトル量から算出
	private void SpeedTest1() {
		Vector3 velocityLocal = omusubiObject.GetComponent<Rigidbody>().velocity;
		velocityLocal.x = 0f;
		float speed = velocityLocal.magnitude;
		string text = "speedTest1: " + speed;
		text1.text = text;
	}

	//移動距離から算出
	private void SpeedTest2() {
		frame++;
		Vector3 omusubiPositionLocal = omusubiObject.transform.position;
		omusubiPositionLocal.x = 0f;
		float rangeLocal = Vector3.Distance(omusubiStartPosition.transform.position, omusubiPositionLocal);
		if (range[frame % 60] == null) {
			float speed = rangeLocal / frame * 10000;
			string text = "speedTest2: " + speed;
			text2.text = text;
		} else {
			float speed = ( rangeLocal - range[frame % 60].Value ) / 60f * 10000f;
			string text = "speedTest2: " + speed;
			text2.text = text;
		}
		range[frame % 60] = rangeLocal;
	}
}
