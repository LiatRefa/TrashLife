using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smaller : MonoBehaviour
{
    //public GameObject player;
    public float scaleFactor = 0.2f;


    private void Update()
    {
    }
    public void ChangeSize(bool small)
    {
        //Transform playerTransform = player.GetComponent<Transform>();
        //playerTransform.localScale *= scaleFactor;
        GameObject player = GameObject.FindWithTag("Player");
        if (small)
        {
            player.transform.localScale *= scaleFactor;
        }
        else
        {
            player.transform.localScale = Vector3.one;
        }

    }



}
