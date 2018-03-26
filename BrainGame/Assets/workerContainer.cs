using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workerContainer : MonoBehaviour {
    public GameObject worker;
    public int maxWorkers = -1;
    public int maxRows = -1;

    private List<GameObject> workersList;

	// Use this for initialization
	void Start () {
        //might have to delete this manually to prevent leaks?
        workersList = new List<GameObject>();

        //setup our default valules
        if (maxWorkers == -1)
            maxWorkers = 6;
        if (maxRows == -1)
            maxRows = 1;

        addWorker();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void addWorker() {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        Vector3 objectPosition = rt.position;
        Vector2 objectSize = rt.rect.size;

        // TODO: add some fancy math that calculates optimal spacing for the specified amounts of workers

        //for now I'll just try this

        GameObject workerObject =  Instantiate(worker);
        workerObject.transform.position = objectPosition;
        workersList.Add(workerObject);
    }
}
