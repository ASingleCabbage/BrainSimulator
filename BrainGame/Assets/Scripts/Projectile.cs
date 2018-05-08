using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Vector2 knockbackForce;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (!playerController.isImmune) {
                playerController.TriggerImmuneEffect();
                playerController.Knockback(knockbackForce);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ReduceHealth(damage);
            }
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
