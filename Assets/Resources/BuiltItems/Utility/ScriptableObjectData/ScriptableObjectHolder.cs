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
        if(data != null)
        {

        
        if (GetComponent<SpriteRenderer>() != null && data != null && transform.childCount <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = data.Sprite;
            if (GetComponent<BoxCollider2D>() != null && GetComponent<SpriteRenderer>().sprite != null)
            {
                Vector2 Size = GetComponent<SpriteRenderer>().sprite.bounds.size;
                GetComponent<BoxCollider2D>().size = Size;
                GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
          
            }
        }
        else if (GetComponent<MeshRenderer>() != null && data.Mesh != null && transform.childCount <= 0)
        {
            GetComponent<MeshFilter>().mesh = data.Mesh;
                Vector3 Size = data.Mesh.bounds.size;
                GetComponent<BoxCollider>().size = Size;
       
            }
        foreach (Transform t in gameObject.transform)
        {
            if (!t.gameObject.activeSelf)
            {
                if (t.GetComponent<BoxCollider2D>() != null)
                {
                    DestroyImmediate(t.GetComponent<BoxCollider2D>());
                }
                if (t.GetComponent<SpriteRenderer>() != null)
                {
                    DestroyImmediate(t.GetComponent<SpriteRenderer>());
                }
                if (t.GetComponent<ScriptableObjectHolder>() != null)
                {
                    DestroyImmediate(t.GetComponent<ScriptableObjectHolder>());
                }
                if (t.GetComponent<MeshFilter>() != null)
                {
                    DestroyImmediate(t.GetComponent<MeshFilter>());
                }
            }
        }
        }
    }
}
