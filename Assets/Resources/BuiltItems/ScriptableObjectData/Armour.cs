using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "BaseData/Armour", order = 2)]
public class Armour : ScriptableObjectData
{
    public ScriptableObjectData Slot1;
    public ScriptableObjectData Slot2;
    public ScriptableObjectData Slot3;
    public ScriptableObjectData Slot4;
    public ScriptableObjectData Slot5;
    public float TotalDefencePhysical;
    public float MoveSpeed;

    public void ArmourReset()
    {
        Reset();
        Slot1 = null;
        Slot2 = null;
        Slot3 = null;
        Slot4 = null;
        Slot5 = null;
        TotalDefencePhysical = 0;
        MoveSpeed = 0;
    }
}
