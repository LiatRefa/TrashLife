using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using InteractableItems;

public class TrashTrigger : MonoBehaviour
{
    public float paperForcePush = 100f;
    private float coolDownTime = 120f;
    private float coolDownCounter;


    private void Update()
    {
        coolDownCounter -= Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (0 < coolDownCounter) // Because after we hit the trigger it hits it multiple times, so we need a short cooldown until the object exited
        {
            return;
        }

        Debug.Log("Entered");
        if (other.CompareTag("Paper"))
        {
            Debug.Log("Papaer Entered");
            CollisionDisable(other);
            Paper paper = other.gameObject.transform.parent.GetComponent<Paper>();
            paper.GetComponent<Rigidbody>().AddForce(-Camera.main.gameObject.transform.forward * paperForcePush);
            paper.OpenPaper();
            if (paper.wasOpened)
            {
                Debug.Log("OPENED?");
                LevelManager.instance.curr_level++;
                LevelManager.instance.LevelSetup();
            }
            
            StartCoroutine(CollisionEnable(other));
            coolDownCounter = coolDownTime;

            
        }

        else
        {
            other.GetComponent<TrashEventTrigger>()?.Trigger();
            coolDownCounter = coolDownTime;

        }
    }
    

    private IEnumerator CollisionEnable(Collider other)
    {
        yield return new WaitForSeconds(2);
        Physics.IgnoreCollision(other, transform.parent.GetComponent<Collider>(), false);
    }

    private void CollisionDisable(Collider other)
    {
        Physics.IgnoreCollision(other, transform.parent.GetComponent<Collider>());
    }

   
}
