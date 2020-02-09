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
            objectData.BuffValueMaterial = m_buffValue * RaritiesList[rarityID].BuffMuliplier;
            if (itemName != "" && itemDescription != "")
            {
                if (GUILayout.Button("Build Material"))
                {
                    objectData.Sprite = itemTexture;
                    objectData.Name = itemName;
                    objectData.Description = itemDescription;
                    objectData.Type = ItemType.eMaterial;
                    BuildItem("Materials",objectData.Type);
                }
            }
        }
        CloseButton();
    }

}
