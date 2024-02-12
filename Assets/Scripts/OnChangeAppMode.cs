using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeAppMode : MonoBehaviour
{
    [SerializeField] private AppModeTrigger _appModeTrigger;
    public static event Action<AppMode> OnChangeAppModeAction;

    private MeshRenderer _meshRenderer;

    public MeshRenderer meshRenderer => _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AppObjTrigger"))
        {
            OnChangeAppModeAction?.Invoke(_appModeTrigger.state);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AppObjTrigger"))
        {
            OnChangeAppModeAction?.Invoke(AppMode.None);
        }
    }
}
