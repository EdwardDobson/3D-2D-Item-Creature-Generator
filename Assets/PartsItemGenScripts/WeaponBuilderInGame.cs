using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuilderInGame : MonoBehaviour
{
    public string[] weaponTypeNames  = { "Dagger", "Axe", "Sword", "LongSword", "GreatSword","Hammer"};
    public List<WeaponBase> weapons;
    public WeaponBase weapon;
    public Object[] partsAvailable2D;
    public Object[] partsAvailable3D;
    public List<string> partsAvailable2DNames;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Create(WeaponBase _object)
    {
        weapon = _object;
        weapon.weaponName = _object.weaponName;
        weapon.partTotal = _object.partTotal;
        weapon.weaponType = _object.weaponType;

    }
    public void UpdateParts()
    {
        partsAvailable2DNames.Clear();
        partsAvailable2D = null;
        partsAvailable2D = Resources.LoadAll("PartsItemGen/2D/Parts2D", typeof(WeaponPartBuilder));
        for(int i =0; i< partsAvailable2D.Length;++i)
        {
            partsAvailable2DNames.Add(partsAvailable2D[i].name);
        }
    
    }
}
