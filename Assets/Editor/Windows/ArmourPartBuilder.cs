﻿using UnityEngine;
using UnityEditor;
public class ArmourPartBuilderWindow : SubWindowHandler
{
    float m_speedModifer;
    void OnGUI()
    {
        Handle("Materials");
        m_speedModifer = EditorGUILayout.FloatField("Speed Value: ", m_speedModifer);
        objectData.BuffValuePart2 = m_speedModifer;
        if (m_speedModifer < 1)
            m_speedModifer = 1;
        BuildHandle("Armour Part", ItemType.eArmourPart, "ArmourParts", 0);

    }
}
