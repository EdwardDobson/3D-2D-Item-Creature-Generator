using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Dimension
{
    e2D,
    e3D,
}

[CreateAssetMenu(fileName = "BaseObject", menuName = "", order = 1)]
public class BaseObject : ScriptableObject
{
    public string objectName;
    public string objectType;
    public MeshFilter mesh;
    public Sprite sprite;
    public Dimension dimension;
}
