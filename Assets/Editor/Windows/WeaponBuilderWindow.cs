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
        }

    
        ShowWeaponPartsList();
        if (itemName != null && itemDescription != null)
        {
            if (WeaponParts[0] != null)
                weaponData.Slot1 = WeaponParts[WeaponPartsID[0]];
            if (WeaponParts[1] != null)
                weaponData.Slot2 = WeaponParts[WeaponPartsID[1]];
            if (WeaponParts[2] != null)
                weaponData.Slot3 = WeaponParts[WeaponPartsID[2]];
            if (WeaponParts[3] != null)
                weaponData.Slot4 = WeaponParts[WeaponPartsID[3]];
            if (GUILayout.Button("Build Weapon"))
            {
                weaponData.Name = itemName;
                weaponData.Description = itemDescription;
                weaponData.Type = ItemType.eWeapon;
                switch (rarityID)
                {
                    case 0:
                        weaponData.Rarity = Rarity.eCommon;
                        break;
                    case 1:
                        weaponData.Rarity = Rarity.eUncommon;
                        break;
                    case 2:
                        weaponData.Rarity = Rarity.eRare;
                        break;
                    case 3:
                        weaponData.Rarity = Rarity.eEpic;
                        break;
                    case 4:
                        weaponData.Rarity = Rarity.eLegendary;
                        break;
                    case 5:
                        weaponData.Rarity = Rarity.eUnique;
                        break;
                }


                if (m_weaponTypeID != 0)
                {
                    weaponData.Slot4 = null;
                }
                if (weaponData.Slot4 == null)
                {
                    weaponData.TotalDamage = (WeaponParts[WeaponPartsID[0]].BuffValuePart + WeaponParts[WeaponPartsID[1]].BuffValuePart +
                    WeaponParts[WeaponPartsID[2]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                    weaponData.AttackSpeed = (WeaponParts[WeaponPartsID[0]].BuffValuePart2 + WeaponParts[WeaponPartsID[1]].BuffValuePart2 +
                    WeaponParts[WeaponPartsID[2]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;
                    Debug.Log("Adding 3 slot damage");
                }
                else
                {
                    weaponData.TotalDamage = (WeaponParts[WeaponPartsID[0]].BuffValuePart + WeaponParts[WeaponPartsID[1]].BuffValuePart +
                    WeaponParts[WeaponPartsID[2]].BuffValuePart + WeaponParts[WeaponPartsID[3]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                    weaponData.AttackSpeed = (WeaponParts[WeaponPartsID[0]].BuffValuePart2 + WeaponParts[WeaponPartsID[1]].BuffValuePart2 +
                    WeaponParts[WeaponPartsID[2]].BuffValuePart2 + WeaponParts[WeaponPartsID[3]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;
                    Debug.Log("Adding 4 slot damage");
                }
                BuildItem("BuiltWeapons", weaponData.Type);
          
            }
        }
        ViewItem();
        CloseButton();
    }
}
