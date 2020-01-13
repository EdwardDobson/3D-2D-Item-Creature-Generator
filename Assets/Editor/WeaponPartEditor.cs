using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WeaponPartBuilderInGame))]
public class WeaponPartEditor : Editor
{
    WeaponPartBuilderInGame weaponBuilder;
    int weaponPartTypeIndex;
    string partName;
    string[] dimension = { "2D", "3D" };
    int dimensionIndex;
    int statBoost;
    // Start is called before the first frame update
    void OnEnable()
    {
        weaponBuilder = ((MonoBehaviour)target).gameObject.GetComponent<WeaponPartBuilderInGame>();
    }
    public override void OnInspectorGUI()
    {
        partName = EditorGUILayout.TextField("Part Name: ", partName);
        weaponPartTypeIndex  = EditorGUILayout.Popup(weaponPartTypeIndex, weaponBuilder.partTypes);
        dimensionIndex = EditorGUILayout.Popup(dimensionIndex, dimension);
        statBoost = EditorGUILayout.IntField("Stat boost: ", statBoost);
        Debug.Log(weaponPartTypeIndex);
        if (GUILayout.Button("CreatePart"))
        {
            if (partName != "")
            {
                WeaponPartBuilder part = CreateInstance<WeaponPartBuilder>();
                part.partName = partName;
                part.partType = (WeaponPartType)weaponPartTypeIndex-1;
                part.dimension = (Dimension)dimensionIndex;
                part.statBoost = statBoost;
                weaponBuilder.BuildPart(part);
                AssetDatabase.CreateAsset(part, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/Parts" + dimension[dimensionIndex] + "/" + partName + ".asset");
            }
        }
    }
}
