using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTriggerLeft : MonoBehaviour {

	public GameObject currentMap;
	public GameObject previousMap;
	public GameObject player;

    public UnityEvent onLoadEvents;
    //public int xTransPostTrigger = 16;

	void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            //player.transform.Translate (xTransPostTrigger, 0, 0);

            //hardcoded translation value because bad programming foresight
            player.transform.Translate(16, 0, 0);


            previousMap.SetActive(true);
            currentMap.SetActive(false);

            onLoadEvents.Invoke();
            Debug.Log("you have traveled left, with trigger of " + gameObject.transform.parent.gameObject.name);
        }
	}
}
