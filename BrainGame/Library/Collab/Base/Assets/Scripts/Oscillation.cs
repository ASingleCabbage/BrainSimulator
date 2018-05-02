using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation: MonoBehaviour {

	private Vector2 pos1 = new Vector2(-2, GameObject.transform.localPosition);
	private Vector2 pos2 = new Vector2 (2, GameObject.transform.localPosition);
	public float speed = 1.0f;

	void Update() {
		transform.position = Vector2.Lerp (pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}
}
