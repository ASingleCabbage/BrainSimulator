using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
    private float speedMultiplier = 1.0f;
	public Vector2 baseJumpHeight;
    public int knockbackForceMultiplier = 1;
    public float immuneDuration = 0.5f;

    public bool isImmune = false;
    private float immuneCounter = 0.0f;

    private Rigidbody2D rb2d;
    private bool jumpEnable = false;
    private WorkerContainer motorCortexContainer;
	private Animator animator;

	void Start (){
        motorCortexContainer = GameObject.Find("MotorCortex").GetComponent<WorkerContainer>();
		rb2d = GetComponent<Rigidbody2D> ();
		animator = this.GetComponent<Animator>();
	}
		
	void FixedUpdate()
	{
        if (isImmune) {
            immuneCounter += Time.deltaTime;
            if (immuneCounter >= immuneDuration) {
                isImmune = false;
                immuneCounter = 0.0f;
                gameObject.GetComponent<FlashEffect_Sprite>().Deactivate();
            }
        }

        //disable horizontal movement when in the air? sure, good idea. -eduardo
        float moveHorizontal = Input.GetAxis("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, 0) * speed * speedMultiplier;
        gameObject.transform.Translate(movement * Time.deltaTime);
	}

    public void TriggerImmuneEffect() {
        isImmune = true;
        gameObject.GetComponent<FlashEffect_Sprite>().Activate();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) && jumpEnable) {
            GetComponent<Rigidbody2D>().AddForce(baseJumpHeight * motorCortexContainer.GetWorkerCount(), ForceMode2D.Impulse);
        }

		if (Input.GetKey(KeyCode.W))
			animator.SetInteger("PlayerWalking",0);
			animator.SetInteger ("PlayerAttacking", 0);
		if(Input.GetKey(KeyCode.A))
			animator.SetInteger("PlayerWalking",1);
			animator.SetInteger ("PlayerAttacking", 0);
		if(Input.GetKey(KeyCode.D))
			animator.SetInteger("PlayerWalking",1);
			animator.SetInteger ("PlayerAttacking", 0);
		if(Input.GetKey(KeyCode.Space))
			animator.SetInteger("PlayerAttacking",1);
			animator.SetInteger ("PlayerWalking", 0);
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

    public void Knockback(Vector2 force) {
        GetComponent<Rigidbody2D>().AddForce(force * knockbackForceMultiplier, ForceMode2D.Impulse);
    }
}
