using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ArmourBuilderWindow : CreatorWindow
{
    void OnGUI()
    {
        BaseFunction();
        ShowList("ArmourParts");
        if (itemName != "" && itemDescription != "" && ArmourParts.Count >= PartIDs.Length)
        {
            armourData.Slot1 = ArmourParts[PartIDs[0]];
            armourData.Slot2 = ArmourParts[PartIDs[1]];
            armourData.Slot3 = ArmourParts[PartIDs[2]];
            armourData.Slot4 = ArmourParts[PartIDs[3]];
            armourData.Slot5 = ArmourParts[PartIDs[4]];
            if (GUILayout.Button("Build Armour"))
            {
                armourData.Name = itemName;
                armourData.Description = itemDescription;
                armourData.Type = ItemType.eArmour;
                AssignRarity();
                armourData.TotalDefencePhysical = (ArmourParts[PartIDs[0]].BuffValuePart + ArmourParts[PartIDs[1]].BuffValuePart +
                ArmourParts[PartIDs[2]].BuffValuePart + ArmourParts[PartIDs[3]].BuffValuePart + ArmourParts[PartIDs[4]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                armourData.MoveSpeed = (ArmourParts[PartIDs[0]].BuffValuePart2 + ArmourParts[PartIDs[1]].BuffValuePart2 +
                ArmourParts[PartIDs[2]].BuffValuePart2 + ArmourParts[PartIDs[3]].BuffValuePart2 + ArmourParts[PartIDs[4]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;

                BuildItem("BuiltArmours", armourData.Type);

            }
        }
        ViewItem();
        CloseButton();
    }
}
