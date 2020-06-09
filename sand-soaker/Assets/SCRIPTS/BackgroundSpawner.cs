using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundSpawner : MonoBehaviour {
    public List<GameObject> maps;

    private LinkedList<GameObject> backgroundList;
    private Transform player;
    
    // Start is called before the first frame update
    void Start() {
        backgroundList = new LinkedList<GameObject>();
        player = GameObject.Find("Player").transform;

        int rnd = Random.Range(0, 4);
        
        Vector3 pos = new Vector3(0f ,0f ,0f);
        backgroundList.AddLast(Instantiate(maps[0], pos, Quaternion.identity));
        pos.x += 18.08f;
        backgroundList.AddLast(Instantiate(maps[rnd], pos, Quaternion.identity));
        pos.x = -18.08f;
        rnd = Random.Range(0, 4);
        backgroundList.AddFirst(Instantiate(maps[rnd], pos, Quaternion.identity));
    }

    private void FixedUpdate() {
        if (backgroundList.Count == 0) return;
        
        if (backgroundList.Last.Value.transform.position.x - player.position.x < 5) {
            Vector3 pos = backgroundList.Last.Value.transform.position;
            pos.x += 18.08f;

            int rndR = Random.Range(0, 4);
            backgroundList.AddLast(Instantiate(maps[rndR], pos, Quaternion.identity));
        }
        else if (player.position.x - backgroundList.First.Value.transform.position.x < 5) {
            Vector3 pos = backgroundList.First.Value.transform.position;
            pos.x -= 18.08f;

            int rndL = Random.Range(0, 4);
            backgroundList.AddFirst(Instantiate(maps[rndL], pos, Quaternion.identity));
        }

        Vector3 posR = backgroundList.Last.Value.transform.position;
        if (posR.x - player.position.x > 50) {
            Destroy(backgroundList.Last.Value);
            backgroundList.RemoveLast();
        }

        Vector3 posL = backgroundList.First.Value.transform.position;
        if (player.position.x - posL.x > 50) {
            Destroy(backgroundList.First.Value);
            backgroundList.RemoveFirst();
        }


    }
}
