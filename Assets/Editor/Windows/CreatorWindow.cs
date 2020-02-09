using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
public class CreatorWindow : EditorWindow
{
    static CreatorWindow m_window;
    string[] m_buttonNames = { "Material Builder", "Weapon Builder", "Weapon Part Builder", "Armour Builder", "Armour Part Builder","Creature Builder",
    "Creature Part Builder","Potion Builder", "Rarity Window" };
    Rect m_buttonSize;
    bool m_aspectMode;
    List<string> m_MatNames = new List<string>();
    List<string> m_WeaponPartNames = new List<string>();
    bool m_loadData = true;


    protected string currentWindowName = "";
    protected int screenID;
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;
    protected Weapon weaponData;
    protected int materialID;
    protected int rarityID;
    protected List<ScriptableObjectData> Mats = new List<ScriptableObjectData>();
    protected List<ScriptableObjectData> WeaponParts = new List<ScriptableObjectData>();
    protected UnityEngine.Object[] Rarities;
    protected List<RarityBaseData> RaritiesList = new List<RarityBaseData>();
    protected int[] WeaponPartsID = new int[4];
    protected float rarityBuff = 1;

    public Sprite itemTexture;

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

                screenID = i + 1;
                switch (screenID)
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
            foreach (RarityBaseData r in Rarities)
            {
                if (!RaritiesList.Contains(r))
                    RaritiesList.Add(r);
                Debug.Log("Rarities name " + r.name);
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

            itemTexture = (Sprite)EditorGUILayout.ObjectField("Icon", itemTexture, typeof(Sprite), true);
            GUIStyle styleB = new GUIStyle(GUI.skin.GetStyle("label"));
            styleB.fontSize = 15;
            styleB.padding = new RectOffset(15, 0, 15, 0);
            if (m_aspectMode)
            {
                GUILayout.Label("Current Mode 3D", styleB);
                m_aspectMode = GUILayout.Toggle(m_aspectMode, "3D");
            }
            else
            {
                GUILayout.Label("Current Mode 2D", styleB);
                m_aspectMode = GUILayout.Toggle(m_aspectMode, "2D");
            }

            rarityID = EditorGUILayout.Popup("Rarity", rarityID, Enum.GetNames(typeof(Rarity)));

        }
        //  Debug.Log("Object data " + objectData.name);
        //   objectData = (ScriptableObjectData)EditorGUILayout.ObjectField("Data", objectData, typeof(ScriptableObjectData), false);
        //   weaponData = (Weapon)EditorGUILayout.ObjectField("Weapon Data", weaponData, typeof(Weapon), false);
    }
    public void CloseButton()
    {
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Close"))
        {
            screenID = 0;
            Debug.Log("Closing window");
            Close();
        }
    }
    protected void ShowMaterialList(string _dir)
    {
        UnityEngine.Object[] materials = Resources.LoadAll("BuiltItems/" + _dir, typeof(ScriptableObjectData));
        for (int i = 0; i < materials.Length; i++)
        {
            if (!Mats.Contains((ScriptableObjectData)materials[i]))
                Mats.Add((ScriptableObjectData)materials[i]);
        }
        //Used for picking the material to use
        string[] MaterialNames = Array.ConvertAll(materials, x => x.ToString());
        //Shortens the length of the material name
        for (int i = 0; i < MaterialNames.Length; i++)
        {
            if (!m_MatNames.Contains(MaterialNames[i]))
                m_MatNames.Add(MaterialNames[i].Substring(0, MaterialNames[i].IndexOf("(")));
        }
        materialID = EditorGUILayout.Popup(_dir, materialID, m_MatNames.ToArray());

        if (m_MatNames.Count > Mats.Count)
            m_MatNames.Clear();
    }
    protected void ShowWeaponPartsList()
    {
        UnityEngine.Object[] parts = Resources.LoadAll("BuiltItems/WeaponParts", typeof(ScriptableObjectData));
        for (int i = 0; i < parts.Length; i++)
        {
            if (!WeaponParts.Contains((ScriptableObjectData)parts[i]))
                WeaponParts.Add((ScriptableObjectData)parts[i]);
        }
        string[] PartsNames = Array.ConvertAll(parts, x => x.ToString());
        for (int i = 0; i < PartsNames.Length; i++)
        {
            if (!m_WeaponPartNames.Contains(PartsNames[i]))
                m_WeaponPartNames.Add(PartsNames[i].Substring(0, PartsNames[i].IndexOf("(")));
        }
        switch (weaponData.WeaponType)
        {
            case WeaponType.eSword:
                WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], m_WeaponPartNames.ToArray());
                WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], m_WeaponPartNames.ToArray());
                WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], m_WeaponPartNames.ToArray());
                WeaponPartsID[3] = EditorGUILayout.Popup("Slot 4", WeaponPartsID[3], m_WeaponPartNames.ToArray());
                break;
            case WeaponType.eSpear:
            case WeaponType.eMace:
            case WeaponType.eAxe:
                WeaponPartsID[0] = EditorGUILayout.Popup("Slot 1", WeaponPartsID[0], m_WeaponPartNames.ToArray());
                WeaponPartsID[1] = EditorGUILayout.Popup("Slot 2", WeaponPartsID[1], m_WeaponPartNames.ToArray());
                WeaponPartsID[2] = EditorGUILayout.Popup("Slot 3", WeaponPartsID[2], m_WeaponPartNames.ToArray());
                break;

        }
        if (m_WeaponPartNames.Count > WeaponParts.Count)
            m_WeaponPartNames.Clear();

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
        weaponData.TotalDamage = 0;
        weaponData.AttackSpeed = 0;
    }
    //Handles building items/creature parts
    protected void BuildItem(string _dir, ItemType _type)
    {
        GameObject item = new GameObject();
        item.name = itemName;
        string itemNameCombined = itemName + ".prefab";
        if (itemNameCombined != null && objectData != null)
        {
            objectData.Sprite = itemTexture;
            if (m_aspectMode)//3D Mode
            {
                item.AddComponent<BoxCollider>();
                item.AddComponent<MeshFilter>();
                item.AddComponent<MeshRenderer>();
                item.GetComponent<MeshFilter>().mesh = objectData.Mesh;
            }
            else//2D Mode
            {
                item.AddComponent<BoxCollider2D>();
                item.AddComponent<SpriteRenderer>();
                item.GetComponent<SpriteRenderer>().sprite = objectData.Sprite;
            }
            item.AddComponent<ScriptableObjectHolder>();
            if (item.GetComponent<ScriptableObjectHolder>() != null)
            {

                if (_type != ItemType.eWeapon)
                {

                    ScriptableObjectData clone = Instantiate(objectData);
                    AssetDatabase.CreateAsset(clone, "Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset");
                    item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                    PrefabUtility.SaveAsPrefabAssetAndConnect(item, "Assets/Resources/BuiltItems/" + _dir + "/" + itemNameCombined, InteractionMode.UserAction);
                }
                else
                {
                    Weapon clone = Instantiate(weaponData);
                    AssetDatabase.CreateAsset(clone, "Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset");
                    item.GetComponent<ScriptableObjectHolder>().data = (Weapon)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset", typeof(Weapon));
                    PrefabUtility.SaveAsPrefabAssetAndConnect(item, "Assets/Resources/BuiltItems/" + _dir + "/" + itemNameCombined, InteractionMode.UserAction);
                    Debug.Log("Building");
                }

                DestroyImmediate(GameObject.Find(itemName));
                AssetDatabase.Refresh();
                ClearObjectData();
            }

        }
    }
}


