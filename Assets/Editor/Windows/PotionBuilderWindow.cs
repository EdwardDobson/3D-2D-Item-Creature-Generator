using UnityEngine;
using UnityEditor;
public class PotionBuilderWindow : CreatorWindow
{
    float m_buffValue;
    float m_duration;
    void OnGUI()
    {
        BaseFunction();
        ShowList("Materials", Mats, MatNames,materialID);
        m_buffValue = EditorGUILayout.FloatField("Applied Value",m_buffValue);
        m_duration = EditorGUILayout.FloatField("Duration", m_duration);
        if(m_duration < 0)
        {
            m_duration = 0;
        }
        if(m_buffValue < 0)
        {
            m_buffValue = 0;
        }
        if(itemName != "" && itemDescription != "" && objectData.Sprite !=null || objectData.Mesh != null && objectData.Mat != null)
        {
            if (GUILayout.Button("Build Potion"))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = m_buffValue * RaritiesList[rarityID].BuffMuliplier;
              
                objectData.Type = ItemType.ePotion;
                objectData.Duration = m_duration * RaritiesList[rarityID].BuffMuliplier;
                BuildItem("Potions", objectData.Type);

            }
        }

        CloseButton();
    }
}
