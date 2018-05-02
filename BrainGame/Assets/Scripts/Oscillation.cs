using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation: MonoBehaviour {

	private void Start(){

		float ypos = gameObject.transform.position.y;
		float xpos = gameObject.transform.position.x;

		pos1 = new Vector2 (-Oscillationlimit + xpos, ypos);
		pos2 = new Vector2 (Oscillationlimit + xpos, ypos);

	}

	public float Oscillationlimit;
	private Vector2 pos1;
	private Vector2 pos2;
	public float speed = 1.0f;

	void Update() {
		transform.position = Vector2.Lerp (pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
	}
}
