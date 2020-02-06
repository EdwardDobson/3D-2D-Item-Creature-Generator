
using UnityEngine;
public enum ItemType
{
    eMaterial,
    eWeaponPart,
    eArmourPart,
    eWeapon,
    eArmour,
    ePotion,
}
[CreateAssetMenu(fileName = "Object", menuName = "BaseData", order = 2)]
public class ScriptableObjectData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public Mesh Mesh;
    public float BuffValueMaterial;
    public ItemType type;
    public float BuffValuePart;
}
