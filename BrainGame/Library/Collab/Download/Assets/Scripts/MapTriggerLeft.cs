﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerLeft : MonoBehaviour {

	public GameObject currentMap;
	public GameObject previousMap;
	public GameObject player;
    public int xTransPostTrigger = 16;

	void OnTriggerStay2D(Collider2D other) {

		player.transform.Translate (xTransPostTrigger, 0, 0);
		previousMap.SetActive (true);
		currentMap.SetActive (false);
		Debug.Log("you have traveled left");

	}
}
