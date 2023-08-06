using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Hero : MonoBehaviour
{
	public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform groundCheck3;
    public Transform groundCheck4;
    public Transform groundCheck5;
    public Transform groundCheck6;
    public float checkRadius;
    public LayerMask whatIsGround;

	public bool notFlying, touchingGround, touchingWall, touchingTop, onWall;

	Rigidbody2D rb;
	BoxCollider2D collider;

    Vector2 speed;
    public float xSpeedMax = 4, ySpeedMax = -10;
	public float speedx = 20;
    public float jumpForce = 5;
    public float gravity = 7;
    public float sliding = 2;
	public float fric = 0.5f;
    public int moveDirection = 1;
    bool doubleJump = true;
	public bool facingRight = true;

    float wallJumpTime = -1;
    float wallJumpTimeMax = 0.15f;

	public int num;
	public GameObject number;

	public int status;

	public int dead = 0;
	public Pos deadPos;
	public float deadWaitTime, deadMoveTime, deadTime;

    private void Awake() {
    	rb = GetComponent<Rigidbody2D>();
    	collider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		Pos tempPos = Pos.Copy(GameObject.Find("background").GetComponent<Background>().GetRoomNow().GetComponent<Room>().start);
		status = GameObject.Find("background").GetComponent<Background>().GetStatus();
		switch(status){
			case Background.RUN_MODE:
				number.transform.position = Camera.main.WorldToScreenPoint(transform.localPosition);
				if(Input.GetKeyDown(KeyCode.R)){
					GetReady();
				}
				else if(dead > 0){
					Killed();
				}
				else{
					BoolsCheck();
					SpeedCalculate();
				}
				break;
			
			case Background.EDIT_MODE:
				transform.position = new Vector3(tempPos.x, tempPos.y, transform.position.z);
				number.transform.position = Camera.main.WorldToScreenPoint(transform.localPosition);
				break;
			
			default:
				break;
		}
    }

    void SpeedCalculate(){
		if(touchingGround || touchingWall){
			doubleJump = true;
		}
		if(wallJumpTime > 0){
			wallJumpTime -= Time.deltaTime;
			speed.y = jumpForce;
			speed.x = xSpeedMax * moveDirection;
		}
		else if(Input.GetKeyDown(KeyCode.K))
			TryJump();
		else if(onWall && speed.y <= -sliding){
			speed.x = 0;
			speed.y = -sliding;
		}
		else{
			bool press = false;
			if(Input.GetKey(KeyCode.A)){
				facingRight = false;
				moveDirection = -1;
				if(touchingWall && speed.x < 0){
					speed.x = 0;
				}
				speed.x -= Time.deltaTime * speedx;
				if(speed.x < -xSpeedMax)
					speed.x = -xSpeedMax;
				press = true;
			}
			if(Input.GetKey(KeyCode.D)){
				facingRight = true;
				moveDirection = 1;
				if(touchingWall && speed.x > 0){
					speed.x = 0;
				}
				speed.x += Time.deltaTime * speedx;
				if(speed.x > xSpeedMax)
					speed.x = xSpeedMax;
				press = true;
			}
			if(!press){
				if(Mathf.Abs(speed.x) > 1)
					speed.x -= Time.deltaTime * speedx * fric * (speed.x > 0 ? 1 : -1);
				else
					speed.x /= 2;
			}
			if(touchingGround && speed.y < 0)
				speed.y = 0;
			else{
				speed.y -= gravity * Time.deltaTime;
				if(speed.y < ySpeedMax)
					speed.y = ySpeedMax;
				
			}
		}
		if(touchingTop && speed.y > 0)
			speed.y = 0;
		rb.velocity = new Vector3(speed.x, speed.y, 0);
    }

    void BoolsCheck(){
		touchingGround = Physics2D.OverlapCircle(groundCheck1.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, whatIsGround);
		touchingTop = Physics2D.OverlapCircle(groundCheck5.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck6.position, checkRadius, whatIsGround);
		touchingWall = Physics2D.OverlapCircle(groundCheck3.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheck4.position, checkRadius, whatIsGround);
		onWall = ((Input.GetKey(KeyCode.D) && moveDirection > 0) || (Input.GetKey(KeyCode.A) && moveDirection < 0)) && (touchingWall) && (!touchingGround);  //靠墙，前进且不落地
		notFlying = touchingGround || onWall || touchingTop;
    }
   
    void TryJump(){
		if(onWall){
			wallJumpTime = wallJumpTimeMax;
			moveDirection *= -1;
			doubleJump = true;
			speed.x = moveDirection * xSpeedMax;
			speed.y = jumpForce;
		}
		else if(touchingGround || touchingWall){
			speed.y = jumpForce;
			doubleJump = true;
		}
		else if(doubleJump){
			speed.y = jumpForce * 0.8f;
			doubleJump = false;
		}
    }

	public void GetReady(){
		Pos tempPos = Pos.Copy(GameObject.Find("background").GetComponent<Background>().GetRoomNow().GetComponent<Room>().start);
		transform.position = new Vector3(tempPos.x, tempPos.y, transform.position.z);					
		rb.velocity = new Vector2(0, 0);
		speed.x = 0;
		speed.y = 0;
		collider.enabled = true;
		gravity = 7;
		dead = 0;
		deadTime = -1;
		doubleJump = true;
		SetNum(1);
		status = Background.RUN_MODE;
		Debug.Log(status);
		BoolsCheck();
		GameObject.Find("background/hero/eyes").GetComponent<Eyes>().ReceiveMessage("alive");
		GameObject.Find("background").GetComponent<Background>().GetRoomNow().GetComponent<Room>().Renew();
	}

	public void Killed(){
		if(dead == 0){
			rb.velocity = new Vector2(0, 0);
			speed.x = 0;
			speed.y = 0;
			dead = 1;
			deadTime = deadWaitTime;
			deadPos = new Pos();
			deadPos.x = transform.localPosition.x;
			deadPos.y = transform.localPosition.y;
			collider.enabled = false;
			gravity = 0;
			notFlying = false;
			GameObject.Find("background/hero/eyes").GetComponent<Eyes>().ReceiveMessage("dead");
		}
		else if(dead == 1 && deadTime > 0){
			deadTime -= Time.deltaTime;
		}
		else if(dead == 1 && deadTime <= 0){
			dead = 2;
			deadTime = deadMoveTime;
		}
		else if(dead == 2 && deadTime > 0){
			deadTime -= Time.deltaTime;
			if(deadTime < 0)
				deadTime = 0;
			Vector3 tempPos = transform.localPosition;
			Pos tempPosStart = GameObject.Find("background").GetComponent<Background>().GetRoomNow().GetComponent<Room>().start;
			tempPos.x = tempPosStart.x + (deadPos.x - tempPosStart.x) * deadTime / deadMoveTime;
			tempPos.y = tempPosStart.y + (deadPos.y - tempPosStart.y) * deadTime / deadMoveTime;
			transform.localPosition = tempPos;
			number.transform.position = Camera.main.WorldToScreenPoint(transform.localPosition);
		}
		else if(dead == 2 && deadTime <= 0){
			dead = 3;
			deadTime = deadWaitTime;
		}
		else if(dead == 3 && deadTime > 0){
			deadTime -= Time.deltaTime;
		}
		else{
			GetReady();
		}
	}

	public void SetNum(int num){	
		if(num > 9 || num <= 0){
			Debug.Log("error");
			return ;
		}
		this.num = num;
		number.GetComponent<TMP_Text>().text = num.ToString();
	}

	public int GetNum(){	
		return num;
	}
}
