using UnityEngine;
using UnityEditor;
public class WeaponPartWindow : CreatorWindow
{
   float m_speedModifer;
   void OnGUI()
    {
   
        BaseFunction();
        ShowList("Materials", Mats, MatNames, MatNames3D, materialID);
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        if (itemName != "" && itemDescription != "" && Mats.Count >= materialID.Length && objectData.Sprite != null || objectData.Mesh != null && objectData.Mat != null)
        {
            if (GUILayout.Button("Build Weapon Part"))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID[0]].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValuePart2 = m_speedModifer * RaritiesList[rarityID].BuffMuliplier;
           
                objectData.Type = ItemType.eWeaponPart;
                Debug.Log("Building" + Mats[materialID[0]].BuffValueMaterial);
                BuildItem("WeaponParts",objectData.Type);
               
            }
        }
        CloseButton();
   }

}
