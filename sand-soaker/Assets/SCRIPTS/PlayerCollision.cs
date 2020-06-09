using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {
    public GameObject finishUI;
    
    private float health;
    // Start is called before the first frame update
    void Start() {
        health = 100f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("EnemyBullet")) { 
            health -= 6.5f;

            if (health <= 0) {
                Time.timeScale = 0;
                finishUI.SetActive(true);
            }
            
            Destroy(other.gameObject);
        }
    }
}
