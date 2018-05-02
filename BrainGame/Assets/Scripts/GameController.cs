using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
/* Might want to move all button and other UI handling here */
public class GameController : MonoBehaviour {
    public GameObject workerDisplay;    //text displaying the number of idle workers
    private GameObject[] brainRegions;
    public string searchTag = "brainRegion";
    public GameObject infoPanel;

    //variables for workers
    public int maxWorkerCount = 10;
    public float workerStaminaCostPerUpdate;
    private int activeWorkerCount = 0;
    public string workerCountDisplayString;

    //variables for player health (should be integers)
    public GameObject healthDisplay;
    public UnityEvent deathEvents;
    private int playerHealth;
    private int maxHealth;

    //variables for stamina (should be floats)
    public GameObject staminaDisplay;
    public UnityEvent exhaustedEvents;
    private float playerStamina;
    private float maxStamina;

	// Use this for initialization
	void Start () {

        UpdateWorkerDisplay();
        brainRegions = GameObject.FindGameObjectsWithTag(searchTag);
        Debug.Log(brainRegions.Length + " gameobjects with tag " + searchTag + " found");
       
        foreach (GameObject go in brainRegions) {
            // A brain region object should always have add button on index 1 and remove button on index 2
            go.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => AddWorker());
            go.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => RemoveWorker());
        }

        maxHealth = (int) healthDisplay.GetComponent<Slider>().maxValue;
        playerHealth = (int) healthDisplay.GetComponent<Slider>().value;
        maxStamina = staminaDisplay.GetComponent<Slider>().maxValue;
        playerStamina = staminaDisplay.GetComponent<Slider>().value;
	}

    void Update() {
        ReduceStamina(activeWorkerCount * workerStaminaCostPerUpdate);   
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

    /*
     * Player stamina functions 
     */
    public void AddStamina(float staminaIncrease) {
        playerStamina += staminaIncrease;
        if (playerStamina > maxStamina) {
            playerStamina = maxStamina;
        }

        updateStaminaDisplay();
    }

    public void ReduceStamina(float staminaDecrease) {
        playerStamina -= staminaDecrease;
        if (playerStamina <= 0) {
            playerStamina = 0;

            Debug.Log("YOU COLLAPSED DUDE");
            exhaustedEvents.Invoke();
        }
        updateStaminaDisplay();
    }

    void updateStaminaDisplay() {
        staminaDisplay.GetComponent<Slider>().value = playerStamina;
    }
}
