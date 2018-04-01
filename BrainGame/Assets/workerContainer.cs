using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerContainer : MonoBehaviour {
    public GameObject worker;
    public float workerMargin;
    public int maxRow;
    public int maxWorkerCount;

    private List<GameObject> workersList;

    public Button addButton;
    public Button remButton;

	// Use this for initialization
	void Start () {
        //might have to delete this manually to prevent leaks?
        workersList = new List<GameObject>();
    }


    /*
     * Add and remove worker functions return true if operation successful, else false
     * */
    public bool AddWorker() {
        if (workersList.Count >= maxWorkerCount) {
            return false;
        }
        RectTransform rt = gameObject.GetComponent<RectTransform>();

        //getting local x and y position for worker based on the specified params
        float yPos = rt.rect.yMin + rt.rect.height / (maxRow + 1);
        float xPos = rt.rect.xMin + workerMargin * (workersList.Count + 1);
        Vector3 workerLocalPos = new Vector3(xPos, yPos, 0);

        GameObject workerObject =  Instantiate(worker);
        workerObject.transform.parent = gameObject.transform;   //sets created object as child
        workerObject.transform.localPosition = workerLocalPos;
        
        workersList.Add(workerObject);
        return true;
    }

    public bool RemoveWorker() {
        int lastIndex = workersList.Count - 1;
        if (lastIndex < 0) {
            return false;
        }

        Destroy(workersList[lastIndex]);
        workersList.RemoveAt(lastIndex);
        return true;
    }

    public int getWorkerCount() {
        return workersList.Count;
    }
}   
