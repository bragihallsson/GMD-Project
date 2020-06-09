using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    
    private float spawningRate;
    private Transform player;
    
    // Start is called before the first frame update
    void Start() {
        spawningRate = 0.3f;
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate() {
        float rnd = Random.Range(0.000f, 100.001f);

        if (rnd < spawningRate) {
            int rndPos = Random.Range(1, 3);
            Vector3 pos = player.position;
            
            switch (rndPos) {
                case 1:
                    pos.x -= 15f;
                    pos.y = 4f;
                    Instantiate(enemyPrefab, pos, Quaternion.identity);
                    break;
                case 2:
                    pos.x += 15f;
                    pos.y = 4f;
                    Instantiate(enemyPrefab, pos, Quaternion.Euler(0f, 180f, 0f));
                    break;
            }
        }
    }
}
