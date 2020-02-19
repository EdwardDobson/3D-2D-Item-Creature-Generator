using UnityEngine;
using UnityEditor;
public class PotionBuilderWindow : SubWindowHandler
{
    float m_buffValue;
    float m_duration;
    void OnGUI()
    {
        Handle("Materials");

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
        BuildHandle("Potion", ItemType.ePotion, "Potions", m_duration);
    }
}
