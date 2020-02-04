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
    "Creature Part Builder","Potion Builder" };
    Rect m_buttonSize;
    bool m_aspectMode;
    List<string> m_MatNames = new List<string>();

    protected string currentWindowName = "";
    protected int screenID;
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;
    protected int  materialID;
    protected List<ScriptableObjectData>  Mats = new List<ScriptableObjectData>();

    public Sprite itemTexture;

    [MenuItem("Item + Creature Builder/Builder")]
    static void Init()
    {
        m_window = (CreatorWindow)GetWindow(typeof(CreatorWindow));
        m_window.currentWindowName = "Builder";
        m_window.maxSize = new Vector2(200, 550);
        m_window.minSize = m_window.maxSize;
        m_window.Show();
    }
    void OnGUI()
    {
      
        EditorGUILayout.BeginHorizontal();
        CreateLabel(20, new RectOffset(15, 0, 15, 0), "Item + Creature \nBuilder\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nCreated by\nEdward Dobson");
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
        objectData = (ScriptableObjectData)EditorGUILayout.ObjectField("Data", objectData, typeof(ScriptableObjectData), false);

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
    }
 
    public void CreateLabel(int _fontSize, RectOffset _rect, string _labelText)
    {
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = _fontSize;
        style.padding = _rect;
        GUILayout.Label(_labelText, style);
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
                ScriptableObjectData clone = Instantiate(objectData);
                if (clone.type == _type)
                {
                    AssetDatabase.CreateAsset(clone, "Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset");
                    item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/BuiltItems/" + _dir + "/" + itemName + ".asset", typeof(ScriptableObjectData));
                    PrefabUtility.SaveAsPrefabAssetAndConnect(item, "Assets/Resources/BuiltItems/" + _dir + "/" + itemNameCombined, InteractionMode.UserAction);
                }
                DestroyImmediate(GameObject.Find(itemName));
           
          
                AssetDatabase.Refresh();

            }

        }
    }
}


