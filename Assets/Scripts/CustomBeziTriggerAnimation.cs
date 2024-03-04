using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bezi.Bridge;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Animation = Bezi.Bridge.Animation;
using State = Bezi.Bridge.State;

public class CustomBeziTriggerAnimation : MonoBehaviour 
{
    // MARK: list of bezi behaviors and interaction key strings - that will trigger animations
    [SerializeField] private string animationName;
    
    [SerializeField]
    private UDictionary<string, BeziBehavior> triggerBehaviors;
    
    [SerializeField]
    private List<GameObject> enableGameObjects;
    
    [SerializeField]
    private List<GameObject> disableGameObjects;

    [SerializeField] private UnityEvent additionalNonBeziEvents;


    public void OnTriggerEvent()
    {
        EnableGameObjects();

        List<float> listAnimationDurations = new List<float>();
        foreach (var keyValue in triggerBehaviors)
        {
            BeziBehavior behavior = keyValue.Value;

            Interaction interaction = behavior.GetInteraction(keyValue.Key);

            if (interaction != null)
            {
                foreach (KeyValuePair<string, Animation> interactionAnimation in interaction.animations)
                {
                    float totalAnimDuration =
                        PerformAnimation(behavior, interactionAnimation.Value, behavior.gameObject);
                    listAnimationDurations.Add(totalAnimDuration);
                }
            }
        }

        DisableGameObjectsAfterAllAnimations(listAnimationDurations.Max(u => u) + 0.2f);

        if (!additionalNonBeziEvents.IsUnityNull())
        {
            additionalNonBeziEvents.Invoke();
        }
    }

    private float PerformAnimation(BeziBehavior behavior, Animation animation, GameObject thisObj)
    {
        State state = behavior.GetState(Convert.ToInt32(animation.toStateId));

        if (state != null)
        {
            DG.Tweening.Sequence s = DOTween.Sequence().SetTarget(thisObj);

            s.AppendInterval(animation.delay);

            // empty animation to kickstart
            s.AppendInterval(0f);

            if (!animation.notTransformAnimation)
            {
                s.Insert(0, behavior.transform.DOLocalMove(
                    state.position.ToVector3InvertX(),
                    animation.duration, false));
                s.Insert(0, behavior.transform.DOLocalRotate(state.rotation.ToVector3(),
                    animation.duration,
                    RotateMode.FastBeyond360));

            }
            
            if (animation.updateScale)
            {
                s.Insert(0,behavior.transform.DOScale(
                    state.scale.ToVector3(),
                    animation.duration));
            }
        
            if (animation.materialCustomAnimation)
            {
                foreach (KeyValuePair<MeshRenderer,MaterialCustomProperties> keyValuePair in state.materialCustomChange)
                {
                    Renderer renderer = keyValuePair.Key;
                    MaterialCustomProperties props = keyValuePair.Value;
                    foreach (Material rendererMaterial in renderer.materials)
                    {
                        s.Insert(0, rendererMaterial.DOColor(props.albedoColor, animation.duration));
                        s.Insert(0, rendererMaterial.DOFloat(props.roughness, "roughnessFactor", animation.duration));
                        s.Insert(0, rendererMaterial.DOFloat(props.metalness, "metallicFactor", animation.duration));
                        s.Insert(0, rendererMaterial.DOFade(props.opacity, animation.duration));
                    }
                }
            }
            else if (animation.materialFadeIn || animation.materialFadeOut)
            {
                float opacity = (animation.materialFadeIn ? 1f : 0f);
                Renderer[] renderers = behavior.gameObject.GetComponentsInChildren<Renderer>();
            
                foreach (Renderer renderer in renderers)
                {
                    foreach (Material mat in renderer.materials)
                    {
                        s.Insert(0, mat.DOFade(opacity, animation.duration));
                    }
                }
            }

            return animation.delay + animation.duration;
        }

        return 0f;
    }
    
    private void EnableGameObjects()
    {
        foreach (GameObject enableGameObject in enableGameObjects)
        {
            enableGameObject.SetActive(true);
        }
    }
    private void DisableGameObjectsAfterAllAnimations(float maxAnimDuration)
    {
        if (disableGameObjects.Count != 0)
        {
            DG.Tweening.Sequence disableSequence = DOTween.Sequence().SetId("Disabler");
            disableSequence.AppendInterval(maxAnimDuration).OnComplete(DisableGameObjects);
        }
    }

    private void DisableGameObjects()
    {
        foreach (GameObject disableGameObject in disableGameObjects)
        {
            disableGameObject.SetActive(false);
        }
        DOTween.Kill("Disabler");
    }

}
