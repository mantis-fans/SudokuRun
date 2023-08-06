// using System.Collections;
// using System.Collections.Generic;
// using System.Text;
// using System.IO;
// using UnityEngine;
// using TMPro;
// using System.Text.RegularExpressions;
// using System;

// public class Body : MonoBehaviour
// {
//     public GameObject hero;
//     private bool isDoubleJump;
//     private bool rota;
//     private bool facingRight;
//     private bool isRight;

//     public Vector3 killAngle;

//     public void Killed(){
//         killAngle = transform.localEulerAngles;
//         killAngle.z %= 90;
//     }
//     // Start is called before the first frame update
//     void Start()
//     {
//         rota = false;
//         isDoubleJump = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         transform.position = hero.transform.position;
//         if(hero.GetComponent<Hero>().alive){
//             isDoubleJump = hero.GetComponent<Hero>().doubleJump;
//             facingRight = hero.GetComponent<Hero>().facingRight;
//             if(!isDoubleJump){  //使用了二段跳
//                 if(!rota)   //只记录一次
//                     isRight = facingRight;
//                 rota = true;
//             }
//             if(rota){
//                 if(isRight)    
//                     transform.Rotate(0, 0, -2f);
//                 else
//                     transform.Rotate(0, 0, 2f);
//                 if(isDoubleJump){ //碰墙
//                     transform.rotation = Quaternion.Euler(0, 0, 0);
//                     rota = false;
//                 }
//             }
//         }
//         else if(hero.GetComponent<Hero>().killWaitTime < 0.2f && hero.GetComponent<Hero>().killMoveTime > 0){
//             transform.rotation = Quaternion.Euler(killAngle * hero.GetComponent<Hero>().killMoveTime / 1f);
//         }

//     }
// }
