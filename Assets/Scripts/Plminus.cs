using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Plminus : MonoBehaviour
{
    [SerializeField] char type;
    [SerializeField] int num;
    bool able;
    // Start is called before the first frame update
    void Start()
    {
        able = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D box)
    {
        if(able)
            try{
                //Debug.Log(box.name);
                box.gameObject.GetComponent<Numbers>().cal(type, num);
                Hide();
                able = false;
                GameObject.Find("hero").GetComponent<GameController>().ColliderCheck();
            }
            catch(Exception e){
                Debug.Log(e); 
            }
	}

    private void Hide(){
        foreach(Transform child in transform){
            child.GetComponent<SpriteRenderer>().enabled = false;
        }
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Show(){
        foreach(Transform child in transform){
            child.GetComponent<SpriteRenderer>().enabled = true;
        }
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Reset(){
        Show();
        able = true;
    }
    
}
