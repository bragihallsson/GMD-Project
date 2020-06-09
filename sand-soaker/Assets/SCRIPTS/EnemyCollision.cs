using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {
    private float health;
    private int playerType;
    // Start is called before the first frame update
    void Start() {
        health = 2000f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Bullet")) {
            health -= 50;
            
            if (health <= 0) Destroy(gameObject);
            
            Destroy(other.gameObject);
        }
    }
}
