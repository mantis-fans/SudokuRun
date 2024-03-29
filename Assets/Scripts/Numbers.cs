using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine;
using System;
using System.Text;
using UnityEditor;

public class Numbers : MonoBehaviour
{
    [SerializeField] int num;
    [SerializeField] char type;

    int ResetNum;
    char ResetType;
    void Awake(){
        ResetNum = num;
        ResetType = type;
        SetNum(num);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNum(int num){
        num %= 9;
        if(num == 0)
            num = 9;
        this.num = num;
        string path = "./Assets/Nums/";
        Texture2D myTexture = LoadTexture(path + num.ToString() + ".png");
        if (myTexture != null)
        {
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f));
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    public int GetNum(){
        return num;
    }

    Texture2D LoadTexture(string path)
    {
        Texture2D texture = null;
        byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            texture.LoadImage(fileData);
        }
        else
        {
            Debug.LogError("Texture file does not exist at path: " + path);
        }

        return texture;
    }

    public void cal(char type, int num){
        //Debug.Log(gameObject.name + " " + type + " " + num);
        switch(type){
            case '+':
                this.num += num;
            break;
            
            case '-':
                this.num -= num;
            break;

            case '*':
                this.num *= num;
            break;
            
            case '/':
                this.num /= num;
            break;

            default:
            break;
        }
        //Debug.Log(this.num);
        SetNum(this.num);
            
    }

    public List<int> NumberCheck(){
        List<int> nums = new List<int>{};
        switch(type){
            case '+':
                nums.Add(num);
            break;

            case '/':
                for(int i=1; i<=9; i+=1){
                    //Debug.Log(num + " " + i);
                    if(num % i == 0)
                        nums.Add(i);
                }
            break;

            case '\\':
                for(int i=1; i<=9; i+=1)
                    if(i % num == 0)
                        nums.Add(i);
            break;

            default:
                nums.Add(num);
            break;
        }
        /*
        for(int i=1; i<=9; i+=1){
            bool add = false;
            
        }
        */
        // foreach(int i in nums)
        //     Debug.Log(name + " " + i);
        return nums;
    }

    public void Reset(){
        num = ResetNum;
        type = ResetType;
        SetNum(num);
    }
}
