using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ArmourBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        Camera();
        ShowList();
        BuildHandleItem("Armour", ItemType.eArmour, "BuiltArmours", ItemBaseParts);

    }
}
