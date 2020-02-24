using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        Camera();
        ShowList();
        BuildHandleItem("Weapon", ItemType.eWeapon, "BuiltWeapons", ItemBaseParts);
        
    }
}
