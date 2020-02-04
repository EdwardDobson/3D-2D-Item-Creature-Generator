using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeaponBuilderWindow : CreatorWindow
{
    void OnGUI()
    {
        
        BaseFunction();
        ShowMaterialList("WeaponParts");
       

        if (itemName != null && itemDescription != null)
        {

            if (GUILayout.Button("Build Weapon"))
            {
                objectData.type = ItemType.eWeapon;
                BuildItem("BuiltWeapons", objectData.type);
                DestroyImmediate(GameObject.Find(itemName));
            }
        }
        CloseButton();
      
    }
}
