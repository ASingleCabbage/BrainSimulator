using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterScript : MonoBehaviour {
    [Range(0, 100)]
    public int healthIncrease;

	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddHealth(healthIncrease);
        });
	}
}
