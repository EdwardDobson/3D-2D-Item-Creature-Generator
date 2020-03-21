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
    Transform m_holder;
    Vector2 m_scrollPos;
    bool m_viewItem;
    float m_windowSizeX;
    string m_cameraStateName = "Show Camera";
    int m_slotIndex;
    List<string> m_slotNames = new List<string>();
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
    protected List<string> folderNames = new List<string>();
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
    //Handles creating buttons
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
        LoadFolders();
        if (m_loadData)
        {
            objectData = Resources.Load<ScriptableObjectData>("BuiltItems/Utility/ScriptableObjectData/Material");
            Rarities = Resources.LoadAll("BuiltItems/Utility/ScriptableObjectData/RarityData/", typeof(RarityBaseData));
            itemBaseData = Resources.Load<ItemBase>("BuiltItems/Utility/ScriptableObjectData/ItemBase");
            m_holder = GameObject.Find("PartViewHolders").transform;
            foreach (RarityBaseData r in Rarities)
            {
                if (!RaritiesList.Contains(r))
                    RaritiesList.Add(r);
            }
            ViewItem(m_holder.GetChild(0).gameObject);
            Debug.Log("Loading data");
            m_loadData = false;
        }

        for (int i = 0; i < slotAmount; ++i)
        {
            if(!m_slotNames.Contains("Slot " + (i+1)))
            {
                m_slotNames.Add("Slot " + (i + 1));
            }
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
                m_saveDirIndex = EditorGUILayout.Popup("", m_saveDirIndex, folderNames.ToArray());
                break;
            }
        }
    }
    //Loads folders
    void LoadFolders()
    {
        folderNames.Clear();
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/BuiltItems/");
        DirectoryInfo[] fileNames = dir.GetDirectories();
        foreach (DirectoryInfo f in fileNames)
        {
            if (!folderNames.Contains(f.Name) && f.Name != "Utility")
            {
                folderNames.Add(f.Name);
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

            if (GUILayout.Button("Rotate Right"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.down, 15f);
                m_viewCamera.transform.LookAt(m_viewCameraTransform);
            }
            if (GUILayout.Button("Rotate Left"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.up, 15f);
                m_viewCamera.transform.LookAt(m_viewCameraTransform);
            }
            if (GUILayout.Button("Rotate Forward"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.right, 15f);
                m_viewCamera.transform.LookAt(m_viewCameraTransform);
            }
            if (GUILayout.Button("Rotate Backwards"))
            {
                m_viewCamera.transform.RotateAround(m_viewCameraTransform.position, Vector3.left, 15f);
                m_viewCamera.transform.LookAt(m_viewCameraTransform);
            }
            if (GUILayout.Button("Move Up"))
            {
                m_viewCamera.transform.position += Vector3.up * 1f;
            }
            if (GUILayout.Button("Move Down"))
            {
                m_viewCamera.transform.position -= Vector3.up * 1f;
            }
            if (GUILayout.Button("Reset Camera"))
            {
                m_viewCamera.transform.position = new Vector3(0, 0, -5);
                m_viewCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
                m_viewCamera.fieldOfView = 60;
            }
            CreateLabel(15, new RectOffset(5, 0, 0, 0), "Camera Zoom");
            m_viewCamera.orthographicSize = GUILayout.HorizontalSlider(m_viewCamera.orthographicSize, 1, 15);
            if (!aspectMode)
            {
                ViewItem(m_holder.GetChild(0).gameObject);
                m_holder.GetChild(0).gameObject.SetActive(true);
                m_holder.GetChild(1).gameObject.SetActive(false);
            }
            else if (aspectMode)
            {
                ViewItem(m_holder.GetChild(1).gameObject);
                m_holder.GetChild(0).gameObject.SetActive(false);
                m_holder.GetChild(1).gameObject.SetActive(true);
            }
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
    //Handles applying rairties to items
    protected void AssignRarity()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Rarity)).Length; ++i)
        {
            if (rarityID == i)
            {
                objectData.Rarity = (Rarity)rarityID;
            }
        }
    }
    //Handles showing each of the parts the user can use
    protected void ShowList()
    {
        UnityEngine.Object[] m_parts = Resources.LoadAll("BuiltItems/", typeof(ScriptableObjectData));
        List<ScriptableObjectData> _items = new List<ScriptableObjectData>();
        PartNames.Clear();
        Parts.Clear();
        if (Parts.Count != m_parts.Length)
        {
            Parts.Clear();
            for (int i = 0; i < m_parts.Length; ++i)
            {
                if (!Parts.Contains((ScriptableObjectData)m_parts[i]))
                    Parts.Add((ScriptableObjectData)m_parts[i]);
            }
        }
        for (int i = 0; i < Parts.Count; ++i)
        {
            if (Parts[i].AspectMode == aspectMode)
            {
                if (Parts[i].Type == m_type)
                    _items.Add(Parts[i]);
            }
        }
        for (int i = 0; i < _items.Count; ++i)
        {
            PartNames.Add(_items[i].Name);
        }
        m_slotIndex = EditorGUILayout.Popup("", m_slotIndex, m_slotNames.ToArray());
        
        for (int i = 0; i < slotAmount; ++i)
        {
            if (i == m_slotIndex && m_slotIndex <= slotAmount)
            {
                PartIDs[i] = EditorGUILayout.Popup("Slot " + (i + 1), PartIDs[i], PartNames.ToArray());
                m_itemPos[i] = EditorGUILayout.Vector3Field("Position", m_itemPos[i]);
                m_itemRotation[i] = EditorGUILayout.Vector3Field("Rotation", m_itemRotation[i]);
                m_itemScale[i] = EditorGUILayout.Vector3Field("Scale", m_itemScale[i]);
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
                if (m_itemRotation[i].x > 360 || m_itemRotation[i].x < -360)
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
                if (GUILayout.Button("Reset Slot " + (i+1)))
                {
                    ResetSingleSlotValues(i);
                }
            }
         
        }
        if (GUILayout.Button("Reset All Slots"))
        {
            ResetSlotValues();
        }
        ItemBaseParts = _items;
        if (!aspectMode || aspectMode)
        {
            if (_items.Count < 1)
                GUILayout.Label("You need more parts to build the item");
        }
    }
    //Resets the parameters of the slots
    void ResetSlotValues()
    {
        for (int i = 0; i < slotAmount; ++i)
        {
            m_itemScale[i] = new Vector3(1, 1, 1);
            m_itemRotation[i] = new Vector3(0, 0, 0);
            m_itemPos[i] = new Vector3(0, 0, 0);
        }
    }
    //Resets only a single slot
    void ResetSingleSlotValues(int _index)
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
                if (_ItemStruct.isFullItem)
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
                AssetDatabase.CreateAsset(itemData, "Assets/Resources/BuiltItems/" + folderNames[m_saveDirIndex] + "/" + itemName + ".asset");
                item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + folderNames[m_saveDirIndex] + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                Debug.Log("Building item");
                PrefabUtility.SaveAsPrefabAsset(item, "Assets/Resources/BuiltItems/" + folderNames[m_saveDirIndex] + "/" + itemNameCombined);
                AssetDatabase.Refresh();
                for (int i = 0; i < m_itemPos.Length; ++i)
                {
                    m_itemPos[i] = new Vector3(0, 0, 0);
                }
            }
            if (currentWindowName.Contains("Part") || currentWindowName == "Potion Builder" || currentWindowName == "Material Builder")
            {
                DestroyImmediate(GameObject.Find(item.name));
            }

            ResetSlotValues();

            DestroyImmediate(GameObject.Find("New Game Object"));
            ClearObjectData();
        }
    }
    public void CreateFolder(string _folderName)
    {
        AssetDatabase.CreateFolder("Assets/Resources/Builtitems", _folderName);
        folderNames.Add(_folderName);
        Debug.Log("Building folder : " + _folderName);
        Debug.Log("Folder Count: " + folderNames.Count);
        LoadFolders();
    }
    public void DeleteFolder(string _folderName)
    {
        LoadFolders();
        if (folderNames.Count > 1)
        {
            AssetDatabase.DeleteAsset("Assets/Resources/Builtitems/" + _folderName);
            folderNames.Remove(_folderName);
        }
    }
    //Handles the viewing of each slot
    protected void ViewItem(GameObject _holderTransform)
    {
        Camera camera = GameObject.Find("ItemViewCamera").GetComponent<Camera>();
        if (camera != null)
        {
            Texture view = camera.activeTexture;
            GUILayout.Label(view);
            foreach (Transform t in _holderTransform.transform)
            {
                if (t.GetSiblingIndex() < slotAmount)
                {
                   t.gameObject.SetActive(true);
                }
                else
                {
                    t.gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < slotAmount; ++i)
            {
                if (!currentWindowName.Contains("Part") && ItemBaseParts.Count > 0)
                {
                    _holderTransform.transform.GetChild(i).transform.position = m_itemPos[i];
                    if (m_itemScale[i].x >= 1 || m_itemScale[i].y >= 1 || m_itemScale[i].z >= 1)
                    {
                        _holderTransform.transform.GetChild(i).transform.localScale = m_itemScale[i];
                    }
                    _holderTransform.transform.GetChild(i).localEulerAngles = new Vector3(m_itemRotation[i].x, m_itemRotation[i].y, m_itemRotation[i].z);
                    _holderTransform.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ItemBaseParts[PartIDs[i]];
                    _holderTransform.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                }
                if (_holderTransform.name == "PartHolder3D")
                {
                    if (_holderTransform.transform.GetChild(i).GetComponent<BoxCollider>() == null)
                        _holderTransform.transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                    if (_holderTransform.transform.GetChild(i).GetComponent<MeshFilter>() == null)
                        _holderTransform.transform.GetChild(i).gameObject.AddComponent<MeshFilter>();
                    if (_holderTransform.transform.GetChild(i).GetComponent<MeshRenderer>() == null)
                        _holderTransform.transform.GetChild(i).gameObject.AddComponent<MeshRenderer>();
                }
                else
                {
                    if (_holderTransform.transform.GetChild(i).GetComponent<BoxCollider2D>() == null)
                        _holderTransform.transform.GetChild(i).gameObject.AddComponent<BoxCollider2D>();

                    if (_holderTransform.transform.GetChild(i).GetComponent<SpriteRenderer>() == null)
                        _holderTransform.transform.GetChild(i).gameObject.AddComponent<SpriteRenderer>();
                }
                if (_holderTransform.transform.GetChild(i).GetComponent<ScriptableObjectHolder>() == null)
                    _holderTransform.transform.GetChild(i).gameObject.AddComponent<ScriptableObjectHolder>();
                if (i == m_slotIndex)
                {
                    if (_holderTransform.transform.GetChild(m_slotIndex).GetComponent<SpriteRenderer>() != null)
                    {
                        _holderTransform.transform.GetChild(m_slotIndex).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                    }
                    else if (_holderTransform.transform.GetChild(m_slotIndex).GetComponent<MeshRenderer>() != null)
                    {
                        _holderTransform.transform.GetChild(m_slotIndex).gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", Color.red);
                    }
                }
                else
                {
                    if (_holderTransform.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    {
                        _holderTransform.transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                    }
                    else if (_holderTransform.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                    {
                        _holderTransform.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", Color.white);
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


