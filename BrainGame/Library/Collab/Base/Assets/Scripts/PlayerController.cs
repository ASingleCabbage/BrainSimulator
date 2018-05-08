using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
    private float speedMultiplier = 1.0f;
	public Vector2 baseJumpHeight;
	private Rigidbody2D rb2d;
    private bool jumpEnable = false;
    private WorkerContainer motorCortexContainer;

	void Start ()
	{
        motorCortexContainer = GameObject.Find("MotorCortex").GetComponent<WorkerContainer>();
		rb2d = GetComponent<Rigidbody2D> ();
	}
		
	void FixedUpdate()
	{
        //disable horizontal movement when in the air?
        float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, 0) * speed * speedMultiplier;
        gameObject.transform.Translate(movement * Time.deltaTime);
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && jumpEnable) {
            GetComponent<Rigidbody2D>().AddForce(baseJumpHeight * motorCortexContainer.GetWorkerCount(), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.transform.name == "GroundTrigger") {
            jumpEnable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.name == "GroundTrigger") {
            jumpEnable = false;
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
