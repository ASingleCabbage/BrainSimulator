using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTriggerRight : MonoBehaviour {

	public GameObject currentMap;
	public GameObject nextMap;
	public GameObject player;
    public int xTransPostTrigger = -16;

    void OnTriggerEnter2D(Collider2D other) {
		
		player.transform.Translate(xTransPostTrigger, 0, 0);
		nextMap.SetActive (true);
		currentMap.SetActive (false);
		Debug.Log("you have traveled right");

	}
}
