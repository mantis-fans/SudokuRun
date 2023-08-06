using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite dead, alive;
    
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("background/hero").GetComponent<Hero>().facingRight)
            transform.localScale = new Vector3(1, 1, 1);
        else{
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ReceiveMessage(string message){
        switch(message){
            case "dead":
                transform.GetComponent<SpriteRenderer>().sprite = dead;
                break;

            case "alive":
                transform.GetComponent<SpriteRenderer>().sprite = alive;
                break;

            default:
                break;
        }
    }
}
