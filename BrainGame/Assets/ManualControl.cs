using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualControl : MonoBehaviour {
    //keyName is defined in the input manager
    public string keyName;
    public float secondsFrequency;
    public float toleranceRange;    //should be between 0 and 1
    public float maxAllowance;      //when timeSinceLastPress reaches maxAllowance, you die
    public Color targetColor;

    private float timeSinceLastPress;
    private float minToleranceTime;
    private float maxToleranceTime;
    private Color startingColor;

	// Use this for initialization
	void Start () {
        timeSinceLastPress = 0.0f;
        minToleranceTime = secondsFrequency - secondsFrequency * toleranceRange;
        maxToleranceTime = secondsFrequency + secondsFrequency * toleranceRange;
        startingColor = gameObject.GetComponent<Image>().color;
        Debug.Log("Min time: " + minToleranceTime + " Max time: " + maxToleranceTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(keyName) && timeSinceLastPress > minToleranceTime) {
            Debug.Log("Button " + keyName + " is held down. It's been " + timeSinceLastPress + " seconds since last press");
            timeSinceLastPress = 0.0f;
        } else if (timeSinceLastPress > maxAllowance) {
            Debug.Log("You died");
        }
        setColor(timeSinceLastPress);
        timeSinceLastPress += Time.deltaTime;
	}

    void setColor (float time) {
        if (time < minToleranceTime) {
            gameObject.GetComponent<Image>().color = startingColor;
            return;
        }

        float percentTime = (time - minToleranceTime) / (maxToleranceTime - minToleranceTime);
        if (percentTime > 1.0f) {
            gameObject.GetComponent<Image>().color = targetColor;
        } else {
            gameObject.GetComponent<Image>().color = startingColor + ((targetColor - startingColor) * percentTime);
        }
    }
}
