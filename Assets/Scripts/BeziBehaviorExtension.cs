
using Bezi.Bridge;

public static class BeziBehaviorExtension
{
    public static Interaction GetInteraction(this BeziBehavior beziBehavior, string key)
    {
        return beziBehavior.interactions[key];
    }


    // MARK: bezel does not copy the state name, but uses has a list number, starting on 1
    public static State GetState(this BeziBehavior beziBehavior, int animationToStateId)
    {
        return beziBehavior.states.Values[animationToStateId - 1];
    }
}
