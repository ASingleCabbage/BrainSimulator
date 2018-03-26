using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workerContainer : MonoBehaviour {
    public GameObject worker;


	// Use this for initialization
	void Start () {
        addWorker();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void addWorker() {
        //getting the dimensions of the gameObject
        //thought it might be useful later
        Vector3 objectPosition = transform.position;
        Vector3 size = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        Debug.Log("gameObj size " + size);

        Instantiate(worker).transform.position = objectPosition;
    }
}
