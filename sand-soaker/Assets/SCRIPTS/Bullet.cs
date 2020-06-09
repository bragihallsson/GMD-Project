using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed;
    private Vector3 currPos;
    
    // Start is called before the first frame update
    void Start() {
        if (transform.rotation.y == 0) speed = 20f;
        else speed = -20f;

        currPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
        
        if(Mathf.Abs(currPos.x - transform.position.x) > 20) Destroy(gameObject);
    }
}
