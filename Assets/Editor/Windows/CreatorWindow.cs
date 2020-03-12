using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
[ExecuteInEditMode]
public class CreatorWindow : EditorWindow
{
    static CreatorWindow m_window;
    static string[] m_buttonNames = { "Material Builder", "Weapon Builder", "Weapon Part Builder", "Armour Builder", "Armour Part Builder","Creature Builder",
    "Creature Part Builder","Potion Builder", "Rarity Window" , "Folder Window" };
    List<string> m_folderNames = new List<string>();
    int m_saveDirIndex;
    Rect m_buttonSize;
    int m_screenID;
    bool m_loadData = true;
    Sprite m_itemTexture;
    Mesh m_itemMesh;
    Material m_itemMaterial;
    string m_aspectName;
    ItemType m_type;
    Vector3[] m_itemPos = new Vector3[6];
    Vector3[] m_itemScale = new Vector3[6];
    Vector3[] m_itemRotation = new Vector3[6];
    Camera m_viewCamera;
    Transform m_viewCameraTransform;
    Vector2 m_scrollPos;
    bool m_viewItem;
    float m_windowSizeX;
    string m_cameraStateName = "Show Camera";
    protected string currentWindowName = "";
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;
    protected ItemBase itemBaseData;
    protected int rarityID;
    protected List<ScriptableObjectData> Parts = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> ItemBaseParts = new List<ScriptableObjectData>();
    protected UnityEngine.Object[] Rarities;
    protected List<RarityBaseData> RaritiesList = new List<RarityBaseData>();
    protected int[] PartIDs = new int[6];
    protected bool aspectMode;
    protected List<string> PartNames = new List<string>();
    protected int slotAmount;

    [MenuItem("Item + Creature Builder/Builder")]
    static void Init()
    {
        m_window = (CreatorWindow)GetWindow(typeof(CreatorWindow));
        m_window.currentWindowName = "Builder";
        m_window.maxSize = new Vector2(250, 700);
        m_window.minSize = m_window.maxSize;
        m_window.Show();
    }
    void OnGUI()
    {
        CreateLabel(15, new RectOffset(35, 0, 15, 0), "Item + Creature Builder");
        CreateLabel(15, new RectOffset(18, 0, 635, 0), "Created by Edward Dobson");
        Buttons();
    }

