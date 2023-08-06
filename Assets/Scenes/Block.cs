using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Block : MonoBehaviour
{
    public Sprite[] sprite;
    int type;
    public GameObject number;
    public int num = 0;
    bool inited = false;
	public Pos pos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       	if(inited){
			switch(type){
				case 0:
					TryKill();
					break;
				
				default:
					break;
					
			}
		}
    }

    public void Init(Pos pos, int type, GameObject number){
		this.pos = pos;
		this.type = type;
		this.number = number;
		number.transform.parent = GameObject.Find("Canvas").transform;
		number.GetComponent<TMP_Text>().text = "";
		number.transform.position = Camera.main.WorldToScreenPoint(transform.localPosition);
		transform.GetComponent<SpriteRenderer>().sprite = sprite[type];
		transform.GetComponent<BoxCollider2D>().enabled = true;
		inited = true;
		switch(type){
			case 0:
				transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transform.GetComponent<SpriteRenderer>().color.a);
				break;

			case 1:
				ColorChange();
				break;

			case 2:
				transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transform.GetComponent<SpriteRenderer>().color.a);
				break;
		}
		number.transform.localScale = new Vector3(1f, 1f, 1);	//不加这行导出的block的数字也会小一半，而且只有导出的有
		if(type == 1){
		 	transform.GetComponent<BoxCollider2D>().isTrigger = true;

			Vector3 temp = transform.localScale;
			temp.x /= 2;
			temp.y /= 2;
			temp.z /= 2;
			number.transform.localScale = new Vector3(0.5f, 0.5f, 1);

			gameObject.layer = LayerMask.NameToLayer("Default");
		}
    }

    void TryKill(){
		if(HeroInAttack() && num == GameObject.Find("background/hero").GetComponent<Hero>().GetNum() && GameObject.Find("background/hero").GetComponent<Hero>().dead == 0){
			Debug.Log("kill");
			GameObject.Find("background/hero").GetComponent<Hero>().Killed();
		}
    }


    bool HeroInAttack(){
		Vector3 temppos = GameObject.Find("background/hero").transform.localPosition;
		Vector3 pos = transform.localPosition;
		if(GameObject.Find("background/hero").GetComponent<Hero>().notFlying){
			if(Mathf.Abs(temppos.x - pos.x) < 0.5f * Background.BLOCKLEN)
				return true;
			if(Mathf.Abs(temppos.y - pos.y) < 0.5f * Background.BLOCKLEN)
				return true;
			if(Mathf.Abs(temppos.x - pos.x) < Background.BLOCKLEN && Mathf.Abs(temppos.y - pos.y) < Background.BLOCKLEN)
				return true;
		}
		return false;
    }

	public int GetType(){
		return type;
	}

	public void SetPos(Pos pos){
		this.pos = pos;
		transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
	}

	public void Hide(){
		transform.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void Show(){
		transform.GetComponent<SpriteRenderer>().enabled = true;
	}

	void ColorChange(){
		if(type != 1)
			return ;
		if(num >= 0)
			transform.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
		else	
			transform.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
	}

	public void SetNum(int num = -1){
		if(num == -1){
			ShowNum(-1);
			ColorChange();
			return;
		}

		if(this.num != num || type != 1)
			this.num = num;
		else
			this.num = -num;
		ShowNum();
		ColorChange();
	}

	public void ShowNum(int num = -1){
		if(this.num == 0)
			number.GetComponent<TMP_Text>().text = "";
		else if(num == -1)
			number.GetComponent<TMP_Text>().text = Mathf.Abs(this.num).ToString();
		else 
			number.GetComponent<TMP_Text>().text = Mathf.Abs(num).ToString();
	}

	public void SetAlpha(float alpha){
		Color color = transform.GetComponent<SpriteRenderer>().color;
		color.a = alpha;
		transform.GetComponent<SpriteRenderer>().color = color;
	}

	public void Renew(Pos pos, int type){
		this.pos = pos;
		this.type = type;
		transform.GetComponent<SpriteRenderer>().sprite = sprite[type];
		inited = true;
		switch(type){
			case 0:
				transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transform.GetComponent<SpriteRenderer>().color.a);
				break;

			case 1:
				ColorChange();
				break;

			case 2:
				transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, transform.GetComponent<SpriteRenderer>().color.a);
				break;
		}

		{
			Vector3 temp = transform.localScale;
			temp.x /= 2;
			temp.y /= 2;
			temp.z /= 2;
			number.transform.localScale = temp;
		}
	}

	void OnTriggerEnter2D(Collider2D hero)
    {
        try{
			hero.GetComponent<Hero>().SetNum(hero.GetComponent<Hero>().num + num);
			transform.GetComponent<SpriteRenderer>().enabled = false;
			transform.GetComponent<Collider2D>().enabled = false;
			number.GetComponent<TMP_Text>().text = "";
		}
		catch(Exception e){
			Debug.Log(e);
		}
	}

}
