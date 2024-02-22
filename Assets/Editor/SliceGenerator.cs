using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

[InitializeOnLoad]
public class SliceGenerator : MonoBehaviour {
    
    private float diff;
    private GameObject model;
    private Bounds bounds;
    Camera shotCamera;
    public static int numberOfShots;
    public Vector3 size, pos, previousPos, previousRot;
    private Camera _cam1;
    private Mesh _m1;
    private Texture2D tex;
    private Shader shader;
    public static List<float> positionsArray;
    public static List<string> folderNames;
    public static string folderDataPath, folderDataPathName;

    public SliceGenerator()
    {
        positionsArray = new List<float>();
        folderNames = new List<string>();
        numberOfShots = PlayerPrefs.GetInt("CustomShots");
    }

    //Init cameras and get input value
    void Start()
    {
        shotCamera = GameObject.Find("ShotCamera").GetComponent<Camera>();
        shotCamera.enabled = true;
        previousPos = GameObject.FindGameObjectWithTag("model").transform.position;
        previousRot = new Vector3(GameObject.FindGameObjectWithTag("model").transform.rotation.x, GameObject.FindGameObjectWithTag("model").transform.rotation.y, GameObject.FindGameObjectWithTag("model").transform.rotation.z);
        GameObject.FindGameObjectWithTag("model").transform.position = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("model").transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));

        folderDataPath = Application.dataPath + "/Resources/Textures/" + GameObject.FindGameObjectWithTag("model").name;
        folderDataPathName = GameObject.FindGameObjectWithTag("model").name;

        if (!Directory.Exists(folderDataPath))
        {
            Directory.CreateDirectory(folderDataPath);
            folderNames.Add(folderDataPathName);
        }
    }

    //Put the path and the name file of the slice
    public static string ScreenShotName(int width, int height, string name, int id)
    {
        return string.Format("{0}/Resources/Textures/" + folderDataPathName + "/" + name + id + ".png", Application.dataPath);
    }

    //Refresh to load textures in unity asset folder
    private void Refresh()
    {
        AssetDatabase.Refresh();
    }

    //Get the size of the model to make the texture the same size
    public Vector3 getModelSize()
    {
        return GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size;
    }

    //Get the model position to put the textures in the correct spot
    public Vector3 getModelPosition()
    {
        return GameObject.FindGameObjectWithTag("model").transform.position;
    }

    //Save positions of the camera in file
    private static void generateTXTPositionsArray(int n2)
    {
        var fileName = "PositionsArray.txt";
        var sr = File.CreateText(fileName);

        for (int i = 0; i < n2; i++)
        {
            sr.WriteLine(positionsArray[i]);
        }
        sr.Close();
    }

    //Generate the slices of the selected model
    public void generateTextures(string name, Vector3 size, bool finished, int n)
    {
        Start();
        for (int i = 0; i < n; i++)
        {
            //Modify near/far to get the correct slice
            if (shotCamera.nearClipPlane != shotCamera.farClipPlane)
            {
                var fwd = GameObject.FindGameObjectWithTag("model").transform.forward;
                shotCamera.transform.position = new Vector3(getModelPosition().x, getModelPosition().y + getModelSize().y / 2, getModelPosition().z + getModelSize().z);
                shotCamera.transform.rotation = Quaternion.LookRotation(fwd);

                shotCamera.nearClipPlane -= GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z / numberOfShots;
                positionsArray.Add(shotCamera.nearClipPlane);
                diff = Math.Abs(GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z / (numberOfShots/2));
                shotCamera.farClipPlane = shotCamera.nearClipPlane + diff;
                PlayerPrefs.SetFloat("Position" + i, positionsArray[i]);
            }
            
            //Get the image rendered with "ShotCamera"
            RenderTexture rt = new RenderTexture((int)size.y, (int)size.y, 24); 
            shotCamera.targetTexture = rt;

            Texture2D screenShot = new Texture2D((int)size.y, (int)size.y, TextureFormat.ARGB32, false);
            shotCamera.Render();
            RenderTexture.active = rt;

            screenShot.ReadPixels(new Rect(0, 0, (int)size.y, (int)size.y), 0, 0);
            shotCamera.targetTexture = null;
            RenderTexture.active = null; 
            DestroyImmediate(rt);

            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName((int)size.y, (int)size.y, name, i);

            //Write in memory
            var binary = new BinaryWriter(File.Open(filename, FileMode.Create));
            binary.Write(bytes);
            binary.Close();

            Debug.Log(string.Format("Took texture to: {0}", filename));
        }
        //Save camera positions
        generateTXTPositionsArray(n);
        //Load in the project
        Refresh();
        //Restart camera position
        shotCamera.nearClipPlane = -GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z/2;
        diff = Math.Abs(GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z / (numberOfShots));
        shotCamera.farClipPlane = shotCamera.nearClipPlane + diff;
        PlayerPrefs.Save();
    }
}
