using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;


public class Pos{
	public float x, y;

	public static Pos Copy (Pos p){
		Pos temp = new Pos();
		temp.x = p.x;
		temp.y = p.y;
		return temp;
	}

	public static Pos Copy (Vector2 v){
		Pos temp = new Pos();
		temp.x = v.x;
		temp.y = v.y;
		return temp;
	}

	public void Out(){
		Debug.Log(x + " " + y);
	}
};

public class Background : MonoBehaviour
{
    public const int EDIT_MODE = 1;
	public const int RUN_MODE = 2;

	public const int BLOCKLEN = 1;

	public GameObject[] room;	
    public int room_max = 100;
    public GameObject subBlock;
	public GameObject preBlock;
    public int room_now = 1;
    public int block_select;
	bool buttonClick;
    public Vector3 mouse;
	int status;

	void Awake()
    {
        Screen.SetResolution(1854, 946, false);
    }

    // Start is called before the first frame update
    void Start()
    {
		status = EDIT_MODE;
		
		{
			GameObject temproom = room[0];
			room = new GameObject[room_max];
			room[0] = temproom;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.C)){
			ChangeStatus();
		}
		mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		switch(status){
			case (EDIT_MODE):
				Edit_Mode();
				break;
			case (RUN_MODE):
				Run_Mode();
				break;
			default:
				break;
		}
    }

    void Edit_Mode(){
		Pos mousePos;
		mousePos = MouseBlock();
		if((block_select = room[room_now].GetComponent<Room>().BlockHere(mousePos)) != -1){	//定位鼠标格子
			subBlock.GetComponent<Block>().Hide();
			int num;
			if((num = ListenInputNumber()) != -1)
				room[room_now].GetComponent<Room>().SetNum(block_select, num);
		}
		else{
			subBlock.GetComponent<Block>().Show();
			subBlock.GetComponent<Block>().SetPos(mousePos);

		}

		
		if(Input.GetMouseButtonDown(0)){
			if(block_select == -1 && !buttonClick)	//排除按到UI按钮的情况
				room[room_now].GetComponent<Room>().CreateBlock(subBlock.GetComponent<Block>().pos);
		}

		if(Input.GetMouseButtonDown(1)){
			if(block_select != -1 && !buttonClick)	//排除按到UI按钮的情况
				room[room_now].GetComponent<Room>().DeleteBlock(subBlock.GetComponent<Block>().pos, block_select);
		}
    }    

	void Run_Mode(){
		
	}

	public int GetStatus(){
		return status;
	}

    void ChangeStatus(){
		if(status == EDIT_MODE){
			status = RUN_MODE;
			GameObject.Find("background/hero").GetComponent<Hero>().GetReady();
			subBlock.GetComponent<SpriteRenderer>().enabled = false;
			preBlock.GetComponent<SpriteRenderer>().enabled = false;
		}
		else{
			status = EDIT_MODE;
			GameObject.Find("background/hero").GetComponent<Hero>().GetReady();
			subBlock.GetComponent<SpriteRenderer>().enabled = true;
			preBlock.GetComponent<SpriteRenderer>().enabled = true;
			room[room_now].GetComponent<Room>().Renew();
		}
	}

	Pos MouseBlock(){
		Pos pos = new Pos();
		pos.x = Mathf.FloorToInt(mouse.x / BLOCKLEN + 0.5f);
		pos.y = Mathf.FloorToInt(mouse.y / BLOCKLEN + 0.5f);
		if(Input.GetKeyDown(KeyCode.Q))
			Debug.Log(pos.x + " " + pos.y);
		return pos;
    }

	public GameObject GetRoomNow(){
		return room[room_now];
	}

	int ListenInputNumber()	//读取数字
    {
        char ch = ' ';
        if(Input.inputString != ""){
            ch = Input.inputString[0];
        }
    
        try
        {
            if(ch >= '0' && ch <= '9')
				return ch - 48;
			else 
				return -1;
        }
        catch(System.Exception e)
        {
            
        }
        
        
        return -1;
    }
}
