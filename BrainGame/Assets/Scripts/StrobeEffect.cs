using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrobeEffect : MonoBehaviour {
    public Color targetColor;
    public float strobeInterval = 1.0f;
    public bool activateOnStart = false;

    private Color initialColor;
    private bool isActive;

	void Start () {
        initialColor = gameObject.GetComponent<Graphic>().color;
        if (activateOnStart) {
            isActive = true;
        } else {
            isActive = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isActive) {
            gameObject.GetComponent<Graphic>().color = Color.Lerp(initialColor, targetColor, Mathf.PingPong(Time.time, strobeInterval));
        }
	}

    public void Activate() {
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
    }
}
