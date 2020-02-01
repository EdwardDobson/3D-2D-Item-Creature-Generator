using UnityEngine;
using UnityEditor;

public class CreatorWindow : EditorWindow
{
    protected string currentWindowName = "";
    string[] m_buttonNames = { "Material Builder", "Weapon Builder", "Weapon Part Builder", "Armour Builder", "Armour Part Builder","Creature Builder",
    "Creature Part Builder","Potion Builder" };
    Rect m_buttonSize;
    protected int screenID;
    static CreatorWindow window;
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
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = 25;
        style.padding = new RectOffset(15, 0, 15, 0);
        GUILayout.Label("Item + Creature Builder", style);
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
                if(window != null)
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
        EditorGUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.fontSize = 25;
        style.padding = new RectOffset(15, 0, 15, 0);
        GUILayout.Label(currentWindowName, style);
        if (GUILayout.Button("Close"))
        {
            screenID = 0;
            Debug.Log("Closing window");
            this.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}


