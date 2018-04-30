using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualControl : MonoBehaviour {
    //keyName is defined in the input manager
    public string keyName;
    public float secondsFrequency;
    [Range(0.0f, 1.0f)]
    public float toleranceRange;    //should be between 0 and 1
    public Color targetColor;

    //Damage parameters 
    public float maxAllowance;      //when timeSinceLastPress reaches maxAllowance, player takes damage
    public float damageFrequency;   //how frequent damage is dealt once max allowance is reached
    [Range(0, 100)]
    public int damageDealt;
    private bool maxAllowanceReachedPreviously = false;
    private float timeSinceLastDamage; 

    private float timeSinceLastPress;
    private float minToleranceTime;
    private float maxToleranceTime;
    private Color startingColor;

    private Button linkedButton;
    private bool buttonPress;

    private GameController gameController;  //GameController script reference

	// Use this for initialization
	void Start () {
        timeSinceLastPress = 0.0f;
        minToleranceTime = secondsFrequency - secondsFrequency * toleranceRange;
        maxToleranceTime = secondsFrequency + secondsFrequency * toleranceRange;
        startingColor = gameObject.GetComponent<Image>().color;


        buttonPress = false;
        linkedButton = gameObject.GetComponent<Button>();
        linkedButton.interactable = false;
        linkedButton.onClick.AddListener(delegate { buttonPress = true; });

        Debug.Log("Min time: " + minToleranceTime + " Max time: " + maxToleranceTime);
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}

    void OnEnable() {
        timeSinceLastPress = 0.0f;        
    }


    // Update is called once per frame
    void Update () {
        if ((Input.GetButtonDown(keyName) || buttonPress == true) && timeSinceLastPress > minToleranceTime) {
            linkedButton.interactable = false;
            buttonPress = false;

            timeSinceLastPress = 0.0f;
            timeSinceLastDamage = 0.0f;
        } else if (timeSinceLastPress > maxAllowance) {
            damageCheck();
        }
        setColor(timeSinceLastPress);
        timeSinceLastPress += Time.deltaTime;
	}

    //Checks if it's time to deal damage to player
    void damageCheck() {
        if(timeSinceLastDamage > damageFrequency) {
            Debug.Log("Dealing damage");
            gameController.ReduceHealth(damageDealt);
            timeSinceLastDamage = 0.0f;
        } else {
            timeSinceLastDamage += Time.deltaTime;
        }
    }

    //added some code here to set button as interactible only when active
    void setColor (float time) {
        if (time < minToleranceTime) {
            gameObject.GetComponent<Image>().color = startingColor;
            return;
        }

        linkedButton.interactable = true;
        float percentTime = (time - minToleranceTime) / (maxToleranceTime - minToleranceTime);
        if (percentTime > 1.0f) {
            gameObject.GetComponent<Image>().color = targetColor;
        } else {
            gameObject.GetComponent<Image>().color = startingColor + ((targetColor - startingColor) * percentTime);
        }
    }
}
