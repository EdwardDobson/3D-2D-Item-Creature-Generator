using UnityEngine;
using UnityEditor;
public class MaterialWindow : SubWindowHandler
{
    float m_buffValue;
    void OnGUI()
    {
        BaseFunction();
        m_buffValue = EditorGUILayout.FloatField("Buff Value: ", m_buffValue);
        BuildHandle("Material", ItemType.eMaterial, "Materials", m_buffValue);
    }

}
