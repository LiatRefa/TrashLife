using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashTrigger : MonoBehaviour
{
    public float paperForcePush = 10f;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper"))
        {
            Paper paper = other.gameObject.transform.parent.GetComponent<Paper>();
            paper.GetComponent<Rigidbody>().AddForce(-Camera.main.gameObject.transform.forward * paperForcePush);
            paper.OpenPaper();
        }
    }
    
}
