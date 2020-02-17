using UnityEngine;
using UnityEditor;
public class ArmourPartBuilderWindow : CreatorWindow
{
    float m_speedModifer;
    void OnGUI()
    {
        BaseFunction();
        ShowList("Materials", Mats, MatNames, materialID);
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        if (itemName != "" && itemDescription != "" && Mats.Count >= materialID.Length && objectData.Sprite != null || objectData.Mesh != null && objectData.Mat != null)
        {
            if (GUILayout.Button("Build Armour Part"))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID[0]].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValuePart2 = m_speedModifer * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValueMaterial = 0;
                objectData.Type = ItemType.eArmourPart;

                BuildItem("ArmourParts", objectData.Type);
                m_speedModifer = 0;
            }
        }
        CloseButton();
    }
}
