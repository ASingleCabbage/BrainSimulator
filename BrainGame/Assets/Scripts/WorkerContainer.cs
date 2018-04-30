using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkerContainer : MonoBehaviour {
    public GameObject worker;
    public GameObject infoPanel;
    public float workerMargin;
    public int maxRow;
    public int maxWorkerCount = 6;

    public string defaultMessage = "[No Effect]";

    [SerializeField]
    public string[] infoTexts;
    [SerializeField]
    public UnityEvent[] clickFunctionCalls;

    private List<GameObject> workersList;
    private string[] messages;


    public Button addButton;
    public Button remButton;
    
	void Start () {
        workersList = new List<GameObject>();

        //Print out the initial messages and functions
        //PrintMessage();
        TriggerWorkerCountFunction(0);
    }
    
    private void PrintMessage() {
        string message;
        if (workersList.Count < infoTexts.Length) {
            message = infoTexts[workersList.Count];
            if (message.Equals("")) {
                message = defaultMessage;
            }
        } else {
            message = defaultMessage;    
        }
        infoPanel.GetComponent<InfoPanel>().displayText(message);
    }

    private void TriggerWorkerCountFunction(int workerCount) {
        if (clickFunctionCalls.Length > workerCount) {
            clickFunctionCalls[workerCount].Invoke();
        } 
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
        TriggerWorkerCountFunction(workersList.Count);
        PrintMessage();
        return true;
    }

    public bool RemoveWorker() {
        int lastIndex = workersList.Count - 1;
        if (lastIndex < 0) {
            return false;
        }

        Destroy(workersList[lastIndex]);
        workersList.RemoveAt(lastIndex);
        TriggerWorkerCountFunction(workersList.Count);
        PrintMessage();
        return true;
    }

    public int GetWorkerCount() {
        return workersList.Count;
    }
}   
