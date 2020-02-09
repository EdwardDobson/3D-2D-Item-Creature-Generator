using UnityEngine;
using UnityEditor;
public class WeaponPartWindow : CreatorWindow
{
   void OnGUI()
    {
   
        BaseFunction();
        ShowMaterialList("Materials");
        if (itemName != "" && itemDescription != "")
        {
            if (GUILayout.Button("Build Weapon Part"))
            {
                objectData.Sprite = itemTexture;
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValueMaterial = 0;
                objectData.Type = ItemType.eWeaponPart;
                
                BuildItem("WeaponParts",objectData.Type);
             
            }
        }
        CloseButton();
   }
}
