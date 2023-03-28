// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

/*
 * 加速、減速、拡大、縮小など、おむすびが侵入した際にどうするかを返す。
 * ただし、ゲーム中一度だけ効果を発揮する
 */

public class EffectObject : MonoBehaviour {

	private bool isNotUsed = true;
	private enum Effect {
		Acceleration,   //加速
		Deceleration,   //減速
		Hypertrophy,    //肥大
		Shrink, //縮小
	};
	[SerializeField] private Effect objectEffect;
	[SerializeField] private float effectValue;
	private void Awake() {
		//TODO オブジェクト名から自動でエフェクトを設定する（ログを流す）。オブジェクト名が想定外の時だけエラーを吐く。
		if (!this.gameObject.name.StartsWith(objectEffect.ToString())) {
			Debug.LogError("Gameobject名とオブジェクトの効果が一致しません。");
		}
	}

	void Start() {
	}

	void Update() {
	}

	private void OnTriggerEnter(Collider other) {
		if (isNotUsed) {
			GameObject.Find("system").GetComponent<UserControl>().OnTriggerEnterReturn(objectEffect.ToString(), effectValue);
			isNotUsed = false;
		}
	}
}
