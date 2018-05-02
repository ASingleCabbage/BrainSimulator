using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The inventory system really can be more intuitive, but hey I don't really have time to think things through
 * And at least I added comments
 */
public class InventoryManager : MonoBehaviour {
    public GameObject scrollViewContent;

    //used to make editing from inspector possible, data transferred to itemDict when script initializes
    public List<GameObject> itemList;
    //stores all item and their info
    private Dictionary<string, GameObject> itemDict;    
    //dictionary for active objects in inventory, stores inventoryItem which also keep track of item count
    private Dictionary<string, ActiveInventoryItem> inventoryDict;    

	void Start () {
        foreach (Transform child in scrollViewContent.transform) {
            Destroy(child.gameObject);
        }
        setupItemDict();
        inventoryDict = new Dictionary<string, ActiveInventoryItem>();

        //code to add some coffee to inventory as test
        for (int i = 0; i < 5; i++) {
            addItemCount("coffee");
        }
    }

    void setupItemDict() {
        itemDict = new Dictionary<string, GameObject>();
        for (int i = 0; i < itemList.Count; i++) {
            InventoryItem itemScript = itemList[i].GetComponent<InventoryItem>();
            Debug.Assert(!itemScript.Equals(null)); //ensures all item in list have InventoryItem component

            if (!itemDict.ContainsKey(itemScript.itemKey)) {
                itemDict.Add(itemScript.itemKey, itemList[i]);
            } else {
                Debug.LogWarning("Duplicate key spotted when setting up itemDict");
            }
        }
    }

    /*
     *  Public functions to add or remove item count from inventory (items are stacked)
     */
    public void addItemCount(string key) {
        ActiveInventoryItem activeItem;
        if (inventoryDict.ContainsKey(key)) {
            activeItem = inventoryDict[key];
            activeItem.itemCount += 1;
            inventoryDict[key] = activeItem;
        } else {
            activeItem = new ActiveInventoryItem();
            activeItem.itemCount = 1;
            activeItem.itemObject = itemDict[key];
            activeItem.itemInstance = addScrollViewItem(itemDict[key]);
            activeItem.itemInstance.GetComponent<Button>().onClick.AddListener(delegate { reduceItemCount(key); });
            updateScrollViewItemCount(activeItem);
            inventoryDict.Add(key, activeItem);
        }
        updateScrollViewItemCount(activeItem);
    }

    public bool reduceItemCount(string key) {
        if (!inventoryDict.ContainsKey(key)) {
            Debug.LogWarning("item key to remove from inventory doesn't exist in inventory");
            return false;
        }
        ActiveInventoryItem activeItem = inventoryDict[key];
        if (activeItem.itemCount <= 1) {
            removeScrollViewItem(activeItem);
            inventoryDict.Remove(key);
        } else {
            activeItem.itemCount -= 1;
            updateScrollViewItemCount(activeItem);
            inventoryDict[key] = activeItem;
        }
        return true;
    }

    /*
     * Functions to change scroll view display 
     */
    GameObject addScrollViewItem(GameObject itemObject) {
        GameObject newItem = Instantiate(itemObject);
        newItem.transform.SetParent(scrollViewContent.transform, false);
        return newItem;
    }

    void updateScrollViewItemCount(ActiveInventoryItem activeItem) {
        activeItem.itemInstance.GetComponentInChildren<Text>().text = activeItem.itemObject.GetComponent<InventoryItem>().itemName + "\t X " + activeItem.itemCount;
    }
    
    void removeScrollViewItem(ActiveInventoryItem activeItem) {
        Destroy(activeItem.itemInstance);
    }

    

    struct ActiveInventoryItem {
        public int itemCount;
        public GameObject itemObject;
        public GameObject itemInstance;
    }
    
}
