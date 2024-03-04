using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInOutLightAnimation : MonoBehaviour
{
    [SerializeField] private float fadeInMaxIntensity = 64f;
    [SerializeField] private float fadeOutMinIntensity = 0f;
    [SerializeField] private float fadeInDuration = 10f;
    [SerializeField] private float fadeOutDuration = 3f;

    [SerializeField] private bool startFadedOut = true;
    [SerializeField] private bool disableLightOnFadeOut = true;

    private Light _light;

    private void Awake()
    {
        _light = GetComponentInChildren<Light>();

        if (startFadedOut)
        {
            _light.intensity = fadeOutMinIntensity;
            DisableLight();    
        }
        
    }

    public void FadeInLight()
    {
        if (!_light)
        {
            return;
        }

        // it means I should enable it before
        if (disableLightOnFadeOut)
        {
            _light.gameObject.SetActive(true);
        }
        
        _light.DOIntensity(fadeInMaxIntensity, fadeInDuration);
    }
    public void FadeOutLight()
    {
        if (!_light)
        {
            return;
        }

        _light.DOIntensity(fadeOutMinIntensity, fadeOutDuration).OnComplete(DisableLight);
    }

    private void DisableLight()
    {
        if (disableLightOnFadeOut)
        {
            _light.gameObject.SetActive(false);
        }
    }

}
