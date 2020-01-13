using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[ExecuteAlways]
public class BaseCreator : MonoBehaviour
{
    public string itemName;
    public string itemType;
    public Dimension dimension;
    public Sprite sprite;
    public MeshFilter mesh;
    public int amountToCreate;
    public BaseObject scriptableObject;
    public WeaponBase weaponObject;
    public PartBase part;
    public List<PartBase> parts = new List<PartBase>();
    public List<string> partsNames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void Create(BaseObject _object,int _partTotal)
    {
        amountToCreate = _partTotal;
        for (int i = 0; i < amountToCreate; ++i)
        {
           
            if(_object.objectType.Contains("Weapon") || _object.objectType.Contains("weapon"))
            {
                weaponObject = (WeaponBase)_object;
                weaponObject.partTotal = _partTotal;
                if (dimension == Dimension.e2D)
                {
                    weaponObject.sprite = sprite;
                    weaponObject.mesh = null;
                }
                else
                {
                    weaponObject.mesh = mesh;
                    weaponObject.sprite = null;
                }
             
            }
    
         
            if(_object.objectType.Contains("part") || _object.objectType.Contains("Part"))
            {
                Debug.Log("Making part" + _object.objectName);
                part = ScriptableObject.CreateInstance<PartBase>();
                parts.Add(part);
                partsNames.Add(itemName);
            }
        }
    }
    */
}
