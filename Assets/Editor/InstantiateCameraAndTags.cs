using UnityEngine;
using UnityEditor;

public class InstantiateCameraAndTags : MonoBehaviour
{
    GameObject instance;
    LayerMask layer;
    Camera shotCamera;

    public InstantiateCameraAndTags()
    {
        createTagsAndLayer();
        instance = GameObject.FindGameObjectWithTag("model");
        layer = LayerMask.NameToLayer("VolumetricLayer");
    }

    //Create the render camera and put it in the correct rotation/position
    public void Charge()
    {
        if (GameObject.FindGameObjectWithTag("model"))
        {
            instance.layer = layer;
            createCamera();
            shotCamera.transform.position = new Vector3(instance.transform.position.x, instance.transform.position.y + instance.GetComponent<Renderer>().bounds.size.y / 2, instance.transform.position.z - instance.GetComponent<Renderer>().bounds.size.z);
            shotCamera.transform.Rotate(0,0,0);
        }
        else {
            var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly. Go to Tags ans select the tag with name 'model' in which object you want to create the volumetric model \n\n Tags > 'model'", "Ok");
            if (option.Equals(2))
                Charge();
        }
    }

    //Create the camera and specify the render layer
    public void createCamera()
    {
        //Check if exists
        if (!GameObject.FindGameObjectWithTag("ShotCamera"))
        {
            var go = new GameObject("ShotCamera");
            shotCamera = go.AddComponent<Camera>();
            shotCamera.tag = "ShotCamera";
            shotCamera.CopyFrom(Camera.main);
            shotCamera.enabled = false;
        }
        else {
            shotCamera = GameObject.FindWithTag("ShotCamera").GetComponent<Camera>();
            Debug.Log("Camera already created!");
        }
        shotCamera.backgroundColor = new Color(200,200,200,0);
        shotCamera.clearFlags = CameraClearFlags.SolidColor;
        shotCamera.orthographic = true;

        //Calculate the initial ortographic size
        if(instance.GetComponent<Renderer>().bounds.size.y > instance.GetComponent<Renderer>().bounds.size.x)
            shotCamera.orthographicSize = instance.GetComponent<Renderer>().bounds.size.y / 2;
        else
            shotCamera.orthographicSize = instance.GetComponent<Renderer>().bounds.size.x / 2;

        //Start the near at the front of the model and the far in the end
        //Culling Mask -> Volumetric Layer
        shotCamera.nearClipPlane = -instance.GetComponent<Renderer>().bounds.size.z / 2;
        shotCamera.farClipPlane = instance.GetComponent<Renderer>().bounds.size.z / 2;
        shotCamera.cullingMask = 1 << LayerMask.NameToLayer("VolumetricLayer");
    }

    //Create the tags/layers needed at the start
    public void createTagsAndLayer()
    {
        //Open the TagManager
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");
        SerializedProperty layersProp = tagManager.FindProperty("layers");

        //Add the tags and layers
        string tag1 = "model";
        string tag2 = "ShotCamera";
        string tag3 = "LOD1";
        string tag4 = "LOD2";
        string tag5 = "LOD3";
        string tag6 = "LOD4";
        string tag7 = "LOD5";
        string tag8 = "LOD6";
        string layer1 = "VolumetricLayer";

        // First check if it is not already present
        bool found = false;
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(tag1) || t.stringValue.Equals(tag2) || t.stringValue.Equals(tag3) || t.stringValue.Equals(tag4) || t.stringValue.Equals(tag5) || t.stringValue.Equals(tag6) || t.stringValue.Equals(tag7) || t.stringValue.Equals(tag8)) { found = true; break; }
        }

        //If it's not already created, add it
        if (!found)
        {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n1 = tagsProp.GetArrayElementAtIndex(0);
            n1.stringValue = tag1;
            tagsProp.InsertArrayElementAtIndex(1);
            SerializedProperty n2 = tagsProp.GetArrayElementAtIndex(1);
            n2.stringValue = tag2;
            tagsProp.InsertArrayElementAtIndex(2);
            SerializedProperty n3 = tagsProp.GetArrayElementAtIndex(2);
            n3.stringValue = tag3;
            tagsProp.InsertArrayElementAtIndex(3);
            SerializedProperty n4 = tagsProp.GetArrayElementAtIndex(3);
            n4.stringValue = tag4;
            tagsProp.InsertArrayElementAtIndex(4);
            SerializedProperty n5 = tagsProp.GetArrayElementAtIndex(4);
            n5.stringValue = tag5;
            tagsProp.InsertArrayElementAtIndex(5);
            SerializedProperty n6 = tagsProp.GetArrayElementAtIndex(5);
            n6.stringValue = tag6;
            tagsProp.InsertArrayElementAtIndex(6);
            SerializedProperty n7 = tagsProp.GetArrayElementAtIndex(6);
            n7.stringValue = tag7;
            tagsProp.InsertArrayElementAtIndex(7);
            SerializedProperty n8 = tagsProp.GetArrayElementAtIndex(7);
            n8.stringValue = tag8;
        }
        SerializedProperty sp = layersProp.GetArrayElementAtIndex(31);
        if (sp != null) sp.stringValue = layer1;
        tagManager.ApplyModifiedProperties();
    }
}