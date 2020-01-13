using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(BaseCreator))]
public class BaseCreatorEditor : BaseInspector
{
    BaseCreator baseCreator;
    BaseInGame baseInGame;
    // Start is called before the first frame update
    void OnEnable()
    {
        baseCreator = ((MonoBehaviour)target).gameObject.GetComponent<BaseCreator>();
        baseInGame = ((MonoBehaviour)target).gameObject.GetComponent<BaseInGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInspectorGUI()
    {
        baseCreator.itemName = EditorGUILayout.TextField("Item name:", baseCreator.itemName);
        baseCreator.itemType = EditorGUILayout.TextField("Item type:", baseCreator.itemType);
        if (GUILayout.Button("Create " + baseCreator.itemName))
        {
          
            BaseObject item = CreateInstance<BaseObject>();
            baseCreator.Create(item);
           
            if (dimensionIndex == 0)
            {
                item.objectType = baseInGame.itemTypesList2D[itemTypeIndex];
               AssetDatabase.CreateAsset(item, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList2D[itemTypeIndex] + "/" + dimension[dimensionIndex] + baseCreator.itemName + ".asset");
            }
            else
            {

                item.objectType = baseInGame.itemTypesList3D[itemTypeIndex];
                AssetDatabase.CreateAsset(item, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList3D[itemTypeIndex] + "/" + dimension[dimensionIndex] + baseCreator.itemName + ".asset");
            }
      

        } 
    }
}