    void Buttons()
    {
        for (int i = 0; i < m_buttonNames.Length; ++i)
        {
            m_buttonSize = new Rect(50, 95 + i * 50, 140, 30);
            if (GUI.Button(m_buttonSize, m_buttonNames[i]))
            {
                m_screenID = i + 1;
                switch (m_screenID)
                {
                    case 1:
                        m_window = (MaterialWindow)GetWindow(typeof(MaterialWindow));
                        break;
                    case 2:
                        m_window = (WeaponBuilderWindow)GetWindow(typeof(WeaponBuilderWindow));
                        break;
                    case 3:
                        m_window = (WeaponPartWindow)GetWindow(typeof(WeaponPartWindow));
                        break;
                    case 4:
                        m_window = (ArmourBuilderWindow)GetWindow(typeof(ArmourBuilderWindow));

                        break;
                    case 5:
                        m_window = (ArmourPartBuilderWindow)GetWindow(typeof(ArmourPartBuilderWindow));
                        break;
                    case 6:
                        m_window = (CreatureBuilderWindow)GetWindow(typeof(CreatureBuilderWindow));
                        break;
                    case 7:
                        m_window = (CreaturePartBuilderWindow)GetWindow(typeof(CreaturePartBuilderWindow));
                        break;
                    case 8:
                        m_window = (PotionBuilderWindow)GetWindow(typeof(PotionBuilderWindow));
                        break;
                    case 9:
                        m_window = (RarityWindow)GetWindow(typeof(RarityWindow));
                        break;
                    case 10:
                        m_window = (FolderWindow)GetWindow(typeof(FolderWindow));
                        break;
                }
   
                if (m_window != null)
                {
                    m_window.currentWindowName = m_buttonNames[i];
                    m_windowSizeX = m_window.maxSize.x;
                    m_window.Show();
                }
            }
       
        }
    }
    //Contains the basic blocks for a window
    protected void BaseFunction()
    {
      
        CreateLabel(25, new RectOffset(5, 0, 5, 0), currentWindowName);
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/BuiltItems/");
        DirectoryInfo[] fileNames = dir.GetDirectories();
        foreach (DirectoryInfo f in fileNames)
        {
            if(!m_folderNames.Contains(f.Name) && f.Name != "Utility")
            {
                m_folderNames.Add(f.Name);
            }
         
        }
        if (m_loadData)
        {
            objectData = Resources.Load<ScriptableObjectData>("BuiltItems/Utility/ScriptableObjectData/Material");
            Rarities = Resources.LoadAll("BuiltItems/Utility/ScriptableObjectData/RarityData/", typeof(RarityBaseData));
            itemBaseData = Resources.Load<ItemBase>("BuiltItems/Utility/ScriptableObjectData/ItemBase");
            foreach (RarityBaseData r in Rarities)
            {
                if (!RaritiesList.Contains(r))
                    RaritiesList.Add(r);
            }

            Debug.Log("Loading data");
            m_loadData = false;
        }
        for (int i = 0; i < m_buttonNames.Length - 2; ++i)
        {
            if (m_buttonNames[i].Contains(currentWindowName))
            {
                CreateLabel(15, new RectOffset(5, 0, 15, 0), "Item + Creature Name");
                itemName = GUILayout.TextField(itemName);
                CreateLabel(15, new RectOffset(5, 0, 25, 0), "Description");
                itemDescription = GUILayout.TextField(itemDescription);
                CreateLabel(15, new RectOffset(5, 0, 15, 0), "Current aspect mode " + m_aspectName);

                if (aspectMode)
                {
                    m_aspectName = "3D";
                }
                else
                {
                    m_aspectName = "2D";
                }
                if (GUILayout.Button("Change Aspect Mode"))
                {
                    if (aspectMode)
                    {
                        aspectMode = false;
                    }
                    else
                    {
                        aspectMode = true;
                    }
                    PartIDs = new int[slotAmount];
                }

                rarityID = EditorGUILayout.Popup("Rarity", rarityID, Enum.GetNames(typeof(Rarity)));
                if (currentWindowName == "Weapon Part Builder" || currentWindowName == "Armour Part Builder" || currentWindowName == "Material Builder"
                    || currentWindowName == "Creature Part Builder" || currentWindowName == "Potion Builder")
                {
                    DisplayItemSpriteMesh();

                }
                switch (currentWindowName)
                {
                    case "Armour Builder":
                        m_type = ItemType.eArmourPart;
                        slotAmount = 5;
                        break;
                    case "Weapon Builder":
                        m_type = ItemType.eWeaponPart;
                        slotAmount = 4;
                        break;
                    case "Creature Builder":
                        m_type = ItemType.eCreaturePart;
                        slotAmount = 6;
                        break;
                    case "Potion Builder":
                    case "Material Builder":
                    case "Weapon Part Builder":
                    case "Armour Part Builder":
                        m_type = ItemType.eMaterial;
                        slotAmount = 1;
                        break;
                    default:
                        break;

                }
                CreateLabel(15, new RectOffset(5, 0, 15, 0), "Save To:");
                m_saveDirIndex = EditorGUILayout.Popup("", m_saveDirIndex, m_folderNames.ToArray());
                break;

            }
        }
    }
    protected void ScrollbarStart()
    {
        EditorGUILayout.BeginHorizontal();
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true, GUILayout.Width(m_windowSizeX), GUILayout.MinHeight(200), GUILayout.MaxHeight(1000), GUILayout.ExpandHeight(true));
    }
    protected void EndView()
    {
        EditorGUILayout.EndHorizontal();
        GUILayout.EndScrollView();
    }
    protected void Camera()
    {
        if (GUILayout.Button(m_cameraStateName))
        {
            if (m_viewItem)
            {
                m_viewItem = false;
                m_cameraStateName = "Show Camera";
            }
            else
            {
                m_viewItem = true;
                m_cameraStateName = "Hide Camera";
            }

        }
        m_viewCamera = GameObject.Find("ItemViewCamera").GetComponent<Camera>();
        m_viewCameraTransform = GameObject.Find("ViewCenter").transform;
        if (m_viewItem)
        {
            m_viewCamera.transform.LookAt(m_viewCameraTransform);
            if (GUILayout.Button("Rotate Right"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.down, 15f);
            }
            if (GUILayout.Button("Rotate Left"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.up, 15f);
            }
            if (GUILayout.Button("Rotate Forward"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.right, 15f);
            }
            if (GUILayout.Button("Rotate Backwards"))
            {
                
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.left, 15f);
            }
            if (GUILayout.Button("Reset Camera"))
            {
                m_viewCamera.transform.position = new Vector3(0, 0, -5);
                m_viewCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
                m_viewCamera.fieldOfView = 60;
            }
            CreateLabel(15, new RectOffset(5, 0, 0, 0), "Camera Zoom");
            m_viewCamera.orthographicSize = GUILayout.HorizontalSlider(m_viewCamera.orthographicSize, 1,15);
       
            ViewItem();
        }
  
    }
    public void CloseButton()
    {
        if (GUILayout.Button("Close"))
        {
            m_screenID = 0;
            Debug.Log("Closing window");
            Close();
        }
    }
 
    protected void AssignRarity()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Rarity)).Length; i++)
        {
            if (rarityID == i)
            {
                objectData.Rarity = (Rarity)rarityID;
            }
        }
    }
    protected void ShowList()
    {

        UnityEngine.Object[] m_parts = Resources.LoadAll("BuiltItems/", typeof(ScriptableObjectData));
        List<ScriptableObjectData> _items = new List<ScriptableObjectData>();
      
        PartNames.Clear();
        Parts.Clear();
        if (Parts.Count != m_parts.Length)
        {
            Parts.Clear();
            for (int i = 0; i < m_parts.Length; i++)
            {
                if (!Parts.Contains((ScriptableObjectData)m_parts[i]))
                    Parts.Add((ScriptableObjectData)m_parts[i]);
            }
        }
        for (int i = 0; i < Parts.Count; i++)
        {
            if (Parts[i].AspectMode == aspectMode)
            {
                if(Parts[i].Type == m_type)
                _items.Add(Parts[i]);
            }
              
        }
        for (int i = 0; i < _items.Count; i++)
        {
            PartNames.Add(_items[i].Name);
        }
        for (int i = 0; i < slotAmount; i++)
        {
            PartIDs[i] = EditorGUILayout.Popup("Slot " + (i + 1), PartIDs[i], PartNames.ToArray());
            m_itemPos[i] = EditorGUILayout.Vector3Field("", m_itemPos[i]);
            m_itemRotation[i] = EditorGUILayout.Vector3Field("", m_itemRotation[i]);
            m_itemScale[i] = EditorGUILayout.Vector3Field("", m_itemScale[i]);
            if (m_itemScale[i].x < 1)
            {
                m_itemScale[i].x = 1;
            }
            if (m_itemScale[i].y < 1)
            {
                m_itemScale[i].y = 1;
            }
            if (m_itemScale[i].z < 1)
            {
                m_itemScale[i].z = 1;
            }
            if(m_itemRotation[i].x > 360 || m_itemRotation[i].x < -360)
            {
                m_itemRotation[i].x = 0;
            }
            if (m_itemRotation[i].y > 360 || m_itemRotation[i].y < -360)
            {
                m_itemRotation[i].y = 0;
            }
            if (m_itemRotation[i].z > 360 || m_itemRotation[i].z < -360)
            {
                m_itemRotation[i].z = 0;
            }
            if (GUILayout.Button("Reset Slot " + (i + 1)))
            {
                ResetSlotValues(i);
            }
         
        }
        ItemBaseParts = _items;
        if (!aspectMode || aspectMode)
        {
            if (_items.Count < 1)
                GUILayout.Label("You need more parts to build the item");
        }

    }
    //Resets the parameters of the slots
    void ResetSlotValues(int _index)
    {
   
        m_itemScale[_index] = new Vector3(1, 1, 1);
        m_itemRotation[_index] = new Vector3(0, 0, 0);
        m_itemPos[_index] = new Vector3(0, 0, 0);
    }
    public void CreateLabel(int _fontSize, RectOffset _rect, string _labelText)
    {
        GUIStyle style = GUI.skin.GetStyle("label");
        style.fontSize = _fontSize;
        style.padding = _rect;
        GUILayout.Label(_labelText, style);
    }
    //Resets the values in the base scriptable objects to avoid old persisting data
    protected void ClearObjectData()
    {
        if (objectData != null)
            objectData.Reset();
        if (itemBaseData != null)
            itemBaseData.ItemBaseReset();
        itemDescription = "";
        itemName = "";
    }

    //Adds a slot to the editor to add meshes and sprites to objects
    protected void DisplayItemSpriteMesh()
    {
        if (aspectMode)
        {
            objectData.Sprite = null;
            m_itemTexture = null;
            m_itemMesh = (Mesh)EditorGUILayout.ObjectField("Mesh", m_itemMesh, typeof(Mesh), true);
            objectData.Mesh = m_itemMesh;
        }
        else
        {
            objectData.Mesh = null;
            m_itemMesh = null;
            m_itemTexture = (Sprite)EditorGUILayout.ObjectField("Sprite", m_itemTexture, typeof(Sprite), true);
            objectData.Sprite = m_itemTexture;
        }
        m_itemMaterial = (Material)EditorGUILayout.ObjectField("Material", m_itemMaterial, typeof(Material), true);
        objectData.Mat = m_itemMaterial;
    }
    //Handles building items/creature parts
    protected void BuildItem(string _dir, ItemTypeStruct _ItemStruct)
    {
        GameObject item = new GameObject();
        if (currentWindowName == "Weapon Builder" || currentWindowName == "Armour Builder" || currentWindowName == "Creature Builder")
        {
            if (!aspectMode)
            {
                item = GameObject.Find("PartViewHolders").transform.GetChild(0).gameObject;
            }
            else if (aspectMode)
            {
                item = GameObject.Find("PartViewHolders").transform.GetChild(1).gameObject;
            }
        }
      
        if (currentWindowName.Contains("Part") || currentWindowName == "Potion Builder" || currentWindowName == "Material Builder")
        {
            if (aspectMode)
            {
                if (item.GetComponent<MeshFilter>() == null)
                    item.AddComponent<MeshFilter>().mesh = objectData.Mesh;
                if (item.GetComponent<MeshRenderer>() == null)
                    item.AddComponent<MeshRenderer>().material = objectData.Mat;
                if (item.GetComponent<BoxCollider>() == null)
                    item.AddComponent<BoxCollider>();
            }
            else
            {
                if (item.GetComponent<SpriteRenderer>() == null)
                    item.AddComponent<SpriteRenderer>().sprite = objectData.Sprite;
                item.GetComponent<SpriteRenderer>().material = objectData.Mat;
                if (item.GetComponent<BoxCollider2D>() == null)
                    item.AddComponent<BoxCollider2D>();
            }
            item.AddComponent<ScriptableObjectHolder>().data = objectData;
        }
        item.name = itemName;
        string itemNameCombined = itemName + ".prefab";
        if (itemNameCombined != "" && objectData != null)
        {
            if (item.GetComponent<ScriptableObjectHolder>() != null)
            {
                ScriptableObjectData itemData = CreateInstance<ScriptableObjectData>();
                if(_ItemStruct.isFullItem)
                {
                    itemData = Instantiate(itemBaseData);
                }
                else
                {
                    itemData = Instantiate(objectData);
                }
              
                if (!aspectMode)
                {
                    GameObject.Find("PartViewHolders").transform.GetChild(0).gameObject.name = "PartHolder2D";
                }
                else
                {
                    GameObject.Find("PartViewHolders").transform.GetChild(1).gameObject.name = "PartHolder3D";
                }
                itemData.AspectMode = aspectMode;
                foreach (int i in Enum.GetValues(typeof(Rarity)))
                {
                    if (rarityID == i)
                    {
                        itemData.Rarity = (Rarity)i;
                    }
                }
                AssetDatabase.CreateAsset(itemData, "Assets/Resources/BuiltItems/" + m_folderNames[m_saveDirIndex] + "/" + itemName + ".asset");
                item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + m_folderNames[m_saveDirIndex] + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                Debug.Log("Building item");
                PrefabUtility.SaveAsPrefabAsset(item, "Assets/Resources/BuiltItems/" + m_folderNames[m_saveDirIndex] + "/" + itemNameCombined);
                AssetDatabase.Refresh();
                for(int i =0; i < m_itemPos.Length; ++i)
                {
                    m_itemPos[i] = new Vector3(0, 0, 0);
                }
            }
            if (currentWindowName.Contains("Part") || currentWindowName == "Potion Builder" || currentWindowName == "Material Builder")
            {
                DestroyImmediate(GameObject.Find(item.name));
            }
            for(int i =0; i < slotAmount; ++i)
            {
                ResetSlotValues(i);
            }
            DestroyImmediate(GameObject.Find("New Game Object"));
            ClearObjectData();
        }
    }
    public void CreateFolder(string _folderName)
    {
        AssetDatabase.CreateFolder("Assets/Resources/Builtitems", _folderName);
        string newFolderPath = AssetDatabase.GUIDToAssetPath(_folderName);
        m_folderNames.Add(_folderName);
        Debug.Log("Building folder : " + _folderName);
        Debug.Log("Folder Count: " + m_folderNames.Count);
    }
    protected void ViewItem()
    {
        Camera camera = GameObject.Find("ItemViewCamera").GetComponent<Camera>();
        if (camera != null)
        {
            Texture view = camera.activeTexture;
            GUILayout.Label(view);
            Transform holder = GameObject.Find("PartViewHolders").transform;
            GameObject partHolderWeapon2D = holder.GetChild(0).gameObject;
            GameObject partHolderWeapon3D = holder.GetChild(1).gameObject;
            for (int i = 0; i < partHolderWeapon2D.transform.childCount; i++)
            {
                if (partHolderWeapon2D.transform.GetChild(i).childCount < 1)
                {
                    partHolderWeapon2D.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < partHolderWeapon3D.transform.childCount; i++)
            {
                if (partHolderWeapon3D.transform.GetChild(i).childCount < 1)
                {
                    partHolderWeapon3D.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            if (!aspectMode)
            {
                partHolderWeapon2D.SetActive(true);
                partHolderWeapon3D.SetActive(false);
            }
            else
            {
                partHolderWeapon3D.SetActive(true);
                partHolderWeapon2D.SetActive(false);
            }

            for (int i = 0; i < partHolderWeapon3D.transform.childCount; i++)
            {
              
                    if (partHolderWeapon3D.transform.GetChild(i).GetComponent<BoxCollider>() == null)
                        partHolderWeapon3D.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                    if (partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>() == null)
                        partHolderWeapon3D.transform.GetChild(i).gameObject.AddComponent<ScriptableObjectHolder>();
                    if (partHolderWeapon3D.transform.GetChild(i).GetComponent<MeshFilter>() == null)
                        partHolderWeapon3D.transform.GetChild(i).gameObject.AddComponent<MeshFilter>();
                    if (partHolderWeapon3D.transform.GetChild(i).GetComponent<MeshRenderer>() == null)
                        partHolderWeapon3D.transform.GetChild(i).gameObject.AddComponent<MeshRenderer>();
              
            }
            for (int i = 0; i < partHolderWeapon2D.transform.childCount; i++)
            {

                if (partHolderWeapon2D.transform.GetChild(i).GetComponent<BoxCollider2D>() == null)
                    partHolderWeapon2D.transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();
                if (partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>() == null)
                    partHolderWeapon2D.transform.GetChild(i).gameObject.AddComponent<ScriptableObjectHolder>();
                if (partHolderWeapon2D.transform.GetChild(i).GetComponent<SpriteRenderer>() == null)
                    partHolderWeapon2D.transform.GetChild(i).gameObject.AddComponent<SpriteRenderer>();

            }
            for (int i = 0; i < slotAmount; i++)
            {
                if (aspectMode)
                {
                    if (partHolderWeapon3D.transform.GetChild(i).childCount < 1)
                    {
                        partHolderWeapon3D.transform.GetChild(i).gameObject.SetActive(true);
                        if (!currentWindowName.Contains("Part"))
                        {
                            if (ItemBaseParts.Count > 0)
                            {
                                partHolderWeapon3D.transform.GetChild(i).transform.position = m_itemPos[i];
                                if(m_itemScale[i].x >= 1 || m_itemScale[i].y >= 1 || m_itemScale[i].z >= 1)
                                {
                                    partHolderWeapon3D.transform.GetChild(i).transform.localScale = m_itemScale[i];
                                }
                                partHolderWeapon3D.transform.GetChild(i).localEulerAngles = new Vector3(m_itemRotation[i].x, m_itemRotation[i].y, m_itemRotation[i].z);
                                partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ItemBaseParts[PartIDs[i]];
                                partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                            }
                           
                        }
                    }
                }
                else
                {
                    if (partHolderWeapon2D.activeSelf)
                    {
                        if (partHolderWeapon2D.transform.GetChild(i).childCount < 1)
                        {
                            partHolderWeapon2D.transform.GetChild(i).gameObject.SetActive(true);
                            if (!currentWindowName.Contains("Part"))
                            {
                                if (ItemBaseParts.Count > 0)
                                {
                                    partHolderWeapon2D.transform.GetChild(i).transform.position = m_itemPos[i];
                                    if (m_itemScale[i].x >= 1 || m_itemScale[i].y >= 1 || m_itemScale[i].z >= 1)
                                    {
                                        partHolderWeapon2D.transform.GetChild(i).transform.localScale = m_itemScale[i];
                                    }
                                    partHolderWeapon2D.transform.GetChild(i).localEulerAngles = new Vector3(m_itemRotation[i].x, m_itemRotation[i].y, m_itemRotation[i].z);
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ItemBaseParts[PartIDs[i]];
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void OnDestroy()
    {
        ClearObjectData();
        Debug.Log("Window Force Closed");
       
    }
}


