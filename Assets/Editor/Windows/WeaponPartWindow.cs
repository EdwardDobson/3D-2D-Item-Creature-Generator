using UnityEngine;
using UnityEditor;
public class WeaponPartWindow : CreatorWindow
{

  
    void OnGUI()
    {
   
        BaseFunction();
        if (itemName != "" && itemDescription != "")
        {
            ShowMaterialList("Materials");
         
            if (GUILayout.Button("Build Weapon Part"))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID].BuffValueMaterial;
                objectData.BuffValueMaterial = 0;
                objectData.type = ItemType.eWeaponPart;
                objectData.Sprite = itemTexture;
                BuildItem("WeaponParts",objectData.type);
             
            }
        }
        CloseButton();
    }
}
