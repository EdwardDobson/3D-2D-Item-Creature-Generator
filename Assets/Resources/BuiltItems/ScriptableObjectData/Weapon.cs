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
    public ScriptableObjectData[] Slots = new ScriptableObjectData[4];
    public float TotalDamage;
    public float AttackSpeed;
    public void WeaponReset()
    {
        Reset();
        WeaponType = WeaponType.eSword;
        Slots = new ScriptableObjectData[4];
        TotalDamage = 0;
        AttackSpeed = 0;
    }
}
