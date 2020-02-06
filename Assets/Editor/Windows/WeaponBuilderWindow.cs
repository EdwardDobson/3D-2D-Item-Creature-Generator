using UnityEngine;
using UnityEditor;
public class WeaponBuilderWindow : CreatorWindow
{

    void OnGUI()
    {
        BaseFunction();
        ShowWeaponPartsList();
        if (itemName != null && itemDescription != null)
        {
            if (GUILayout.Button("Build Weapon"))
            {
                weaponData.Name = itemName;
                weaponData.Description = itemDescription;
                weaponData.type = ItemType.eWeapon;
                weaponData.Slot1 = WeaponParts[WeaponPartsID[0]];
                weaponData.Slot2 = WeaponParts[WeaponPartsID[1]];
                weaponData.Slot3 = WeaponParts[WeaponPartsID[2]];
                weaponData.Slot4 = WeaponParts[WeaponPartsID[3]];
                weaponData.TotalDamage = WeaponParts[WeaponPartsID[0]].BuffValuePart + WeaponParts[WeaponPartsID[1]].BuffValuePart + WeaponParts[WeaponPartsID[2]].BuffValuePart + WeaponParts[WeaponPartsID[3]].BuffValuePart;

                BuildItem("BuiltWeapons", weaponData.type);
                DestroyImmediate(GameObject.Find(itemName));
            }
        }
        CloseButton();
    }
}
