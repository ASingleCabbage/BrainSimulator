using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewManager : MonoBehaviour {
    public GameObject scrollViewContent;
    public GameObject item;

    private List<GameObject> itemList;

	// Use this for initialization
	void Start () {
        itemList = new List<GameObject>();

        for (int i = 0; i < 2; i++) {
            addItemToScrollView();
        }
    }

    void addItemToScrollView() {
        GameObject newItem = Instantiate(item);
        newItem.transform.SetParent(scrollViewContent.transform, false);
        itemList.Add(item);

    }
	

    
}
