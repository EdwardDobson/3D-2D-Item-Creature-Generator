
using UnityEngine;
[CreateAssetMenu(fileName = "Object", menuName = "BaseData", order = 2)]
public class ScriptableObjectData : ScriptableObject
{
    public Sprite Sprite;
    public Mesh Mesh;
    public float BuffValue;
    public bool IsMaterial;

}
