using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
public class CreatorWindow : EditorWindow
{

    static CreatorWindow m_window;
    static string[] m_buttonNames = { "Material Builder", "Weapon Builder", "Weapon Part Builder", "Armour Builder", "Armour Part Builder","Creature Builder",
    "Creature Part Builder","Potion Builder", "Rarity Window" };
    Rect m_buttonSize;
    int m_screenID;
    bool m_loadData = true;
    Sprite m_itemTexture;
    Mesh m_itemMesh;
    Material m_itemMaterial;
    protected string currentWindowName = "";
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;
    protected Weapon weaponData;
    protected Armour armourData;
    protected int rarityID;
    protected List<ScriptableObjectData> Mats = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> WeaponParts = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> ArmourParts = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> Parts = new List<ScriptableObjectData>();
    protected UnityEngine.Object[] Rarities;
    protected List<RarityBaseData> RaritiesList = new List<RarityBaseData>();
    protected int[] PartIDs = new int[5];
    protected bool aspectMode;
    protected List<string> PartNames = new List<string>();
    protected int slotAmount;

    [MenuItem("Item + Creature Builder/Builder")]
    static void Init()
    {
        m_window = (CreatorWindow)GetWindow(typeof(CreatorWindow));
        m_window.currentWindowName = "Builder";
        m_window.maxSize = new Vector2(200, 600);
        m_window.minSize = m_window.maxSize;
        m_window.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        CreateLabel(20, new RectOffset(15, 0, 15, 0), "Item + Creature \nBuilder\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nCreated by\nEdward Dobson");
        Buttons();
        EditorGUILayout.EndHorizontal();
    }

