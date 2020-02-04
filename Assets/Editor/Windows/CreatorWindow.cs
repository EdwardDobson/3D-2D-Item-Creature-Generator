using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
public class CreatorWindow : EditorWindow
{
    static CreatorWindow window;
    string[] m_buttonNames = { "Material Builder", "Weapon Builder", "Weapon Part Builder", "Armour Builder", "Armour Part Builder","Creature Builder",
    "Creature Part Builder","Potion Builder" };
    Rect m_buttonSize;
    bool m_aspectMode;
    int m_materialID;
    List<string> m_MatNames = new List<string>();
    List<ScriptableObjectData> m_Mats = new List<ScriptableObjectData>();

    protected string currentWindowName = "";
    protected int screenID;
    protected string itemName;
    protected string itemDescription;
    protected ScriptableObjectData objectData;

    public Sprite itemTexture;

    [MenuItem("Window/Item + Creature Builder")]
    static void Init()
    {

        window = (CreatorWindow)GetWindow(typeof(CreatorWindow));
        window.currentWindowName = "Builder";
        window.Show();
    }
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        CreateLabel(25, new RectOffset(15, 0, 15, 0), "Item + Creature Builder");
        Buttons();
        
        EditorGUILayout.EndHorizontal();

    }
    void Buttons()
    {
        for (int i = 0; i < m_buttonNames.Length; ++i)
        {
            m_buttonSize = new Rect(20, 55 + i * 40, 140, 30);
            if (GUI.Button(m_buttonSize, m_buttonNames[i]))
            {
                screenID = i + 1;
                switch (screenID)
                {
                    case 1:
                        window = (MaterialWindow)GetWindow(typeof(MaterialWindow));
                        break;
                    case 2:
                        window = (WeaponBuilderWindow)GetWindow(typeof(WeaponBuilderWindow));
                        break;
                    case 3:
                        window = (WeaponPartWindow)GetWindow(typeof(WeaponPartWindow));
                        break;
                    case 4:
                        window = (ArmourBuilderWindow)GetWindow(typeof(ArmourBuilderWindow));
                        break;
                    case 5:
                        window = (ArmourPartBuilderWindow)GetWindow(typeof(ArmourPartBuilderWindow));
                        break;
                    case 6:
                        window = (CreatureBuilderWindow)GetWindow(typeof(CreatureBuilderWindow));
                        break;
                    case 7:
                        window = (CreaturePartBuilderWindow)GetWindow(typeof(CreaturePartBuilderWindow));
                        break;
                    case 8:
                        window = (PotionBuilderWindow)GetWindow(typeof(PotionBuilderWindow));
                        break;
                }
                if (window != null)
                {
                    window.currentWindowName = m_buttonNames[i];
                    window.Show();
                }

                Debug.Log("Clicked " + m_buttonNames[i] + screenID);
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

        GUIStyle styleB = new GUIStyle(GUI.skin.GetStyle("label"));
        styleB.fontSize = 15;
        styleB.padding = new RectOffset(15, 0, 15, 0);


        UnityEngine.Object[] materials = Resources.LoadAll("Materials");
        for (int i = 0; i < materials.Length; i++)
        {
            if(!m_Mats.Contains((ScriptableObjectData)materials[i]))
                m_Mats.Add((ScriptableObjectData)materials[i]);
        }

        string[] MaterialNames = Array.ConvertAll(materials, x => x.ToString());
        for (int i = 0; i < MaterialNames.Length; i++)
        {
            if(!m_MatNames.Contains(MaterialNames[i]))
                m_MatNames.Add(MaterialNames[i].Substring(0, MaterialNames[i].IndexOf("(")));
        }

        itemTexture = (Sprite)EditorGUILayout.ObjectField("Icon", itemTexture, typeof(Sprite), true);
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
    protected void ShowMaterialList()
    {
        m_materialID = EditorGUILayout.Popup("Materials", m_materialID, m_MatNames.ToArray());
        itemTexture = m_Mats[m_materialID].Sprite;
    }
    public void CreateLabel(int _fontSize, RectOffset _rect, string _labelText)
    {
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = _fontSize;
        style.padding = _rect;
        GUILayout.Label(_labelText, style);
    }
    protected void BuildItem()
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
             
                    AssetDatabase.CreateAsset(clone, "Assets/Resources/Materials/" + itemName + ".asset");
                    item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/Materials/" + itemName + ".asset", typeof(ScriptableObjectData));
                    Debug.Log("Item buff value " + item.GetComponent<ScriptableObjectHolder>().data.BuffValue);
              
                    item.GetComponent<ScriptableObjectHolder>().data = (ScriptableObjectData)AssetDatabase.LoadAssetAtPath("Assets/Resources/Materials/" + itemName + ".asset", typeof(ScriptableObjectData));
                    PrefabUtility.SaveAsPrefabAssetAndConnect(item, "Assets/BuiltItems/" + itemNameCombined, InteractionMode.UserAction);
                 
                
                AssetDatabase.Refresh();

            }

        }
    }
}


