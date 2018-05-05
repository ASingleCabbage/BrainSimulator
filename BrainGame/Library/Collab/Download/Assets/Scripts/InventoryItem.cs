using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
    public string itemName;
    public string itemKey;
    public string itemDescription;

    private GameObject gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindWithTag("GameController");
        gameObject.GetComponent<Button>().onClick.AddListener(() => useItem());
	}

    void useItem() {
        Debug.Log("Using item " + itemName);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
