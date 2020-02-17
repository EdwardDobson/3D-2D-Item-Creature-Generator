using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CreatureParts
{
    eHead,
    eLeftShoulder,
    eRightShoulder,
    eChest,
    eAbdomen,
    eUpperLeftLeg,
    eUpperRightLeg,
    eLowerRightLeg,
    eLowerLeftLeg,
    eLeftFoot,
    eRightFoot,
    eUpperLeftArm,
    eUpperRightArm,
    eLowerLeftArm,
    eLowerRightArm,
    eLeftHand,
    eRightHand,
}
[CreateAssetMenu(fileName = "Object", menuName = "BaseData", order = 2)]
public class Creature : ScriptableObjectData
{
    public ScriptableObjectData[] Parts;

    public void CreatureReset()
    {
        Reset();
        Parts = null;
    }
}
