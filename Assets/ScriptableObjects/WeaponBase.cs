using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseWeapon", menuName = "WeaponObject", order = 1)]
public class WeaponBase : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public MeshFilter mesh;
    public string weaponType;
    public Dimension dimension;
    public int damage;
    public List<WeaponPartBuilder> parts;
    public int partTotal;
}
