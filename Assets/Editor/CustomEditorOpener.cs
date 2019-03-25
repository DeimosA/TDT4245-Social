using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorOpener
{
    //allows for opening of custom assets in their custom editors via Unity Inspector
    [UnityEditor.Callbacks.OnOpenAsset(1)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        //check if object is of type to be opened in custom editor
        if (Selection.activeObject as ActivityCard != null) //open character editor
        {
            ActivityCardEditor.Init(Selection.activeObject as ActivityCard);
            return true; //catch open file
        }
        //no matches for custom editors, let unity open file as normal
        return false;
    }

    public static bool OpenActivityCardEditor()
    {
        ActivityCardEditor.Init(Selection.activeObject as ActivityCard);
        return true;
    }
}
