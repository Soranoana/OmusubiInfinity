using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UserControl : MonoBehaviour {

	public GameObject omusubiObject;
	public GameObject omusubiStartPosition;
	private FieldInfo fieldInfo;

	public GameObject text1;
	public GameObject text2;

	private int flame = -1;
	private float?[] range = new float?[60];

	void Start() {
		fieldInfo = GetComponent<FieldInfo>();
		if (fieldInfo == null) {
			Debug.LogError("not set \"FieldInfo\" Object.");
		}
	}

	void Update() {
		if (Input.GetMouseButton(0)) {

			float omusubiX = omusubiObject.transform.position.x;
			float omusubiStartX = omusubiStartPosition.transform.position.x;
			float courseWidth = fieldInfo.courseWidth;
			float MouseX = Input.GetAxis("Mouse X");

			if (omusubiX + MouseX >= omusubiStartX + courseWidth) {
				omusubiObject.transform.position = new Vector3(omusubiStartX + courseWidth, omusubiObject.transform.position.y, omusubiObject.transform.position.z);
			} else if (omusubiX + MouseX <= omusubiStartX - courseWidth) {
				omusubiObject.transform.position = new Vector3(omusubiStartX - courseWidth, omusubiObject.transform.position.y, omusubiObject.transform.position.z);
			} else {
				omusubiObject.transform.position += new Vector3(MouseX, 0, 0);
			}
		}
		SpeedManege();

	}

	void SpeedManege() {
		SpeedTest1();
		SpeedTest2();
	}

	void SpeedTest1() {
		//ベクトル量から算出
		Vector3 velocityLocal = new Vector3(0, omusubiObject.GetComponent<Rigidbody>().velocity.y, omusubiObject.GetComponent<Rigidbody>().velocity.z);
		string text = "speedTest1: " + Vector3.Distance(velocityLocal, Vector3.zero);
		// Debug.Log(text);
		text1.GetComponent<Text>().text = text;
	}

	void SpeedTest2() {
		//移動距離から算出
		Vector3 omusubiPositionLocal = new Vector3(0, omusubiObject.transform.position.y, omusubiObject.transform.position.z);
		string text;
		flame++;
		float rangeLocal = Vector3.Distance(omusubiStartPosition.transform.position, omusubiPositionLocal);
		if (range[flame % 60] == null) {
			text = "speedTest2: " + ( rangeLocal / flame * 10000 );
			// Debug.Log(text);
			text2.GetComponent<Text>().text = text;
		} else {
			text = "speedTest2: " + ( ( rangeLocal - range[flame % 60] ) / 60 * 10000 );
			// Debug.Log(text);
			text2.GetComponent<Text>().text = text;
		}

		range[flame % 60] = rangeLocal;
	}

}
