using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {
    public GameObject infoText;
    [Range(0.0f, 5.0f)]
    public float displayTimeSeconds = 2.0f;

    private GameObject currTextObj = null;
    private float timeSinceCreate = 0.0f;

    private void Update() {
        if (currTextObj != null) {
            if (timeSinceCreate >= displayTimeSeconds) {
                Destroy(currTextObj);
                timeSinceCreate = 0.0f;
                currTextObj = null;
            } else {
                currTextObj.GetComponent<CanvasGroup>().alpha =  (displayTimeSeconds - timeSinceCreate) / displayTimeSeconds; //better way to do this??
                timeSinceCreate += Time.deltaTime;
            }
        }
    }

    public void displayText(string s) {
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
