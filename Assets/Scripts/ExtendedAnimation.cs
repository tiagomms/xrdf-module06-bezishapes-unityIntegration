using Bezi.Bridge;
using UnityEngine;
using Animation = Bezi.Bridge.Animation;

[System.Serializable]
public class ExtendedAnimation
{
    // Reference to the original Animation class
    public Animation originalAnimation;

    // New fields you want to add
    public bool updateScale = false;
    public bool materialFadeIn = false;
    public bool materialFadeOut = false;

    public bool notTransformAnimation = false;
    public bool materialCustomAnimation = false;

    // Constructor to initialize with an existing Animation object
    public ExtendedAnimation(Animation animation)
    {
        this.originalAnimation = animation;
    }

    // You can add any new methods or properties here as needed
}