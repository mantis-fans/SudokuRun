using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Room_Input : MonoBehaviour
{
    public int index, temp;
    public GameObject back;
    public bool flag = false;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<TMP_InputField>().onEndEdit.AddListener (End_Value);
        void End_Value(string inp){
            if(inp != ""){
                temp = int.Parse(inp);
                if(temp > 0 && temp <= back.GetComponent<Background>().room_max)
                    index = temp;
                else   
                    transform.GetComponent<TMP_InputField>().text = index.ToString();
            }
            else{
                index = back.GetComponent<Background>().room_now;
                transform.GetComponent<TMP_InputField>().text = index.ToString();
            }

            flag = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
            transform.GetComponent<TMP_InputField>().text = back.GetComponent<Background>().room_now.ToString();
    }
}
