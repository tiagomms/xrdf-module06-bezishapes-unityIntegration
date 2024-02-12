using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDock : MonoBehaviour
{
    [SerializeField] private bool openDock = false;
    [SerializeField] private CustomBeziTriggerAnimation openDockAnim;
    [SerializeField] private CustomBeziTriggerAnimation closeDockAnim;

    //TODO: set event manager for triggering dock animation
    public void ToggleDockStatus()
    {
        openDock = !openDock;
    }
    
    public void PerformToggleDock()
    {
        ToggleDockStatus();
        
        DockChange();
    }

    private void DockChange()
    {
        if (openDock)
        {
            openDockAnim.OnTriggerEvent();
        }
        else
        {
            closeDockAnim.OnTriggerEvent();
        }
    }
}
