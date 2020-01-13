using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(BaseInGame))]
public class BaseInspector : Editor
{

    BaseInGame baseInGame;
    protected string[] dimension = new string[] { "2D", "3D" };
    protected int dimensionIndex;
    protected int itemTypeIndex;
    protected string folderName = null;
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
        folderName = EditorGUILayout.TextField("Object Folder Name:", folderName);

        if (GUILayout.Button("Add object type"))
        {
            if (folderName + dimension[dimensionIndex] != "")
            {
                if (dimensionIndex == 0)
                {
                    if (!baseInGame.itemTypesList2D.Contains(folderName + dimension[dimensionIndex]))
                    {
                        baseInGame.itemTypesList2D.Add(folderName + dimension[dimensionIndex]);
                    }
                }
                else if (!baseInGame.itemTypesList3D.Contains(folderName + dimension[dimensionIndex]))
                {
                    if (!baseInGame.itemTypesList3D.Contains(folderName + dimension[dimensionIndex]))
                    {
                        baseInGame.itemTypesList3D.Add(folderName + dimension[dimensionIndex]);
                       
                    }
                }
                CreateFolder();
   
            }
           
        }
    
        RemoveItemType();
        baseInGame.filePath = "/" + dimension[dimensionIndex] + "/" + folderName;
        if (dimensionIndex == 0)
        itemTypeIndex = EditorGUILayout.Popup("ItemTypes", itemTypeIndex, baseInGame.itemTypesList2D.ToArray());
        else
            itemTypeIndex = EditorGUILayout.Popup("ItemTypes", itemTypeIndex, baseInGame.itemTypesList3D.ToArray());

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
            if(dimensionIndex == 0)
            {
                AssetDatabase.DeleteAsset("Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList2D[itemTypeIndex]);
                baseInGame.itemTypesList2D.RemoveAt(itemTypeIndex);
            }
            else
            {
                AssetDatabase.DeleteAsset("Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList3D[itemTypeIndex]);
                baseInGame.itemTypesList3D.RemoveAt(itemTypeIndex);
            }
        }
    }
    void CreateFolder()
    {
        if (folderName != "")
        {
            string s = AssetDatabase.CreateFolder("Assets/Resources/PartsItemGen/" + dimension[dimensionIndex], folderName + dimension[dimensionIndex]);
            string newFolderPath = AssetDatabase.GUIDToAssetPath(s);
            Debug.Log("Creating Folder at: " + newFolderPath);
        }
    }

}
