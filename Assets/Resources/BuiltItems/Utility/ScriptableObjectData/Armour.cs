using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "BaseData/Armour", order = 2)]
public class Armour : ScriptableObjectData
{
    public ScriptableObjectData[] Slots = new ScriptableObjectData[5];
    public float TotalDefencePhysical;
    public float MoveSpeed;

    public void ArmourReset()
    {
        Reset();
        Slots = new ScriptableObjectData[4];
        TotalDefencePhysical = 0;
        MoveSpeed = 0;
    }
}
