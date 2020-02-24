using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FolderWindow : CreatorWindow
{
   string m_folderName;
   void OnGUI()
    {
        BaseFunction();
        m_folderName = EditorGUILayout.TextField(m_folderName);
        if (GUILayout.Button("Create Folder"))
        {
            CreateFolder(m_folderName);
        }
        CloseButton();
    }
}
