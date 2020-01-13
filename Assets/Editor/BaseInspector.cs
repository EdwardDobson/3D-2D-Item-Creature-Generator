using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(BaseInGame))]
public class BaseInspector : Editor
{

    BaseInGame baseInGame;
    string[] dimension = new string[] { "2D", "3D" };
    int dimensionIndex;

    int itemTypeIndex;
    string folderName = null;
    void OnEnable()
    {
        baseInGame = ((MonoBehaviour)target).gameObject.GetComponent<BaseInGame>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnInspectorGUI()
    {

        dimensionIndex = EditorGUILayout.Popup("Dimension", dimensionIndex, dimension);
        GUILayout.Label("Enter the name of resources folder you wish to add.");
        folderName = EditorGUILayout.TextField("", folderName);

        if (GUILayout.Button("Add object type"))
        {
            if (dimensionIndex == 0)
            {
                if (!baseInGame.itemTypesList2D.Contains(folderName) && folderName != "")
                {
                    baseInGame.itemTypesList2D.Add(folderName);
                    Debug.Log("Creating item 2D");
                }
            }
            else if (!baseInGame.itemTypesList3D.Contains(folderName) && folderName != "")
            {

                baseInGame.itemTypesList3D.Add(folderName);
                Debug.Log("Creating item 3D");

            }
            baseInGame.filePath = "/" + dimension[dimensionIndex] + "/" + folderName;
            if (folderName != "")
            {
                string s = AssetDatabase.CreateFolder("Assets/Resources/PartsItemGen/" + dimension[dimensionIndex], folderName);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(s);
                Debug.Log("Creating Folder at: " + newFolderPath);

            }

        }
        RemoveItemType();
        if (dimensionIndex == 0)
        {
            Debug.Log("2d mode");
            itemTypeIndex = EditorGUILayout.Popup("ItemTypes", itemTypeIndex, baseInGame.itemTypesList2D.ToArray());
        }
        else
        {
            Debug.Log("3d mode");
            itemTypeIndex = EditorGUILayout.Popup("ItemTypes", itemTypeIndex, baseInGame.itemTypesList3D.ToArray());
        }
        if (GUILayout.Button("Update Object List"))
        {
            baseInGame.findObjects();
            if (baseInGame.objects.Length <= 0)
            {
                Debug.Log("Error no assets found");
            }

        }
    }
    void RemoveItemType()
    {
        if (GUILayout.Button("Remove Item Type"))
        {
            if (dimensionIndex == 0)
            {
                AssetDatabase.DeleteAsset("Assets/Resources/PartsItemGen/2D/" + baseInGame.itemTypesList2D[itemTypeIndex]);
                Debug.Log(baseInGame.itemTypesList2D[itemTypeIndex]);
                baseInGame.itemTypesList2D.RemoveAt(itemTypeIndex);
            }
            else
            {
                AssetDatabase.DeleteAsset("Assets/Resources/PartsItemGen/2D/" + baseInGame.itemTypesList2D[itemTypeIndex]);
                baseInGame.itemTypesList3D.RemoveAt(itemTypeIndex);
                Debug.Log(baseInGame.itemTypesList3D[itemTypeIndex]);
            }
        }
    }

}
