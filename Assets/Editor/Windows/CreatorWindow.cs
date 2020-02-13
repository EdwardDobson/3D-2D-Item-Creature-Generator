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
    UnityEngine.Object[] m_parts;
    Sprite itemTexture;
    Mesh itemMesh;
    Material itemMaterial;
    Material itemMaterial2D;

    protected string currentWindowName = "";
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;
    protected Weapon weaponData;
    protected Armour armourData;
    protected int[] materialID = new int[1];
    protected int rarityID;
    protected List<ScriptableObjectData> Mats = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> WeaponParts = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> ArmourParts = new List<ScriptableObjectData>();
    protected UnityEngine.Object[] Rarities;
    protected List<RarityBaseData> RaritiesList = new List<RarityBaseData>();
    protected int[] WeaponPartsID = new int[4];
    protected int[] ArmourPartsID = new int[5];
    protected List<string> ArmourPartNames = new List<string>();
    protected bool aspectMode;
    protected List<string> ArmourPartNames3D = new List<string>();
    protected List<string> MatNames = new List<string>();
    protected List<string> MatNames3D = new List<string>();
    protected List<string> WeaponPartNames = new List<string>();
    protected List<string> WeaponPartNames3D = new List<string>();
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
            GUIStyle styleB = new GUIStyle(GUI.skin.GetStyle("label"));
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
                || currentWindowName == "Creature Part Builder")
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
    protected void ShowList(string _dir, List<ScriptableObjectData> _data, List<string> _partNames2D, List<string> _partNames3D, int[] _partIDs)
    {
        m_parts = Resources.LoadAll("BuiltItems/" + _dir, typeof(ScriptableObjectData));
        for (int i = 0; i < m_parts.Length; i++)
        {
            if (!_data.Contains((ScriptableObjectData)m_parts[i]))
                _data.Add((ScriptableObjectData)m_parts[i]);
        }
        string[] PartsNames = Array.ConvertAll(m_parts, x => x.ToString());
        for (int i = 0; i < PartsNames.Length; i++)
        {
            if (!_data[i].AspectMode)
                if (!_partNames2D.Contains(PartsNames[i]))
                    _partNames2D.Add(PartsNames[i].Substring(0, PartsNames[i].IndexOf("(")));
                else
                if (!_partNames3D.Contains(PartsNames[i]))
                    _partNames3D.Add(PartsNames[i].Substring(0, PartsNames[i].IndexOf("(")));
        }
        for (int i = 0; i < _partIDs.Length; i++)
        {
            if (_data.Count >= _partIDs.Length)
            {
                if (!aspectMode)
                    _partIDs[i] = EditorGUILayout.Popup("Slot " + (i + 1), _partIDs[i], _partNames2D.ToArray());
                else
                    _partIDs[i] = EditorGUILayout.Popup("Slot " + (i + 1), _partIDs[i], _partNames3D.ToArray());
            }
        }
        if (!aspectMode)
        {
            if (_data.Count < _partIDs.Length)
            {
                GUILayout.Label("You need more parts to build the item");
            }
        }
        if (aspectMode)
        {
            if (_data.Count < _partIDs.Length)
            {
                GUILayout.Label("You need more parts to build the item");
            }
        }

        if (_partNames2D.Count > _data.Count)
            _partNames2D.Clear();
        if (_partNames3D.Count > _data.Count)
            _partNames3D.Clear();
    }
    protected void ShowWeaponPartsList()
    {
        m_parts = Resources.LoadAll("BuiltItems/WeaponParts", typeof(ScriptableObjectData));
        for (int i = 0; i < m_parts.Length; i++)
        {
            if (!WeaponParts.Contains((ScriptableObjectData)m_parts[i]))
                WeaponParts.Add((ScriptableObjectData)m_parts[i]);
        }
        string[] PartsNames = Array.ConvertAll(m_parts, x => x.ToString());
        for (int i = 0; i < PartsNames.Length; i++)
        {
            if (!WeaponParts[i].AspectMode)
            {
                if (!WeaponPartNames.Contains(PartsNames[i]))
                    WeaponPartNames.Add(PartsNames[i].Substring(0, PartsNames[i].IndexOf("(")));
            }
            if (WeaponParts[i].AspectMode)
            {
                if (!WeaponPartNames3D.Contains(PartsNames[i]))
                    WeaponPartNames3D.Add(PartsNames[i].Substring(0, PartsNames[i].IndexOf("(")));
            }
        }
        switch (weaponData.WeaponType)
        {
            case WeaponType.eSword:
                if (!aspectMode)
                {
                    WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], WeaponPartNames.ToArray());
                    WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], WeaponPartNames.ToArray());
                    WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], WeaponPartNames.ToArray());
                    WeaponPartsID[3] = EditorGUILayout.Popup("Slot 4", WeaponPartsID[3], WeaponPartNames.ToArray());
                }
                else
                {
                    WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], WeaponPartNames3D.ToArray());
                    WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], WeaponPartNames3D.ToArray());
                    WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], WeaponPartNames3D.ToArray());
                    WeaponPartsID[3] = EditorGUILayout.Popup("Slot 4", WeaponPartsID[3], WeaponPartNames3D.ToArray());
                }
                break;
            case WeaponType.eSpear:
            case WeaponType.eMace:
            case WeaponType.eAxe:
                if (!aspectMode)
                {
                    WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], WeaponPartNames.ToArray());
                    WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], WeaponPartNames.ToArray());
                    WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], WeaponPartNames.ToArray());
                }
                else
                {
                    WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], WeaponPartNames3D.ToArray());
                    WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], WeaponPartNames3D.ToArray());
                    WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], WeaponPartNames3D.ToArray());
                }
                break;
        }
        if (WeaponPartNames.Count > WeaponParts.Count)
            WeaponPartNames.Clear();
    }

    public void CreateLabel(int _fontSize, RectOffset _rect, string _labelText)
    {
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = _fontSize;
        style.padding = _rect;
        GUILayout.Label(_labelText, style);
    }
    //Resets the values in the base scriptable objects to avoid old persisting
    protected void ClearObjectData()
    {
        objectData.Name = "";
        objectData.Description = "";
        objectData.Sprite = null;
        objectData.Mesh = null;
        objectData.Type = ItemType.eMaterial;
        weaponData.Rarity = Rarity.eCommon;
        objectData.BuffValueMaterial = 0;
        objectData.BuffValuePart = 0;
        weaponData.Name = "";
        weaponData.Description = "";
        weaponData.Sprite = null;
        weaponData.Mesh = null;
        weaponData.Type = ItemType.eWeapon;
        weaponData.Rarity = Rarity.eCommon;
        weaponData.BuffValueMaterial = 0;
        weaponData.BuffValuePart = 0;
        weaponData.BuffValuePart2 = 0;
        weaponData.Slot1 = null;
        weaponData.Slot2 = null;
        weaponData.Slot3 = null;
        weaponData.Slot4 = null;
        weaponData.AttackSpeed = 0;
    }

    //Adds a slot to the editor to add meshes and sprites to objects
    protected void DisplayItemSpriteMesh()
    {
        if (aspectMode)
        {
            itemTexture = null;
            itemMesh = (Mesh)EditorGUILayout.ObjectField("Mesh", itemMesh, typeof(Mesh), true);
            objectData.Mesh = itemMesh;
            objectData.Mat = itemMaterial;
        }
        else
        {
            itemMesh = null;
            itemTexture = (Sprite)EditorGUILayout.ObjectField("Sprite", itemTexture, typeof(Sprite), true);
            objectData.Sprite = itemTexture;
            objectData.Mat = itemMaterial;
        }
        itemMaterial = (Material)EditorGUILayout.ObjectField("Material", itemMaterial, typeof(Material), true);
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
        item.name = itemName;
        string itemNameCombined = itemName + ".prefab";
        if (itemNameCombined != null && objectData != null)
        {
            if (currentWindowName == "Weapon Part Builder" || currentWindowName == "Armour Part Builder" || currentWindowName == "Material Builder")
            {
                if (aspectMode)//3D Mode
                {
                    item.AddComponent<BoxCollider>();
                    item.AddComponent<MeshFilter>();
                    item.AddComponent<MeshRenderer>();
                    item.GetComponent<MeshFilter>().mesh = objectData.Mesh;
                    item.GetComponent<MeshRenderer>().material = objectData.Mat;
                }
                else//2D Mode
                {
                    item.AddComponent<BoxCollider2D>();
                    item.AddComponent<SpriteRenderer>();
                    item.GetComponent<SpriteRenderer>().sprite = objectData.Sprite;
                    item.GetComponent<SpriteRenderer>().material = objectData.Mat;
                }
                item.AddComponent<ScriptableObjectHolder>();
            }
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
                        itemData = Instantiate(objectData);
                        break;
                    case ItemType.eArmourPart:
                        itemData = Instantiate(objectData);
                        break;
                    case ItemType.eMaterial:
                        itemData = Instantiate(objectData);
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
                AssetDatabase.CreateAsset(itemData, "Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset");
                item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                foreach (Transform g in item.transform)
                {
                    if (!g.gameObject.activeSelf)
                    {
                        DestroyImmediate(g.gameObject);
                    }
                }
                PrefabUtility.SaveAsPrefabAsset(item, "Assets/Resources/BuiltItems/" + _dir + "/" + itemNameCombined);
                AssetDatabase.Refresh();
                ClearObjectData();
            }
            DestroyImmediate(GameObject.Find("New Game Object"));
            DestroyImmediate(GameObject.Find(itemName));
        }
    }
    protected void ViewItem()
    {
        Camera camera = GameObject.Find("ItemViewCamera").GetComponent<Camera>();

        if (camera != null)
        {
            Texture view = camera.activeTexture;
            GUILayout.Label(view);
            UnityEngine.Object[] parts = Resources.LoadAll("BuiltItems/", typeof(GameObject));
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
                                partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = WeaponParts[WeaponPartsID[i]];
                                break;
                            case "Armour Builder":
                                partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ArmourParts[ArmourPartsID[i]];
                                break;

                        }
                        partHolderWeapon2D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
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
                                partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = WeaponParts[WeaponPartsID[i]];
                                break;
                            case "Armour Builder":
                                partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().data = ArmourParts[ArmourPartsID[i]];
                                break;
                        }
                        partHolderWeapon3D.transform.GetChild(i).GetComponent<ScriptableObjectHolder>().ResetValues();
                    }

                }
            }
        }
    }
}


