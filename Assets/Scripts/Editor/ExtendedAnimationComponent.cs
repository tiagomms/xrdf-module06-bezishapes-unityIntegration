using UnityEngine;
using Animation = Bezi.Bridge.Animation;

public class ExtendedAnimationComponent : MonoBehaviour
{
    public BeziAnimationComponent animationComponent;
    public ExtendedAnimation extendedAnimation;

    private void Awake()
    {
        if (animationComponent != null)
        {
            extendedAnimation = new ExtendedAnimation(animationComponent.baseAnimation);
        }
    }
}