using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookthecam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(Camera.main.transform.localPosition);
        Vector3 templook = transform.eulerAngles;
        templook.x = 0;
        templook.z = 0;
        transform.eulerAngles = templook;
    }
}
