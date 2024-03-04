using System;
using JetBrains.Annotations;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class AppModeTrigger : MonoBehaviour
{
    [SerializeField] private AppMode _state;
    public AppMode state => _state;
    
    [SerializeField] private SnapInteractable _interactable;
    public SnapInteractable interactable => _interactable;

    [SerializeField] [CanBeNull] private CustomBeziTriggerAnimation _beziAnimations;
    [CanBeNull] public CustomBeziTriggerAnimation beziAnimations => _beziAnimations;

    [SerializeField] private OnChangeAppMode snapArea;
    public OnChangeAppMode thisSnapArea => snapArea;

    public void ToggleSnapAreaIfAppStateMatches(AppMode newState)
    {
        if (snapArea == null) return;
        GameObject obj = snapArea.gameObject;
        obj.SetActive(!obj.activeSelf);
    }

    public void EnableSnapAreaMeshRenderer()
    {
        if (snapArea != null && snapArea.meshRenderer != null)
        {
            snapArea.meshRenderer.enabled = true;
        }
    }
    public void DisableSnapAreaMeshRenderer()
    {
        if (snapArea != null && snapArea.meshRenderer != null)
        {
            snapArea.meshRenderer.enabled = false;
        }
    }
}