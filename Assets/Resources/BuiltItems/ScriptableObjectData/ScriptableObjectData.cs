
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
public enum Rarity
{ 
    eCommon,
    eUncommon,
    eRare,
    eEpic,
    eLegendary,
    eUnique,
}

[CreateAssetMenu(fileName = "Object", menuName = "BaseData", order = 2)]
public class ScriptableObjectData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public Mesh Mesh;
    public float BuffValueMaterial;
    public ItemType Type;
    public Rarity Rarity;
    public float BuffValuePart;
}
