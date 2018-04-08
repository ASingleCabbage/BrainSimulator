using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Might want to move all button and other UI handling here */
public class GameController : MonoBehaviour {
    public GameObject workerDisplay;    //text displaying the number of idle workers
    public GameObject[] brainRegions;
    public string searchTag = "brainRegion";
    public GameObject infoPanel;

    public int maxWorkerCount = 10;
    private int activeWorkerCount = 0;
    public string workerCountDisplayString;


	// Use this for initialization
	void Start () {
        brainRegions = GameObject.FindGameObjectsWithTag(searchTag);
        Debug.Log(brainRegions.Length + " gameobjects with tag " + searchTag + " found");
       
        foreach (GameObject go in brainRegions) {
            Dictionary<string, string[]> messageDict = setupMessages();
            if (messageDict.ContainsKey(go.name)) {
                go.GetComponent<WorkerContainer>().SetMessages(messageDict[go.name], infoPanel);
            } else {
                Debug.Log("There is no messages set for key " + go.name);
            }

            // A brain region object should always have add button on index 1 and remove button on index 2
            go.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => AddWorker());
            go.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => RemoveWorker());
        }
	}

    /*
     * Messages displayed are all defined here, hardcoded.
     * */
    private Dictionary<string, string[]> setupMessages() {
        Dictionary<string, string[]> messagesDict = new Dictionary<string, string[]>();

        /* Problem: without knowing exactly what effects each change has, I can't make very meaningful info texts*/
        string r1 = "FrontalLobe";
        string[] m1 = {"I..feel paralyzed", "I...have no idea what I'm doing...", "I can't seem to study today", "I feel motivated", "I feel inspired", "", "" };
        messagesDict.Add(r1, m1);

        string r2 = "ParietalLobe";
        string[] m2 = { "What's going on?", "I feel slightly confused", "Everything seems fine", "", "", "", "" };
        messagesDict.Add(r2, m2);

        string r3 = "OccipitalLobe";
        string[] m3 = { "Who turned off the lights?", "Everything's gone blurry", "Maybe I need glasses after all", "Everything looks...so clear", "", "", "" };
        messagesDict.Add(r3, m3);

        string r4 = "MotorCortex";
        string[] m4 = { "Is this what paralysis feels like?", "Muscles are feeling a bit numb", "My body feels...ok..", "My body feels light", "", "", "" };
        messagesDict.Add(r4, m4);

        string r5 = "TemporalLobe";
        string[] m5 = { "The world has gone silent", "Huh...everything's sounding weird", "The birds are chirping", "", "", "", "" };
        messagesDict.Add(r5, m5);

        string r6 = "BrainStem";
        string[] m6 = { "No...heartbeat...", "Can't....breathe.....", "I feel okay...", "", "", "", "" };
        messagesDict.Add(r6, m6);
        return messagesDict;
    }

    void AddWorker() {
        if (activeWorkerCount >= maxWorkerCount) {
            Debug.Log("Cannot add more workers");
            return;
        }
        //true if add successful, false otherwise
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<WorkerContainer>().AddWorker()) {
            activeWorkerCount++;
            UpdateWorkerDisplay();
        } else {
            Debug.Log("Container cannot hold more workers");
        }
    }

    void RemoveWorker() {
        if (activeWorkerCount == 0) {
            Debug.Log("Cannot remove more workers");
            return;
        }
        if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.GetComponent<WorkerContainer>().RemoveWorker()) {
            activeWorkerCount--;
            UpdateWorkerDisplay();
        } else {
            Debug.Log("Container has no more workers to remove");
        }
    }

    void UpdateWorkerDisplay() {
        workerDisplay.GetComponent<Text>().text = String.Format(workerCountDisplayString, maxWorkerCount - activeWorkerCount, maxWorkerCount);
    }

	// Update is called once per frame
	void Update () {

	}
}
