using System;
using System.Collections;
using System.Collections.Generic;
using Utils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Paper : MonoBehaviour
{
   [SerializeField] private Renderer paperRenderer;
   [SerializeField] private GameObject openedPaper;
   [SerializeField] private GameObject closedPaper;
   private Texture paperTxt;
   public InteractionLayerMask paperLayerMask;
   public InteractionLayerMask openedPaperLayerMask;

   public bool wasOpened;

   public void Start()
   {
      closedPaper.SetActive(true);
      openedPaper.SetActive(false);
      //paperTxt = paperRenderer.material.GetTexture("_MainTex");
      GetComponent<XRGrabInteractable>().interactionLayers = paperLayerMask;
      
   }

   public void OpenPaper()
   {
        // Todo: Add paper open sound
      wasOpened = true;
      closedPaper.SetActive(false);
      openedPaper.SetActive(true);
      //openedPaper.GetComponent<Renderer>().material.SetTexture("_MainTex", paperTxt);
      GetComponent<XRGrabInteractable>().interactionLayers = openedPaperLayerMask;
   }
}
