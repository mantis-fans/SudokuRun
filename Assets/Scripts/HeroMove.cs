using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine;
using System;
using System.Text;
using UnityEditor;

public class HeroMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float playerSpeed;
    private float horizontal;
    private bool facingRight = true;
    [SerializeField] float jumpSpeed;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundLayer;
    Vector2 vecGravity;
    [SerializeField] float jumpMultiplier;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpTime;
    float jumpCounter;
    bool isJumping;
    [SerializeField] bool doubleJump;

    Vector3 ResetPosition;
    // Start is called before the first frame update
    void Awake(){
        //GetComponent<Numbers>().SetNum(2);
        ResetPosition = transform.position;
    }

    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Reset();
            GameController.Reset();
            GetComponent<GameController>().ColliderCheck();
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")){
            if(isGround()){
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                isJumping = true;
                jumpCounter = 0;
                doubleJump = true;
            }
            else if(doubleJump){
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*0.9f);
                isJumping = true;
                jumpCounter = 0;
                doubleJump = false;
            }
            
        }
        if(Input.GetButtonUp("Jump")){
            isJumping = false;
        }
    }

    private void FixedUpdate(){
        Move();
        if(isGround())
            doubleJump = true;
        transform.Find("ColliderBox").GetComponent<ColliderBox>().SetContact(false);
        Jump();
    }

    private void Move(){
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        if((facingRight && horizontal < 0) || (!facingRight && horizontal > 0)){
            facingRight = ! facingRight;
            transform.GetChild(1).localScale = new Vector3(transform.GetChild(1).localScale.x * -1, 1, 1);
        }
        
    }

    private bool isGround(){
        //  Physics2D.OverlapCapsule(GroundCheck.position, new Vector2(0.9f, 0.03f), CapsuleDirection2D.Horizontal, 0, GroundLayer) && 
        return transform.Find("ColliderBox").GetComponent<ColliderBox>().isContact();
    }

    private void Jump(){
        if(rb.velocity.y > 0 && isJumping){
            jumpCounter += Time.deltaTime;
            if(jumpCounter > jumpTime)
                isJumping = false;
            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }
        if(rb.velocity.y < 0)
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
    }

    public void Reset(){
        transform.position = ResetPosition;
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Numbers>().Reset();
    }
}
