using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float fric;

    Vector3 ResetPosition;

    void Awake(){
        ResetPosition = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate(){
        Vector3 speed = rb.velocity;
        if(speed.x != 0)
            speed.x -= speed.x * fric * Time.deltaTime;
        rb.velocity = speed;
    }

    public void Reset(){
        transform.position = ResetPosition;
        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Numbers>().Reset();
    }
}
