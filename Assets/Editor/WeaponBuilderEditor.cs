using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WeaponBuilderInGame))]
public class WeaponBuilderEditor : Editor
{
    WeaponBuilderInGame weaponBuilder;
    WeaponPartBuilderInGame parts;
    string weaponName;
    int weaponTypeIndex;
    int dimensionIndex;
    int partTotalIndex;
    int partsAvailableIndex;
    List<WeaponPartBuilder> applyingparts = new List<WeaponPartBuilder>();
    string[] dimension = { "2D", "3D" };
    // Start is called before the first frame update
    void OnEnable()
    {
        weaponBuilder = ((MonoBehaviour)target).gameObject.GetComponent<WeaponBuilderInGame>();
        parts = ((MonoBehaviour)target).gameObject.GetComponent<WeaponPartBuilderInGame>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnInspectorGUI()
    {
        weaponName = EditorGUILayout.TextField("Weapon Name: ", weaponName);
        weaponTypeIndex = EditorGUILayout.Popup(weaponTypeIndex, weaponBuilder.weaponTypeNames);
        dimensionIndex = EditorGUILayout.Popup(dimensionIndex, dimension);

        partTotalIndex = EditorGUILayout.IntField("Part total: ", partTotalIndex);
        partsAvailableIndex = EditorGUILayout.Popup(partsAvailableIndex, weaponBuilder.partsAvailable2DNames.ToArray());

        if (GUILayout.Button("Update Parts List"))
        {
            weaponBuilder.UpdateParts();
        }

        if (GUILayout.Button("Apply part"))
        {
            if (!applyingparts.Contains((WeaponPartBuilder)weaponBuilder.partsAvailable2D[partsAvailableIndex]))
                applyingparts.Add((WeaponPartBuilder)weaponBuilder.partsAvailable2D[partsAvailableIndex]);

        }
        if (GUILayout.Button("Create Weapon"))
        {
            if (weaponName != "")
            {
                WeaponBase weapon = CreateInstance<WeaponBase>();
                weapon.weaponName = weaponName;
                weapon.weaponType = weaponBuilder.weaponTypeNames[weaponTypeIndex];
                weapon.dimension = (Dimension)dimensionIndex;
                weapon.partTotal = partTotalIndex;
                weapon.parts = new List<WeaponPartBuilder>(partTotalIndex);
                for (int i = 0; i < applyingparts.Count; i++)
                {
                    weapon.parts.Add(applyingparts[i]);
                    weapon.damage += applyingparts[i].statBoost;
                }
                weaponBuilder.Create(weapon);
                AssetDatabase.CreateAsset(weapon, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/Weapon" + dimension[dimensionIndex] + "/" + weaponName + ".asset");
            }
        }
    }
}
