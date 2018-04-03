using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DrawCard : MonoBehaviour, IPointerClickHandler {

	private static readonly string[] possibleCards = {
		"Small Feet", "Medium Feet", "Large Feet",
		"Brown Hair", "Blonde Hair", "Black Hair",
		"Blue Eyes", "Brown Eyes", "Green Eyes",
		"Blunt Object", "Knife", "Gun",
		"Tattoo", "Scar", "Glasses"
	};

	public GameObject card;

	public void OnPointerClick (PointerEventData eventData)
	{
		GameObject[] hands = GameObject.FindGameObjectsWithTag ("PlayerHand");
		if (hands.Length != 0) {
			GameObject hand = hands [0];
			if (hand.transform.GetChildCount() < 4) {
				GameObject createdCard = Instantiate (card, hand.transform);
				Text cardText;
				cardText = createdCard.GetComponentInChildren<Text>();
				cardText.text = possibleCards[Random.Range(0, possibleCards.Length)];
			}
		}
	}

	// Use this for initialization
	public void ClickTest () {
		Debug.Log ("Hello world");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
