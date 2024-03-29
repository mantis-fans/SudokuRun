using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener (Press);
        void Press(){
            Debug.Log("Start");
            GameController.instance.nextLevel(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick(){
        Debug.Log("Start");
        GameController.instance.nextLevel(); 
    }
}
