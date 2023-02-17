using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
[SerializeField] public float speed;

void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 runDirection = transform.position - other.transform.position;
            GetComponent<Rigidbody>().AddForce(runDirection * speed);
        }
    }
}
