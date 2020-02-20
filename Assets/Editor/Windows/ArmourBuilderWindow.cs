using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ArmourBuilderWindow : SubWindowHandler
{
    void OnGUI()
    {
        BaseFunction();
        ShowList("ArmourParts");
        ViewItem();
        BuildHandleItem("Armour", ItemType.eArmour, "BuiltArmours", ItemBaseParts);

    }
}
