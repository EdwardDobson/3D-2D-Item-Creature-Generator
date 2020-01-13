using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPartBuilderInGame : MonoBehaviour
{
    public List<WeaponPartBuilder> Parts = new List<WeaponPartBuilder>();
    public List<string> PartNames = new List<string>();
    WeaponPartBuilder part;
    public string[] partTypes = { "Hilt", "Pummel", "Blade","Guard" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuildPart(WeaponPartBuilder _part)
    {
        part = Instantiate(_part);
        part.partName = _part.partName;
        part.partType = _part.partType;
        part.statBoost = _part.statBoost;
        part.dimension = _part.dimension;
        Parts.Add(part);
        PartNames.Add(part.partName);
    }
}
