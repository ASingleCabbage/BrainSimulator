using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGame : MonoBehaviour, IPointerClickHandler {

	private static readonly string[] possibleCards = {
		"Small Feet", "Medium Feet", "Large Feet",
		"Brown Hair", "Blonde Hair", "Black Hair",
		"Blue Eyes", "Brown Eyes", "Green Eyes",
		"Blunt Object", "Knife", "Gun",
		"Tattoo", "Scar", "Glasses"
	};

	public GameObject hand1;
	public GameObject hand2;
	public GameObject suspectLineup;
	public GameObject card;
	public GameObject suspect;
	public GameObject discardPile;

	public void OnPointerClick (PointerEventData eventData) {
		for (int i = hand1.transform.GetChildCount() - 1; i >= 0; i--) {
			   	Destroy(hand1.transform.GetChild(i).gameObject);
		}
		for (int i = hand2.transform.GetChildCount() - 1; i >= 0; i--) {
			   	Destroy(hand2.transform.GetChild(i).gameObject);
		}
		for (int i = suspectLineup.transform.GetChildCount() - 1; i >= 0; i--) {
			   	Destroy(suspectLineup.transform.GetChild(i).gameObject);
		}
		for (int i = discardPile.transform.GetChildCount() - 1; i >= 0; i--) {
			   	Destroy(discardPile.transform.GetChild(i).gameObject);
		}
		DealHand (hand1);
		DealHand (hand2);
		GenerateSuspects (suspectLineup);
	}

	private void DealHand(GameObject hand) {
		for (int i = 0; i < 4; i++) {
			GameObject createdCard = Instantiate (card, hand.transform);
			Text cardText;
			cardText = createdCard.GetComponentInChildren<Text> ();
			cardText.text = possibleCards [Random.Range (0, possibleCards.Length)];
		}
	}

	private void GenerateSuspects(GameObject lineup) {
		for (int i = 0; i < 3; i++) {
			CreateNewSuspect (lineup);
		}
	}

	private void CreateNewSuspect(GameObject lineup) {
			GameObject createdSuspect = Instantiate (suspect, lineup.transform);
			Text cardText;
			cardText = createdSuspect.GetComponentInChildren<Text> ();
			int index1 = Random.Range (0, 4);
			int index2 = Random.Range (0, 4);
			int index3 = Random.Range (0, 4);
			while (index2 == index1) {
				index2 = Random.Range (0, 4);
			}
			while (index3 == index1 || index3 == index2) {
				index3 = Random.Range (0, 4);
			}
			
			int subindex1 = Random.Range (0, 2);
			int subindex2 = Random.Range (0, 2);
			int subindex3 = Random.Range (0, 2);

			cardText.text = possibleCards [(3 * index1) + subindex1];
			cardText.text += "\n" + possibleCards [(3 * index2) + subindex2];
			cardText.text += "\n" + possibleCards [(3 * index3) + subindex3];
	}
}
