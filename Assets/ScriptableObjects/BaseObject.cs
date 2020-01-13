using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BaseObject", menuName = "", order = 1)]
public class BaseObject : ScriptableObject
{
    public string objectName;
    public MeshRenderer mesh;
    public Sprite sprite;
}
