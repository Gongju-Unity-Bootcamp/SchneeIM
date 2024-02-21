using UnityEngine;
using UnityEditor;
using System.Collections;

public class ResolutionEditor : EditorWindow 
{
    public static int myFieldWidth = 64, myFieldHeight = 64, customShots = 8;
	static bool myBool = false;

	 [MenuItem("Tools/VMC Tool/Quality Settings...", false, 2)]
	 static void Init () {
        ResolutionEditor window = (ResolutionEditor)EditorWindow.GetWindow(typeof(ResolutionEditor));
        window.maxSize = new Vector2(500f, 170f);
        window.Show();
	}
	
    //Create window with resolution values: width/height of shots and number of slices
	void OnGUI ()
    {
        GUILayout.Label("Choose a resolution for volumetric model (px) -> Power of two", EditorStyles.boldLabel);
        GUILayout.Label("(in case that there are more than two LOD this resolution will be the first LOD)");
        GUILayout.Label("Please be careful. Putting a big resolution can cause an overload in the application");
		GUILayout.Label ("Quality Settings", EditorStyles.boldLabel);
        myFieldWidth = EditorGUILayout.IntField("Width", myFieldWidth);
        myFieldHeight = EditorGUILayout.IntField("Height", myFieldHeight);
        customShots = EditorGUILayout.IntField("Number Of Slices", customShots);
        
        ResolutionEditor window = (ResolutionEditor)EditorWindow.GetWindow(typeof(ResolutionEditor));
        if(GUILayout.Button("Save"))
            myBool = true;
        if (myBool)
        {
            window.Close();
            PlayerPrefs.SetInt("CustomShots", customShots);
            customShots = PlayerPrefs.GetInt("CustomShots");
        }
        myBool = false;
	}
}
