using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject bulletPrefab;
    public Animator animator;
    public AudioSource asrc;
    public AudioClip clip_running;
    public AudioClip clip_shotgun;

    private float horizontalMovement;
    private float speed;
    private Transform player;
    private bool onGround;
    private int shootingCounter;
    private int aimingFrameCounter;
    
    // Start is called before the first frame update
    void Start() {
        horizontalMovement = 0;
        if (transform.rotation.y == 0) speed = 7.5f;
        else speed = -7.5f;
        player = GameObject.Find("Player").transform;
        onGround = false;
        shootingCounter = 0;
        aimingFrameCounter = 0;
    }

    private void Update() {
        horizontalMovement = 0;
        if (Mathf.Abs(transform.position.x - player.position.x) > 5f) horizontalMovement = speed * Time.deltaTime;
    }

    void FixedUpdate() {
        movementCheck();
        shootingCheck();
    }

    private void movementCheck() {
        if (player.position.x - transform.position.x > 0) {
            speed = 7.5f;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else {
            speed = -7.5f;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (horizontalMovement == 0 && animator.GetBool("isRunning")) {
            animator.SetBool("isRunning", false);
            asrc.clip = null;
            asrc.loop = false;
            aimingFrameCounter = 0;
        }
        else if (horizontalMovement != 0 && !animator.GetBool("isRunning")) {
            animator.SetBool("isRunning", true);
            asrc.clip = clip_running;
            asrc.volume = 0.1f;
            asrc.Play();
            asrc.loop = true;
        }
        
        if(onGround && animator.GetBool("isRunning") && aimingFrameCounter >= 20) transform.position += new Vector3(horizontalMovement, 0f, 0f);

        ++aimingFrameCounter;
    }
    
    private void shootingCheck() {
        Vector3 pos = transform.position;
        if (!animator.GetBool("isRunning") && shootingCounter % 60 == 0) {
            float y = 0.17f;
            for (int i = 0; i < 3; ++i) {
                GameObject bullet;
                
                if (transform.rotation.y == 0)
                    bullet = Instantiate(bulletPrefab, new Vector3(pos.x + 0.55f, pos.y + y, -1f), Quaternion.identity);
                else
                    bullet = Instantiate(bulletPrefab, new Vector3(pos.x - 0.55f, pos.y + y, -1),
                        Quaternion.Euler(0f, 180f, 0f));

                y += 0.045f;
                bullet.tag = "EnemyBullet";
            }
            
            asrc.clip = clip_shotgun;
            asrc.volume = 0.3f;
            asrc.loop = false;
            asrc.Play();
        }

        ++shootingCounter;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag.Equals("Floor") && !onGround) onGround = true;
    }
}
