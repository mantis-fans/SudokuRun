using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class Room : MonoBehaviour
{
    int ID;
    public GameObject[] block;
	public int block_count;
    int block_max = 1000;
    public Pos start;
	public GameObject subBlock;
	public GameObject preBlock;
	public GameObject num_obj;

    void Start()
    {
        Init();
    }

    void Update()
    {
    }

    public void Init(){
		block = new GameObject[block_max];
		start = Pos.Copy(new Vector2(-9999, -9999));
    }

    public void CreateBlock(Pos pos){
		int type = subBlock.GetComponent<Block>().GetType();
		if(BlockHere(pos) != -1)
			return ;
		else{
			AddBlock(pos, type);
		}
    }

    public void DeleteBlock(Pos pos, int index){
		if(block[index].GetComponent<Block>().GetType() == 2)
			start = Pos.Copy(new Vector2(-9999, -9999));
		else{
			Destroy(block[index].GetComponent<Block>().number);
			Destroy(block[index]);
			for(int j=index; j<block_count - 1; j++){
				block[j] = block[j+1];
				block[j].name = "block" + j;
			}
			block[block_count - 1] = null;
			block_count--;
		}
    }


    public int BlockHere(Pos pos){
		if(pos.x == start.x && pos.y == start.y)
			return -2;
		for(int i=0; i<block_count; i++){
			if(block[i].GetComponent<Block>().pos.x == pos.x && block[i].GetComponent<Block>().pos.y == pos.y)
				return i;
		}
		return -1;
    }

    public void AddBlock(Pos pos, int type){
		if(type == 2)
			start = pos;
		else{
			GameObject clone = Instantiate(subBlock, subBlock.transform.position, subBlock.transform.rotation);
			GameObject number = Instantiate(num_obj, subBlock.transform.position, subBlock.transform.rotation);
			clone.GetComponent<Block>().Init(pos, type, number);
			clone.GetComponent<Block>().SetAlpha(1);
			clone.transform.localScale = subBlock.transform.localScale;
			clone.transform.parent = transform;
			clone.name = "block" + block_count;
			block[block_count] = clone;
			block_count++;
		}	
    }

	public void Renew(){
		for(int i=0; i<block_count; i++){
			block[i].GetComponent<SpriteRenderer>().enabled = true;
			block[i].GetComponent<Collider2D>().enabled = true;
			block[i].GetComponent<Block>().SetNum();
		}
	}

	public void SetNum(int select, int num){
		if(select != -2)
			block[select].GetComponent<Block>().SetNum(num);
		else
			GameObject.Find("background/hero").GetComponent<Hero>().SetNum(num);
	}
}
