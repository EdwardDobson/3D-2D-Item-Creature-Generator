using UnityEngine;
using UnityEditor;
public class WeaponBuilderWindow : CreatorWindow
{
    void OnGUI()
    {
        BaseFunction();
        ShowMaterialList();
        CloseButton();
        if (itemName != null && itemDescription != null)
        {

            if (GUILayout.Button("Build Weapon"))
            {
                BuildItem();
          
                DestroyImmediate(GameObject.Find(itemName));
            }
        }
      
    }
}
