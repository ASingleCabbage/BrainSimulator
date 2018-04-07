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
    private string[] messages;
    private GameObject infoPanel;


    public Button addButton;
    public Button remButton;
    
	void Start () {
        workersList = new List<GameObject>();
    }

    public void SetMessages(string[] messages, GameObject infoPanel) {
        this.messages = messages;
        this.infoPanel = infoPanel;
    }

    //Default message is set here
    private void PrintMessage() {
        if (workersList.Count < messages.Length) {
            string message = messages[workersList.Count];
            if (message.Equals("")) {
                message = "[NO EFFECT]";
            }
            infoPanel.GetComponent<InfoPanel>().displayText(message);
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
        PrintMessage();
        return true;
    }

    public int getWorkerCount() {
        return workersList.Count;
    }
}   
