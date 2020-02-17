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
    public ScriptableObjectData Slot1;
    public ScriptableObjectData Slot2;
    public ScriptableObjectData Slot3;
    public ScriptableObjectData Slot4;
    public float TotalDamage;
    public float AttackSpeed;
    public void WeaponReset()
    {
        Reset();
        WeaponType = WeaponType.eSword;
        Slot1 = null;
        Slot2 = null;
        Slot3 = null;
        Slot4 = null;
        TotalDamage = 0;
        AttackSpeed = 0;
    }
}
