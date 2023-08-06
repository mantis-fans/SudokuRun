using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public struct Gateinfo
{
    public int start, end;
    public int direction;
}

public struct Wallinfo
{
    public int room;
    public bool l,r,u,d;
}

public class Line : MonoBehaviour
{
    public float width = 12.3f, height = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
