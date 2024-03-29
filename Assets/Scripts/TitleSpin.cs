using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpin : MonoBehaviour
{
    float yAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yAngle = transform.rotation.y;
        yAngle += 360f*Time.deltaTime;
        transform.Rotate(0, yAngle, 0, Space.World);

    }
}
