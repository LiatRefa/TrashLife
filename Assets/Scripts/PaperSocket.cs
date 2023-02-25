using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaperSocket : MonoBehaviour
{
    private bool _paperInsterted;

    public bool PaperInserted => _paperInsterted;

    public void InsertPaper(SelectEnterEventArgs args)
    {
        _paperInsterted = true;
    }
    
    public void RemovePaper(SelectExitEventArgs args)
    {
        _paperInsterted = false;
    }
}