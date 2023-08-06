using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Choose : MonoBehaviour
{
    public Sprite Sprite1, Sprite2;
    public SpriteRenderer render;
    public int mode = 0;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        // GetComponent<Button>().onClick.AddListener (Press);
        // void Press(){
        //     Debug.Log("1");
        //     if(mode == 0){
        //         mode = 1;
        //         render.sprite = Sprite2;
        //     }
        //     else{
        //         mode = 0;
        //         render.sprite = Sprite1;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click2Choose(){
        Debug.Log("1");
        if(mode == 0){
            mode = 1;
            render.sprite = Sprite2;
        }
        else{
            mode = 0;
            render.sprite = Sprite1;
        }
    }
}
