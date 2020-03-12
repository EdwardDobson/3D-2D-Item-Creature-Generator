using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FolderWindow : CreatorWindow
{
   string m_folderName;
    int m_deleteIndex;
   void OnGUI()
    {
        BaseFunction();
        m_folderName = EditorGUILayout.TextField(m_folderName);
        if (GUILayout.Button("Create Folder"))
        {
            CreateFolder(m_folderName);
        }
        m_deleteIndex = EditorGUILayout.Popup("", m_deleteIndex, folderNames.ToArray());
        string folderName = folderNames[m_deleteIndex];
        if(folderName != "")
        {
            if (GUILayout.Button("Delete Folder") && folderNames.Count > 1)
            {
                DeleteFolder(folderName);
            }
        }
  
        CloseButton();
    }
}
