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
    public Transform player;


    private void Update()
    {
        coolDownCounter -= Time.deltaTime;
    }

    private void Start()
    {
        player = FindObjectOfType<Camera>().transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (0 < coolDownCounter) // Because after we hit the trigger it hits it multiple times, so we need a short cooldown until the object exited
        {
            return;
        }

        if (other.CompareTag("Paper"))
        {
            CollisionDisable(other);
            Vector3 direction = (player.position - transform.position).normalized;
            Paper paper = other.gameObject.transform.parent.GetComponent<Paper>();
            paper.GetComponent<Rigidbody>().AddForce(Vector3.up * paperForcePush * Time.deltaTime, ForceMode.Impulse);
            paper.GetComponent<Rigidbody>().AddForce(direction * paperForcePush * Time.deltaTime, ForceMode.Impulse);
            paper.OpenPaper();
            if (paper.wasOpened)
            {
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
        //Physics.IgnoreCollision(other, transform.parent.GetComponent<Collider>(), false);
        Physics.IgnoreLayerCollision(10, 11, false);
    }

    private void CollisionDisable(Collider other)
    {
        Debug.Log("?!?");
        //Physics.IgnoreCollision(other, transform.parent.GetComponent<Collider>());
        Physics.IgnoreLayerCollision(10, 11, true);
    }

   
}
