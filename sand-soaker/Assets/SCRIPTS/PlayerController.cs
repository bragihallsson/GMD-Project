using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    public Animator animator;
    public AudioSource asrc;
    public AudioClip clip_running;
    public AudioClip clip_shotgun;
    
    private float speed;
    private float horizontalMovement;
    private bool isAiming;
    private bool isShoting;
    private int shootingCounter;
    private bool isJumping;
    
    // Start is called before the first frame update
    void Start() {
        Time.timeScale = 1;
        
        speed = 7.5f;
        horizontalMovement = 0;
        isAiming = false;
        isShoting = false;
        shootingCounter = 0;
        isJumping = false;
    }

    // Update is called once per frame
    void Update() {
        horizontalMovement = 0;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5) horizontalMovement = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if (Input.GetAxis("Vertical") > 0.5 && !isJumping && horizontalMovement != 0) jump();
        else if (Input.GetAxis("Vertical") < -0.5) {
            isAiming = true;
            horizontalMovement = 0;
        }
        else if(animator.GetBool("isAiming")) isAiming = false;
    }

    private void FixedUpdate() {
        shootingCheck();
        aimingCheck();
        runningCheck();
    }

    private void jump() {
        isJumping = true;
        animator.SetBool("isJumping", isJumping);
        rb.velocity = new Vector2(0, 5);
    }

    private void shootingCheck() {
        Vector3 pos = transform.position;
        if (isShoting && shootingCounter % 10 == 0) {
            float y = 0.17f;
            for (int i = 0; i < 3; ++i) {
                if (transform.rotation.y == 0)
                    Instantiate(bulletPrefab, new Vector3(pos.x + 0.55f, pos.y + y, -1f), Quaternion.identity);
                else
                    Instantiate(bulletPrefab, new Vector3(pos.x - 0.55f, pos.y + y, -1),
                        Quaternion.Euler(0f, 180f, 0f));
                
                y += 0.045f;
            }
            
            asrc.clip = clip_shotgun;
            asrc.volume = 0.3f;
            asrc.loop = false;
            asrc.Play();
        }
        
        ++shootingCounter;
    }

    private void aimingCheck() {
        if(isAiming) animator.SetBool("isAiming", isAiming);
        else if(animator.GetBool("isAiming")) animator.SetBool("isAiming", false);
    }

    private void runningCheck() {
        if (horizontalMovement == 0) {
            if (animator.GetBool("isRunning")) {
                animator.SetBool("isRunning", false);
                asrc.clip = null;
                asrc.loop = false;
            }
            return;
        }

        if (!animator.GetBool("isRunning")) {
            animator.SetBool("isRunning", true);
            asrc.clip = clip_running;
            asrc.volume = 0.1f;
            asrc.Play();
            asrc.loop = true;
        }

        if(transform.rotation.y != 180 && horizontalMovement < 0) transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        else if(transform.rotation.y != 0 && horizontalMovement > 0) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        transform.position += new Vector3(horizontalMovement, 0f, 0f);
    }
    
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag.Equals("Floor")) {
            if (!isJumping) return;
            
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
        }
    }
    
    public void OnFireDown() {
        shootingCounter = 0;
        isShoting = true;
    }

    public void OnFireUp() {
        isShoting = false;
    }
}
