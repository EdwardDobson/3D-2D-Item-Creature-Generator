using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponPartType
{
    eHilt,
    eBlade,
    eGuard,
    ePummel,
}


[CreateAssetMenu(fileName = "WeaponPart", menuName = "WeaponPart", order = 1)]
public class WeaponPartBuilder : ScriptableObject
{
    public string partName;
    public Sprite icon;
    public WeaponPartType partType;
    public Dimension dimension;
    public int statBoost;

}
