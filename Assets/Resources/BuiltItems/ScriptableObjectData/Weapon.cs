using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "BaseData/Weapon", order = 2)]
public class Weapon : ScriptableObjectData
{
    public ScriptableObjectData Slot1;
    public ScriptableObjectData Slot2;
    public ScriptableObjectData Slot3;
    public ScriptableObjectData Slot4;
    public float TotalDamage;
}
