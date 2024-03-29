using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBox : MonoBehaviour
{
    [SerializeField] bool contact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D box){
        //Debug.Log(box.gameObject.layer);
        if(box.name == "wall1" || box.name == "wall2"){
            if(GameController.Contact(transform.parent.gameObject, box.transform.parent.gameObject))
                contact = true;
        }
        else if(box.gameObject.layer == 6 || box.gameObject.layer == 9)//Ground
            contact = true;
    }

    public void SetContact(bool contact){
        this.contact = contact;
    }

    public bool isContact(){
        return contact;
    }
}
