using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class BaseInGame : MonoBehaviour
{
    [HideInInspector]
    public string filePath;
    public Object[] objects;
    public List<string> itemTypesList2D = new List<string>();
    public List<string> itemTypesList3D = new List<string>();
    void OnEnable()
    {
        /*
        itemTypesList2D.Clear();
        itemTypesList3D.Clear();
        */
    }
    public void findObjects()
    {
        objects = Resources.LoadAll("PartsItemGen" + filePath, typeof(BaseObject));
        Debug.Log("Objects size" + objects.Length);
    }
  
}
