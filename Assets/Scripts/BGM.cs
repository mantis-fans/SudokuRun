using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    //public AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1854, 946, false);
        DontDestroyOnLoad(this.gameObject);
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
