using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    public Transform cam;

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.position);
        transform.rotation = cam.transform.rotation;
    }
}