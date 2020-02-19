using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponBuilderWindow : CreatorWindow
{

    int m_weaponTypeID;
    void OnGUI()
    {
        BaseFunction();
        m_weaponTypeID = EditorGUILayout.Popup("Weapon Type", m_weaponTypeID, System.Enum.GetNames(typeof(WeaponType)));
        switch (m_weaponTypeID)
        {
            case 0:
                weaponData.WeaponType = WeaponType.eSword;
                break;
            case 1:
                weaponData.WeaponType = WeaponType.eAxe;
                break;
            case 2:
                weaponData.WeaponType = WeaponType.eSpear;
                break;
            case 3:
                weaponData.WeaponType = WeaponType.eMace;
                break;
            default:
                break;
        }
        ShowList("WeaponParts");
         
        Debug.Log(WeaponParts[PartIDs[0]]);
        if (itemName != "" && itemDescription != "")
        {
            weaponData.Slot1 = WeaponParts[PartIDs[0]];
            weaponData.Slot2 = WeaponParts[PartIDs[1]];
            weaponData.Slot3 = WeaponParts[PartIDs[2]];
            weaponData.Slot4 = WeaponParts[PartIDs[3]];
            if (GUILayout.Button("Build Weapon"))
            {
                weaponData.Name = itemName;
                weaponData.Description = itemDescription;
                weaponData.Type = ItemType.eWeapon;
                AssignRarity();
                if (m_weaponTypeID != 0)
                {
                    weaponData.Slot4 = null;
                }
                    weaponData.TotalDamage = (WeaponParts[PartIDs[0]].BuffValuePart + WeaponParts[PartIDs[1]].BuffValuePart +
                    WeaponParts[PartIDs[2]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                    weaponData.AttackSpeed = (WeaponParts[PartIDs[0]].BuffValuePart2 + WeaponParts[PartIDs[1]].BuffValuePart2 +
                    WeaponParts[PartIDs[2]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;
                BuildItem("BuiltWeapons", weaponData.Type);

            }
        }
        ViewItem();
        CloseButton();
    }
}
