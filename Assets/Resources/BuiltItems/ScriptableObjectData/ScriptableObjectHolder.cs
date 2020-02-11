using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class ScriptableObjectHolder : MonoBehaviour
{
    public ScriptableObjectData data;
    void Start()
    {

        ResetValues();
    }
    public void ResetValues()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.Sprite;
            if (GetComponent<BoxCollider2D>() != null && GetComponent<SpriteRenderer>().sprite != null)
            {
                Vector2 Size = GetComponent<SpriteRenderer>().sprite.bounds.size;
                GetComponent<BoxCollider2D>().size = Size;
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            }
        }
        else if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshFilter>().mesh = data.Mesh;
        }
    }
}
