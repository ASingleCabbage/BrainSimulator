using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour {
    public Color targetColor;
    public float flashInterval = 1.0f;
    public bool activateOnStart = false;

    private Color initialColor;
    private bool isActive;
    private bool isTargetColor;
    private float currTime;
    private Graphic gameObjectGraphicComponent;

    void Start() {
        gameObjectGraphicComponent = gameObject.GetComponent<Graphic>();
        initialColor = gameObjectGraphicComponent.color;
        if (activateOnStart) {
            isActive = true;
        } else {
            isActive = false;
        }
        isTargetColor = false;
        currTime = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (isActive) {
            if (currTime > flashInterval) {
                currTime = 0.0f;
                if (isTargetColor) {
                    isTargetColor = false;
                    gameObjectGraphicComponent.color = initialColor;
                } else {
                    isTargetColor = true;
                    gameObjectGraphicComponent.color = targetColor;
                }
            }
            currTime += Time.deltaTime;
        }
    }

    public void Activate() {
        isActive = true;
    }

    public void Deactivate() {
        isActive = false;
        gameObjectGraphicComponent.color = initialColor;
    }
}
