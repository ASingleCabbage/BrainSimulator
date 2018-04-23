﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
    private float speedMultiplier = 1.0f;
	public Vector2 jumpheight;
	private Rigidbody2D rb2d;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, 0) * speed * speedMultiplier;
        gameObject.transform.Translate(movement * Time.deltaTime);

		if (Input.GetKeyDown (KeyCode.Space)) {
			GetComponent<Rigidbody2D> ().AddForce (jumpheight, ForceMode2D.Impulse);
		}

	}

    public void setSpeedMultiplier(float multiplier) {
        if (multiplier >= 0.0f && multiplier <= 1.0f) {
            speedMultiplier = multiplier;
        } else {
            Debug.LogWarning("Invalid parameter for setSpeedMultiplier " + multiplier);
        }
    }
}