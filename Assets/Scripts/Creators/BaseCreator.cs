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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public BaseObject Create(BaseObject _object)
    {
        for(int i = 0; i < amountToCreate; ++i)
        {

            _object.objectName = itemName;
            _object.dimension = dimension;
            _object.objectType = itemType;
            if (dimension == Dimension.e2D)
            {
                _object.sprite = sprite;
                _object.mesh = null;
            }
            else
            {
                _object.mesh = mesh;
                _object.sprite = null;
            }
         
        }
        return _object;



    }

}
