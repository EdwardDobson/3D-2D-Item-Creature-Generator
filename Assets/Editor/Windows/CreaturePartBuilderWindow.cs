using UnityEngine;
using UnityEditor;

public class CreaturePartBuilderWindow : CreatorWindow
{
    float m_speedModifer;
    int m_creaturePartID;
  
    void OnGUI()
    {
        BaseFunction();
 
        ShowList("Materials");
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        m_creaturePartID = EditorGUILayout.Popup("Part Type", m_creaturePartID, System.Enum.GetNames(typeof(CreatureParts)));
        foreach (int i in System.Enum.GetValues(typeof(CreatureParts)))
        {
            if(m_creaturePartID == i)
            {
                objectData.CreaturePartType = (CreatureParts)m_creaturePartID;
            }
        }
        
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        if (itemName != "" && itemDescription != "")
        {
            if (GUILayout.Button("Build Creature Part"))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[PartIDs[0]].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValuePart2 = m_speedModifer * RaritiesList[rarityID].BuffMuliplier;
                objectData.BuffValueMaterial = 0;
                objectData.Type = ItemType.eCreaturePart;

                BuildItem("CreatureParts", objectData.Type);
                m_speedModifer = 0;
            }
        }
        CloseButton();
    }
}
