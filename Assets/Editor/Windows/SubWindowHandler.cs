using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script contains the base attributes and functions that are used in item building
public class SubWindowHandler : CreatorWindow
{
    protected void Handle(string _dir)
    {
        ScrollbarStart();
        BaseFunction();
        ShowList();
        EndView();
    }
    protected void BuilderHandle()
    {
        ScrollbarStart();
        BaseFunction();
        Camera();
        ShowList();
        EndView();
    }
    //_dir example "Potions" _TypeName example "Potion" Make sure that the _dir is all one word
    protected void BuildHandle(string _TypeName, ItemType _type, string _dir, float _duration)
    {
        if (itemName != "" && itemDescription != "" && objectData.Sprite != null && objectData.Mat != null || objectData.Mesh != null && objectData.Mat != null)
        {
            if (GUILayout.Button("Build " + _TypeName))
            {

                objectData.Name = itemName;
                objectData.Description = itemDescription;
                AssignRarity();
                if (currentWindowName != "Material Builder")
                    objectData.BuffValuePart = Parts[PartIDs[0]].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                else
                    objectData.BuffValueMaterial = _duration * RaritiesList[rarityID].BuffMuliplier;
                objectData.Type = _type;
                objectData.Struct.isFullItem = false;
                objectData.Duration = _duration * RaritiesList[rarityID].BuffMuliplier;
                BuildItem(_dir, objectData.Struct);

            }
        }
        CloseButton();
    }
    protected void BuildHandleItem(string _TypeName, ItemType _type, string _dir, List<ScriptableObjectData> _parts)
    {
        itemBaseData.Slots = new ScriptableObjectData[slotAmount];
        if (itemName != "" && itemDescription != "")
        {
            if (GUILayout.Button("Build " + _TypeName))
            {

                for (int i = 0; i < itemBaseData.Slots.Length; ++i)
                {
                    itemBaseData.Slots[i] = _parts[PartIDs[i]];
                }

                itemBaseData.Name = itemName;
                itemBaseData.Description = itemDescription;
                itemBaseData.Type = _type;
                itemBaseData.Struct.isFullItem = true;
                AssignRarity();

                itemBaseData.BuffValue = (_parts[PartIDs[0]].BuffValuePart + _parts[PartIDs[1]].BuffValuePart +
                _parts[PartIDs[2]].BuffValuePart) * RaritiesList[rarityID].BuffMuliplier;
                itemBaseData.Speed = (_parts[PartIDs[0]].BuffValuePart2 + _parts[PartIDs[1]].BuffValuePart2 +
                _parts[PartIDs[2]].BuffValuePart2) * RaritiesList[rarityID].BuffMuliplier;
                BuildItem(_dir, itemBaseData.Struct);
            }
        }
        CloseButton();
    }
}
