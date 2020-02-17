using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ArmourBuilderWindow : CreatorWindow
{
    void OnGUI()
    {
        BaseFunction();
        ShowList("ArmourParts", ArmourParts, ArmourPartNames, ArmourPartsID);
        armourData.Slot1 = ArmourParts[ArmourPartsID[0]];
        armourData.Slot2 = ArmourParts[ArmourPartsID[1]];
        armourData.Slot3 = ArmourParts[ArmourPartsID[2]];
        armourData.Slot4 = ArmourParts[ArmourPartsID[3]];
        armourData.Slot5 = ArmourParts[ArmourPartsID[4]];
        if (itemName != "" && itemDescription != "" && ArmourParts.Count >= ArmourPartsID.Length)
        {
          
            if (GUILayout.Button("Build Armour"))
            {
                armourData.Name = itemName;
                armourData.Description = itemDescription;
                armourData.Type = ItemType.eArmour;
                switch (rarityID)
                {
                    case 0:
                        armourData.Rarity = Rarity.eCommon;
                        break;
                    case 1:
                        armourData.Rarity = Rarity.eUncommon;
                        break;
                    case 2:
                        armourData.Rarity = Rarity.eRare;
                        break;
                    case 3:
                        armourData.Rarity = Rarity.eEpic;
                        break;
                    case 4:
                        armourData.Rarity = Rarity.eLegendary;
                        break;
                    case 5:
                        armourData.Rarity = Rarity.eUnique;
                        break;
                }

                armourData.TotalDefencePhysical = (ArmourParts[ArmourPartsID[0]].BuffValuePart + ArmourParts[ArmourPartsID[1]].BuffValuePart +
                ArmourParts[ArmourPartsID[2]].BuffValuePart + ArmourParts[ArmourPartsID[3]].BuffValuePart + ArmourParts[ArmourPartsID[4]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                armourData.MoveSpeed = (ArmourParts[ArmourPartsID[0]].BuffValuePart2 + ArmourParts[ArmourPartsID[1]].BuffValuePart2 +
                ArmourParts[ArmourPartsID[2]].BuffValuePart2 + ArmourParts[ArmourPartsID[3]].BuffValuePart2 + ArmourParts[ArmourPartsID[4]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;

                BuildItem("BuiltArmours", armourData.Type);

            }
        }
        ViewItem();
        CloseButton();
    }
}
