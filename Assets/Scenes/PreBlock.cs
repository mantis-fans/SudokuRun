using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class PreBlock : MonoBehaviour
{
    public GameObject subBlock;
    public int num;
    public int type = 0;
    int typeMax = 3;

    public SpriteRenderer render;
    public Sprite[] spriteList;
    public Vector3[] size;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        size = new Vector3[10];

        transform.position = new Vector3(-11, 5, 0);

        SetType(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0 && GameObject.Find("background").GetComponent<Background>().GetStatus() == Background.EDIT_MODE)
        {	
            type += -1 * (Input.GetAxis("Mouse ScrollWheel") > 0 ? 1 : -1);
            if(type == 3)
                type = 0;
            if(type < 0)
                type = 2;
            subBlock.GetComponent<Block>().Renew(subBlock.GetComponent<Block>().pos, type);
            subBlock.GetComponent<Block>().SetAlpha(0.37f);
			
            SetType(type);
        }
        switch(type){
            case 0:
                transform.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1);
                break;
            
            case 1:
                transform.GetComponent<SpriteRenderer>().color = new Color (0, 1, 0, 1);
                break;

            default:
                break;
        }

    }

    public int GetType(){
        return type;
    }

    void SetType(int type){
        this.type = type;
        render.sprite = spriteList[type];
    }
}
