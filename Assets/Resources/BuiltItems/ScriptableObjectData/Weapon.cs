using UnityEngine;
public enum WeaponType
{
    eSword,
    eAxe,
    eSpear,
    eMace,
}
[CreateAssetMenu(fileName = "Weapon", menuName = "BaseData/Weapon", order = 2)]
public class Weapon : ScriptableObjectData
{
    public WeaponType WeaponType;
    public ScriptableObjectData[] Slots;
    public ScriptableObjectData Slot1;
    public ScriptableObjectData Slot2;
    public ScriptableObjectData Slot3;
    public ScriptableObjectData Slot4;
    public float TotalDamage;
    public float AttackSpeed;
}
