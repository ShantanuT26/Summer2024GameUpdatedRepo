
/*
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class ScriptableObjAutoSave
{
    static ScriptableObjAutoSave()
    {
        EditorApplication.playModeStateChanged += ChangeAfterEdit;
    }
    public static void ChangeAfterEdit(PlayModeStateChange state)
    {
        if(state==PlayModeStateChange.ExitingEditMode)
        {
            SaveScrObjs();
        }
    }
    public static void SaveScrObjs()
    {
        string[] temp = AssetDatabase.FindAssets("t:ScriptableObject");
        foreach(string s in temp) 
        {
            string path = AssetDatabase.GUIDToAssetPath(s);
            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if(asset!= null) 
            {
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssetIfDirty(asset);
            }
        }
        
    }

}*/
