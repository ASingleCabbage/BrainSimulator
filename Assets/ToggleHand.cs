using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleHand : MonoBehaviour, IPointerClickHandler {

	public GameObject hand1;
	public GameObject hand2;
	public GameObject P1Label;
	public GameObject P2Label;

	public void OnPointerClick (PointerEventData eventData)
	{
		if (hand1.active == false) {
			hand2.active = false;
			P2Label.active = false;
			hand1.active = true;
			P1Label.active = true;
		}
		else {
			hand1.active = false;
			P1Label.active = false;
			hand2.active = true;
			P2Label.active = true;
		}
	}
}
