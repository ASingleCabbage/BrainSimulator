using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Might want to move all button and other UI handling here */
public class GameController : MonoBehaviour {
    public GameObject workerDisplay;
    public List<GameObject> brainRegions;   //adding in brianRegions will require more scripting or custom inspector

    //so I'll just use a variable for now
    public GameObject brainRegion;

    public int maxWorker;
    public string workerCountDisplayString;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //might want to update workerCount only when button presses
        int usedWorkerCount = 0;
        //foreach (GameObject region in brainRegions) {
        //    usedWorkerCount += region.GetComponent<WorkerContainer>().getWorkerCount();
        //}
        usedWorkerCount = brainRegion.GetComponent<WorkerContainer>().getWorkerCount();
        Debug.Log("setting text...");
        workerDisplay.GetComponent<Text>().text = string.Format(workerCountDisplayString, maxWorker - usedWorkerCount, maxWorker);
	}
}
