using UnityEngine;
using UnityEditor;
public class MaterialWindow : CreatorWindow
{
    float m_buffValue;
    void OnGUI()
    {
        BaseFunction();
        m_buffValue = EditorGUILayout.FloatField("Buff Value: ", m_buffValue);
     
   
        if (objectData != null)
        {
            objectData.BuffValue = m_buffValue;
        if (itemName != null && itemDescription != null)
        {
            if (GUILayout.Button("Build Material"))
            {
                    objectData.IsMaterial = true;
                BuildItem();
                DestroyImmediate(GameObject.Find(itemName));
            }
        }
        }
    }

}
