using System;
using System.Collections;
using System.Collections.Generic;
using Utils;
using UnityEngine;

public class Paper : MonoBehaviour
{
   [SerializeField] private Renderer paperRenderer;
   [SerializeField] private GameObject openedPaper;
   [SerializeField] private GameObject closedPaper;
   private Texture paperTxt;

   public void Start()
   {
      closedPaper.SetActive(true);
      openedPaper.SetActive(false);
      paperTxt = paperRenderer.material.GetTexture("_MainTex");
   }

   public void OpenPaper()
   {
      // Todo: Add paper open sound
      closedPaper.SetActive(false);
      openedPaper.SetActive(true);
      openedPaper.GetComponent<Renderer>().material.SetTexture("_MainTex", paperTxt);
      List<Texture> lst = new List<Texture>();
   }
}
