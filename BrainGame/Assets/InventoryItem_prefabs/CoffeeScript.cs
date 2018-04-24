using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeScript : MonoBehaviour {
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(BoostStamina);
	}

    void BoostStamina() {
        GameObject.FindWithTag("GameController").GetComponent<GameController>().AddStamina(30.0f);
    }
}
