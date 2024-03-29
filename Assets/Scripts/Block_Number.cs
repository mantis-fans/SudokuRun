using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Number : MonoBehaviour
{
    private GameObject Hidewall1, Hidewall2;
    // Start is called before the first frame update
    void Start()
    {
        Hidewall1 = transform.Find("wall1").gameObject;
        Hidewall2 = transform.Find("wall2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameController.Contact(GameObject.Find("hero"), gameObject)){
            
            //search();
            Hidewall1.transform.GetComponent<SpriteRenderer>().enabled = true;
            Hidewall2.transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        else{
            Hidewall1.transform.GetComponent<SpriteRenderer>().enabled = false;
            Hidewall2.transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    
    private void search(){
        Ray ray1 = new Ray(transform.position, new Vector3(-1, 0, 0));
        Ray ray2 = new Ray(transform.position, new Vector3(1, 0, 0));
        RaycastHit hitInfo1, hitInfo2;
        if (Physics.Raycast(ray1, out hitInfo1) && Physics.Raycast(ray2, out hitInfo2))
        {
            Hidewall1.transform.localScale = new Vector3(hitInfo2.transform.localPosition.x - hitInfo1.transform.localPosition.x, Hidewall1.transform.localScale.y, Hidewall1.transform.localScale.z);
            Hidewall1.transform.position = new Vector3((hitInfo2.transform.localPosition.x + hitInfo1.transform.localPosition.x) / 2, Hidewall1.transform.position.y, Hidewall1.transform.position.z);
        }

        Ray ray3 = new Ray(transform.position, new Vector3(0, -1, 0));
        Ray ray4 = new Ray(transform.position, new Vector3(0, 1, 0));
        RaycastHit hitInfo3, hitInfo4;
        if (Physics.Raycast(ray3, out hitInfo3) && Physics.Raycast(ray4, out hitInfo4))
        {
            Hidewall1.transform.localScale = new Vector3(Hidewall1.transform.localScale.x, hitInfo4.transform.localPosition.y - hitInfo3.transform.localPosition.y, Hidewall1.transform.localScale.z);
            Hidewall1.transform.position = new Vector3(Hidewall1.transform.position.x, (hitInfo4.transform.localPosition.y + hitInfo3.transform.localPosition.y) / 2, Hidewall1.transform.position.z);
        }
        Hidewall1.transform.GetComponent<SpriteRenderer>().enabled = true;
        Hidewall2.transform.GetComponent<SpriteRenderer>().enabled = true;
        //Hidewall1.SetActive(true);
        //Hidewall2.SetActive(true);
    }

}
