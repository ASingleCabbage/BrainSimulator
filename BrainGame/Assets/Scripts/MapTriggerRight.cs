﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTriggerRight : MonoBehaviour {

	public GameObject currentMap;
	public GameObject nextMap;
	public GameObject player;

    public UnityEvent onLoadEvents;
    //public int xTransPostTrigger = -16;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            //hardcoded translation value because bad programming foresight
            player.transform.Translate(-16, 0, 0);

            nextMap.SetActive(true);
            currentMap.SetActive(false);

            onLoadEvents.Invoke();
            Debug.Log("you have traveled right");
        }
	}
}