    void Buttons()
    {
        for (int i = 0; i < m_buttonNames.Length; ++i)
        {
            m_buttonSize = new Rect(20, 95 + i * 50, 140, 30);
            if (GUI.Button(m_buttonSize, m_buttonNames[i]))
            {
                m_screenID = i + 1;
                switch (m_screenID)
                {
                    case 1:
                        m_window = (CreatorWindow)GetWindow(typeof(MaterialWindow));
                        break;
                    case 2:
                        m_window = (CreatorWindow)GetWindow(typeof(WeaponBuilderWindow));
                        break;
                    case 3:
                        m_window = (CreatorWindow)GetWindow(typeof(WeaponPartWindow));
                        break;
                    case 4:
                        m_window = (CreatorWindow)GetWindow(typeof(ArmourBuilderWindow));
                        break;
                    case 5:
                        m_window = (CreatorWindow)GetWindow(typeof(ArmourPartBuilderWindow));
                        break;
                    case 6:
                        m_window = (CreatorWindow)GetWindow(typeof(CreatureBuilderWindow));
                        break;
                    case 7:
                        m_window = (CreatorWindow)GetWindow(typeof(CreaturePartBuilderWindow));
                        break;
                    case 8:
                        m_window = (CreatorWindow)GetWindow(typeof(PotionBuilderWindow));
                        break;
                    case 9:
                        m_window = (CreatorWindow)GetWindow(typeof(RarityWindow));
                        break;
                }
                if (m_window != null)
                {
                    m_window.currentWindowName = m_buttonNames[i];
                    m_window.Show();
                }
            }
        }
    }
    //Contains the basic blocks for a window
    protected void BaseFunction()
    {
        CreateLabel(25, new RectOffset(15, 0, 15, 0), currentWindowName);
        if (m_loadData)
        {
            objectData = Resources.Load<ScriptableObjectData>("BuiltItems/ScriptableObjectData/Material");
            weaponData = Resources.Load<Weapon>("BuiltItems/ScriptableObjectData/Weapon");
            Rarities = Resources.LoadAll("BuiltItems/ScriptableObjectData/RarityData/", typeof(RarityBaseData));
            armourData = Resources.Load<Armour>("BuiltItems/ScriptableObjectData/Armour");
            foreach (RarityBaseData r in Rarities)
            {
                if (!RaritiesList.Contains(r))
                    RaritiesList.Add(r);
            }

            Debug.Log("Loading data");
            m_loadData = false;
        }
        if (currentWindowName != "Rarity Window")
        {
            CreateLabel(15, new RectOffset(15, 0, 15, 0), "Item + Creature Name");
            itemName = GUILayout.TextField(itemName);
            CreateLabel(15, new RectOffset(15, 0, 25, 0), "Description");
            itemDescription = GUILayout.TextField(itemDescription);
            GUIStyle styleB = GUI.skin.GetStyle("label");
            styleB.fontSize = 15;
            styleB.padding = new RectOffset(15, 0, 15, 0);
            if (aspectMode)
            {
                GUILayout.Label("Current Mode 3D", styleB);
                aspectMode = GUILayout.Toggle(aspectMode, "3D");
            }
            else
            {
                GUILayout.Label("Current Mode 2D", styleB);
                aspectMode = GUILayout.Toggle(aspectMode, "2D");
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
                    slotAmount = 5;
                    break;
                case "Weapon Builder":
                    slotAmount = 4;
                    break;
                case "Potion Builder":
                case "Material Builder":
                case "Weapon Part Builder":
                case "Armour Part Builder":
                    slotAmount = 1;
                    break;
                default:
                    break;

            }
        }
    }
    public void CloseButton()
    {
        GUILayout.FlexibleSpace();
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
    protected void ShowList(string _dir)
    {
        UnityEngine.Object[] m_parts = Resources.LoadAll("BuiltItems/" + _dir, typeof(ScriptableObjectData));
        List<ScriptableObjectData> _items = new List<ScriptableObjectData>();
        PartNames.Clear();
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
        }
        switch (_dir)
        {
            case "WeaponParts":
                WeaponParts = _items;
                break;
            case "ArmourParts":
                ArmourParts = _items;
                break;
            default:
                break;
        }
         
        if (!aspectMode || aspectMode)
        {
            if (_items.Count < 1)
                GUILayout.Label("You need more parts to build the item");
        }
   
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
        if(objectData != null)
        objectData.Reset();
        if (weaponData != null)
            weaponData.WeaponReset();
        if (armourData != null)
            armourData.ArmourReset();
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
    protected void BuildItem(string _dir, ItemType _type)
    {
        GameObject item = new GameObject();
        if (currentWindowName == "Weapon Builder" || currentWindowName == "Armour Builder")
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
        if (currentWindowName == "Armour Part Builder" || currentWindowName == "Weapon Part Builder" || currentWindowName == "Creature Part Builder" || currentWindowName == "Potion Builder")
        {
            if (aspectMode)
            {
                item.AddComponent<MeshFilter>().mesh = objectData.Mesh;
                item.AddComponent<MeshRenderer>().material = objectData.Mat;
                item.AddComponent<BoxCollider>();
            }
            else
            {
                item.AddComponent<SpriteRenderer>().sprite = objectData.Sprite;
                item.GetComponent<SpriteRenderer>().material = objectData.Mat;
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
                switch (_type)
                {
                    case ItemType.eWeapon:
                        itemData = Instantiate(weaponData);
                        break;
                    case ItemType.eArmour:
                        itemData = Instantiate(armourData);
                        break;
                    case ItemType.eWeaponPart:
                    case ItemType.eArmourPart:
                    case ItemType.eMaterial:
                    case ItemType.ePotion:
                    case ItemType.eCreaturePart:
                        itemData = Instantiate(objectData);
                        break;
                    default:
                        break;
                }
                if (!aspectMode)
                {
                    GameObject.Find("PartViewHolders").transform.GetChild(0).gameObject.name = "PartHolderWeapon2D";
                }
                else
                {
                    GameObject.Find("PartViewHolders").transform.GetChild(1).gameObject.name = "PartHolderWeapon3D";
                }
                itemData.AspectMode = aspectMode;

                switch (rarityID)
                {
                    case 0:
                        itemData.Rarity = Rarity.eCommon;
                        break;
                    case 1:
                        itemData.Rarity = Rarity.eUncommon;
                        break;
                    case 2:
                        itemData.Rarity = Rarity.eRare;
                        break;
                    case 3:
                        itemData.Rarity = Rarity.eEpic;
                        break;
                    case 4:
                        itemData.Rarity = Rarity.eLegendary;
                        break;
                    case 5:
                        itemData.Rarity = Rarity.eUnique;
                        break;
                    default:
                        break;
                }

                AssetDatabase.CreateAsset(itemData, "Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset");
                item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                Debug.Log("Building item");
                PrefabUtility.SaveAsPrefabAsset(item, "Assets/Resources/BuiltItems/" + _dir + "/" + itemNameCombined);
                AssetDatabase.Refresh();
            }
            if (currentWindowName == "Weapon Part Builder" || currentWindowName == "Potion Builder" || currentWindowName == "Armour Part Builder" || currentWindowName == "Creature Part Builder" || currentWindowName == "Material Builder")
            {
                DestroyImmediate(GameObject.Find(item.name));
            }
            DestroyImmediate(GameObject.Find("New Game Object"));
                ClearObjectData();


        }
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
            if (partHolderWeapon2D.activeSelf)
            {
                for (int i = 0; i < slotAmount; i++)
                {
                    if (partHolderWeapon2D.transform.GetChild(i).childCount < 1)
                    {
                        partHolderWeapon2D.transform.GetChild(i).gameObject.SetActive(true);
                        switch (currentWindowName)
                        {
                            case "Weapon Builder":
                           
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = WeaponParts[PartIDs[i]];
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
  
                                break;
                            case "Armour Builder":
                            
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ArmourParts[PartIDs[i]];
                                    partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                             
                                break;
                        }
                    }
                }
            }
            if (partHolderWeapon3D.activeSelf)
            {
                for (int i = 0; i < slotAmount; i++)
                {
                    if (partHolderWeapon3D.transform.GetChild(i).childCount < 1)
                    {
                        partHolderWeapon3D.transform.GetChild(i).gameObject.SetActive(true);
                        switch (currentWindowName)
                        {
                            case "Weapon Builder":

                                    partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = WeaponParts[PartIDs[i]];
                                    partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                            
                                break;
                            case "Armour Builder":

                                    if (ArmourParts[PartIDs[i]].AspectMode)
                                    {
                                        partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ArmourParts[PartIDs[i]];
                                        partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                                    }
                                break;
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


