﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Might want to move all button and other UI handling here */
public class GameController : MonoBehaviour {
    public GameObject workerDisplay;    //text displaying the number of idle workers
    private GameObject[] brainRegions;
    public string searchTag = "brainRegion";
    public GameObject infoPanel;

    //variables for workers
    public int maxWorkerCount = 10;
    private int activeWorkerCount = 0;
    public string workerCountDisplayString;

    //variables for player health
    public GameObject healthDisplay;
    public UnityEvent deathEvents;
    private int playerHealth = 100;
    private const int maxHealth = 100;


	// Use this for initialization
	void Start () {
        brainRegions = GameObject.FindGameObjectsWithTag(searchTag);
        Debug.Log(brainRegions.Length + " gameobjects with tag " + searchTag + " found");
       
        foreach (GameObject go in brainRegions) {
            // A brain region object should always have add button on index 1 and remove button on index 2
            go.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => AddWorker());
            go.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => RemoveWorker());
        }
	}

    public void AddWorker() {
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

    public void RemoveWorker() {
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

    /*
     * Player health funcions
     */
    public void AddHealth(int healthIncrease) {
        playerHealth += healthIncrease;
        if (playerHealth > maxHealth) {
            playerHealth = maxHealth;
        }

        updateHealthDisplay();
    }

    public void ReduceHealth(int healthDecrease) {
        playerHealth -= healthDecrease;
        if (playerHealth <= 0) {
            playerHealth = 0;
            
            Debug.Log("YOU DED DUDE");
            deathEvents.Invoke();
        }
        updateHealthDisplay();
    }

    void updateHealthDisplay() {
        healthDisplay.GetComponent<Slider>().value = playerHealth;
    }

}
