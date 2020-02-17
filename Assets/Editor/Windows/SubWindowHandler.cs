using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script contains the base attributes and functions that are used in item building
public class SubWindowHandler : CreatorWindow
{
    protected void Handle(string _dir, List<ScriptableObjectData> _data, List<string> _matNames,int[] _ids)
    {
        BaseFunction();
        ShowList(_dir, _data, _matNames, _ids);
      
    }
    //_dir example "Potions" _TypeName example "Potion" Make sure that the _dir is all one word
    protected void BuildHandle(string _TypeName, ItemType _type,string _dir,float _duration)
    {
        if (itemName != "" && itemDescription != "" && objectData.Sprite != null || objectData.Mesh != null && objectData.Mat != null)
        {
            if (GUILayout.Button("Build " + _TypeName))
            {
                objectData.Name = itemName;
                objectData.Description = itemDescription;
                objectData.BuffValuePart = Mats[materialID[0]].BuffValueMaterial * RaritiesList[rarityID].BuffMuliplier;
                objectData.Type = _type;
                objectData.Duration = _duration * RaritiesList[rarityID].BuffMuliplier;
                BuildItem(_dir, objectData.Type);

            }
        }
        CloseButton();
    }
}
