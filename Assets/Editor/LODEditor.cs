using UnityEngine;
using UnityEditor;
using System.Collections;

public class LODEditor : EditorWindow 
{
    public static int myField = 4, customLOD = 0;
	bool groupEnabled;
	static bool myBool = false;
    public LODManager lod;

	 [MenuItem("Tools/VMC Tool/Level of Detail/Other...", false, 7)]
	 static void Init () {
        LODEditor window = (LODEditor)EditorWindow.GetWindow(typeof(LODEditor));
        window.maxSize = new Vector2(370f, 140f);
        window.Show();
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD", false);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD", false);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/Other...", true);
	}
	
    //Create window and specify the number of LOD per model (4 as default)
	void OnGUI () {
        GUILayout.Label("(!) For PC Platform there are 3 LOD as maximum", EditorStyles.boldLabel);
        GUILayout.Label("Please be careful. Putting too many levels of detail can cause \nan overload in the application.");
        GUILayout.Label ("LOD Settings", EditorStyles.boldLabel);
        myField = EditorGUILayout.IntField("Number of LOD", myField);

        LODEditor window = (LODEditor)EditorWindow.GetWindow(typeof(LODEditor));
        if(GUILayout.Button("Save"))
            myBool = true;
        if (myBool)
        {
            customLOD = myField;
            window.Close();
        }
        myBool = false;
	}
}
