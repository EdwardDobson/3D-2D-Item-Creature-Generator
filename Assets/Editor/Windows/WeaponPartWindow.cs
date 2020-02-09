using UnityEngine;
using UnityEditor;
public class WeaponPartWindow : CreatorWindow
{
   float m_speedModifer;
   void OnGUI()
    {
   
        BaseFunction();
        ShowMaterialList("Materials");
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        if (itemName != "" && itemDescription != "")
        {
            if (GUILayout.Button("Build Weapon Part"))
            {
                objectData.Sprite = itemTexture;
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValuePart2 = m_speedModifer * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValueMaterial = 0;
                objectData.Type = ItemType.eWeaponPart;
                
                BuildItem("WeaponParts",objectData.Type);
                m_speedModifer = 0;
            }
        }
        CloseButton();
   }
}
