using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public GameObject infoText;
    [Range(0.0f, 5.0f)]
    public float defaultDisplayTime = 3.0f;
    public float fadeOutBegin = 0.0f;

    private GameObject currTextObj = null;
    private float timeSinceCreate = 0.0f;

    private float overrideDuration = 0.0f;
    private bool overrideActive = false;

    private void FixedUpdate() {
        if (overrideActive) {
            if (trackTime(overrideDuration)) {
                overrideDuration = 0.0f;
                overrideActive = false;
            }
        } else {
            trackTime(defaultDisplayTime);
        }
    }

    bool trackTime(float targetTime) {
        if (currTextObj != null) {
            if (timeSinceCreate >= targetTime) {
                Destroy(currTextObj);
                timeSinceCreate = 0.0f;
                currTextObj = null;
                return true;
            } else {
                if (timeSinceCreate > fadeOutBegin) {
                    currTextObj.GetComponent<CanvasGroup>().alpha = ((targetTime - fadeOutBegin) - (timeSinceCreate - fadeOutBegin)) / (targetTime - fadeOutBegin); //better way to do this?
                }
                timeSinceCreate += Time.deltaTime;
            }    
        }
        return false;
    }

    public bool displayText(string s) {
        if (overrideActive) {
            return false;
        }
        createText(s);
        return true;
    }

    public void displayTextOverride(string s, float duration) {
        createText(s);
        overrideActive = true;
        overrideDuration = duration;
    }

    public void overrideCancel() {
        overrideDuration = 0.0f;
    }

    void createText(string s) {
        if (currTextObj == null) {
            currTextObj = Instantiate(infoText);
            currTextObj.GetComponent<UnityEngine.UI.Text>().text = s;
            currTextObj.transform.SetParent(gameObject.transform, false);
            timeSinceCreate = 0.0f;
        } else {
            Destroy(currTextObj);
            currTextObj = null;
            displayText(s);
        }
    }
}
