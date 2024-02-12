using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class AppModeProcessor : MonoBehaviour
{
    // TODO: Debug only
    [SerializeField]
    private AppMode startingMode;
    
    private AppMode _currentMode;

    [SerializeField] 
    private List<AppModeTrigger> triggerObjList;

    private UDictionary<AppMode, AppModeTrigger> _objectDict = new UDictionary<AppMode, AppModeTrigger>();

    [SerializeField] private SnapInteractor interactor;

    private void Awake()
    {
        foreach (AppModeTrigger trigger in triggerObjList)
        {
            _objectDict.Add(trigger.state, trigger);
        }
    }

    private void OnEnable()
    {
        OnChangeAppMode.OnChangeAppModeAction += SetCurrentMode;
    }
    private void OnDisable()
    {
        OnChangeAppMode.OnChangeAppModeAction -= SetCurrentMode;
    }

    // Start is called before the first frame update
    void Start()
    {
        startingMode = AppMode.Normal;
        _currentMode = AppMode.Normal;

        AppModeTrigger newAppModeTrigger = _objectDict[_currentMode];
        if (newAppModeTrigger != null)
        {
            // set new timeout snap place - after the timeout it will do its snap magic
            interactor.InjectOptionalTimeOutInteractable(newAppModeTrigger.interactable);
            
        }
    }

    private void SetCurrentMode(AppMode newMode)
    {
        _currentMode = newMode;
    }

    // Method to be used by Unity Event Wrappers when DeSelecting
    public void HasAppStateChanged()
    {
        if (startingMode != _currentMode)
        {
            if (_currentMode != AppMode.None)
            {
                AppModeTrigger newAppModeTrigger = _objectDict[_currentMode];

                // set new timeout snap place - after the timeout it will do its snap magic
                interactor.InjectOptionalTimeOutInteractable(newAppModeTrigger.interactable);
                
                // startingMode changes - to the new timeout interactable
                startingMode = _currentMode;
                
                // trigger Bezi animations
                if (newAppModeTrigger.beziAnimations != null)
                {
                    newAppModeTrigger.beziAnimations.OnTriggerEvent();
                }
                
                // toggle app mode snap areas
                foreach (AppModeTrigger trigger in triggerObjList)
                {
                    trigger.ToggleSnapAreaIfAppStateMatches(_currentMode);
                }
            }
            else
            {
                // reset current mode to original value (precaution)
                _currentMode = startingMode;
            }
        }
        // else is not needed because the timeout snap thing does its magic
    }

    public void ShowEnabledSnapAreas()
    {
        foreach (AppModeTrigger trigger in triggerObjList)
        {
            trigger.EnableSnapAreaMeshRenderer();
        }
    }
    
    public void HideEnabledSnapAreas()
    {
        foreach (AppModeTrigger trigger in triggerObjList)
        {
            trigger.DisableSnapAreaMeshRenderer();
        }
    }
}

public enum AppMode
{
    Normal,
    Edit,
    None
};
