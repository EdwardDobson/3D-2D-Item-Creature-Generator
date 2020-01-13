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
    int partsIndex;
    // Start is called before the first frame update
    void OnEnable()
    {
        baseCreator = ((MonoBehaviour)target).gameObject.GetComponent<BaseCreator>();
        baseInGame = ((MonoBehaviour)target).gameObject.GetComponent<BaseInGame>();
        baseCreator.parts.Clear();
        baseCreator.partsNames.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnInspectorGUI()
    {
        baseCreator.itemName = EditorGUILayout.TextField("Item name:", baseCreator.itemName);
     
        if (GUILayout.Button("Create " + baseCreator.itemName))
        {
            BaseObject item = CreateInstance<BaseObject>();
            item.objectName = baseCreator.itemName;
            if(dimensionIndex == 0)
            {
                item.dimension = Dimension.e2D;
            }
            else
            {
                item.dimension = Dimension.e3D;
            }
            if (dimensionIndex == 0)
            {
               item.objectType = baseInGame.itemTypesList2D[itemTypeIndex];
               // baseCreator.Create(item, 1);
                AssetDatabase.CreateAsset(item, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList2D[itemTypeIndex] + "/" + dimension[dimensionIndex] + baseCreator.itemName + ".asset");
            }
            else
            {

                item.objectType = baseInGame.itemTypesList3D[itemTypeIndex];
             //   baseCreator.Create(item, 1);
                AssetDatabase.CreateAsset(item, "Assets/Resources/PartsItemGen/" + dimension[dimensionIndex] + "/" + baseInGame.itemTypesList3D[itemTypeIndex] + "/" + dimension[dimensionIndex] + baseCreator.itemName + ".asset");
            }
      

        }
        partsIndex = EditorGUILayout.Popup("Parts", partsIndex, baseCreator.partsNames.ToArray());
    }
}
