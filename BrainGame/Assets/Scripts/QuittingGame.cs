using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuittingGame: MonoBehaviour {

	public KeyCode triggerkey;

	void Start () {
		if (Input.GetKeyDown(triggerkey))
			Application.Quit();
	}

	void Update () {
		if (Input.GetKey(triggerkey))
			Application.Quit();
	}
}
