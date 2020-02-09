using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RarityWindow : CreatorWindow
{
   void OnGUI()
    {
        BaseFunction();
        RaritiesList[0].BuffMuliplier = EditorGUILayout.FloatField("Common Multipler: ", RaritiesList[0].BuffMuliplier);
        RaritiesList[1].BuffMuliplier = EditorGUILayout.FloatField("Uncommon Multipler: ", RaritiesList[1].BuffMuliplier);
        RaritiesList[2].BuffMuliplier = EditorGUILayout.FloatField("Rare Multipler: ", RaritiesList[2].BuffMuliplier);
        RaritiesList[3].BuffMuliplier = EditorGUILayout.FloatField("Epic Multipler: ", RaritiesList[3].BuffMuliplier);
        RaritiesList[4].BuffMuliplier = EditorGUILayout.FloatField("Legendary Multipler: ", RaritiesList[4].BuffMuliplier);
        RaritiesList[5].BuffMuliplier = EditorGUILayout.FloatField("Unique Multipler: ", RaritiesList[5].BuffMuliplier);
        for(int i = 0; i < RaritiesList.Count; ++i)
        {
            if (RaritiesList[i].BuffMuliplier < 0)
                RaritiesList[i].BuffMuliplier = 0;
        }
    }
}
