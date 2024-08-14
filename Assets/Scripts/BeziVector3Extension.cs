using Bezi.Bridge;
using UnityEngine;

public static class BeziVector3Extension
{
    public static Vector3 ToVector3(this BeziVector3 beziVector)
    {
        return new Vector3(beziVector.x, beziVector.y, beziVector.z);
    }

    public static Vector3 ToVector3InvertX(this BeziVector3 beziVector)
    {
        return new Vector3(-beziVector.x, beziVector.y, beziVector.z);
    }
}