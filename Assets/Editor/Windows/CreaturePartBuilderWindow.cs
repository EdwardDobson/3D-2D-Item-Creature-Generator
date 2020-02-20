using UnityEngine;
using UnityEditor;

public class CreaturePartBuilderWindow : SubWindowHandler
{
    float m_speedModifer;
    int m_creaturePartID;
  
    void OnGUI()
    {
        Handle("Materials");
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        m_creaturePartID = EditorGUILayout.Popup("Part Type", m_creaturePartID, System.Enum.GetNames(typeof(CreatureParts)));
        foreach (int i in System.Enum.GetValues(typeof(CreatureParts)))
        {
            if(m_creaturePartID == i)
            {
                objectData.CreaturePartType = (CreatureParts)m_creaturePartID;
            }
        }
        BuildHandle("Creature Part", ItemType.eCreaturePart, "CreatureParts", m_speedModifer);
    }
}
