using UnityEngine;

public class WeaponBuilderWindow : CreatorWindow
{
    void OnGUI()
    {
        BaseFunction();
        if(itemName != null && itemDescription != null)
        {
            if (GUILayout.Button("Build Weapon"))
            {
                BuildItem();
                DestroyImmediate(GameObject.Find(itemName));
            }
        }
      
    }
}
