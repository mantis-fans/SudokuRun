using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;    

public class CommonButton : MonoBehaviour
{
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener (Press);
        void Press(){
            flag = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
