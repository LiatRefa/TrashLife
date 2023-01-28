using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashTrigger : MonoBehaviour
{
    public float paperForcePush = 100F;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            Paper paper = other.GetComponent<Paper>();
            paper.OpenPaper();
            paper.GetComponent<Rigidbody>().AddForce(-Camera.main.gameObject.transform.forward * paperForcePush);
        }
    }
    
}
