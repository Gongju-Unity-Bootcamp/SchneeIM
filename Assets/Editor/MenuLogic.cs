using UnityEngine;
using UnityEditor;
using System.IO;

[ExecuteInEditMode]

public class MenuLogic : MonoBehaviour
{
    public static InstantiateCameraAndTags load;
    public static SliceGenerator textures;
    public static LODManager lod;
    public static VMCMob vmc;
    static Vector3 size;
    public static bool platform = false;

    //Instantiate all the classes
    public static void Start()
    {
        load = new InstantiateCameraAndTags();
        textures = new SliceGenerator();
        lod = new LODManager();
        vmc = new VMCMob();
        size = new Vector3(ResolutionEditor.myFieldWidth, ResolutionEditor.myFieldHeight, 0);
        load.createTagsAndLayer();
    }

    //Generate the volumetric model
    [MenuItem("Tools/VMC Tool/Create Volumetric Model", false, 1)]
    private static void Create()
    {
        Start();
        //Add scripts to the MainCamera for mobile or PC platform
        if (!Camera.main.gameObject.GetComponent<VMC>())
            Camera.main.gameObject.AddComponent(typeof(VMC));
        if (!Camera.main.gameObject.GetComponent<VMCMob>())
            Camera.main.gameObject.AddComponent(typeof(VMCMob));

        //Generate .txt with details and info
        generateTXT();

        //Check Platform
        if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../PC") == true)
        {
            if (Camera.main.gameObject.GetComponent<VMCMob>())
                Camera.main.GetComponent<VMCMob>().enabled = false;
            Camera.main.GetComponent<VMC>().enabled = true;
        }
        else if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices") == true)
        {
            if (Camera.main.gameObject.GetComponent<VMC>())
                Camera.main.GetComponent<VMC>().enabled = false;
            Camera.main.GetComponent<VMCMob>().enabled = true;
        }

        //Error messages
        //In case the user doesnt slect any platform
        if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices") == false && Menu.GetChecked("Tools/VMC Tool/Choose Platform.../PC") == false)
        {
            var option = EditorUtility.DisplayDialog("Choose a correct platform", "Choose a platform in the menu before start creating the volumetric model: \n\n- Mobile Devices \n- PC", "Ok");
            if (GameObject.Find("LOD2Group"))
                DestroyImmediate(GameObject.Find("LOD2Group"));
        }
        //In case the user doesnt select any level of detail
        if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/Other...") == false && Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD") == false && Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD") == false)
        {
            var option = EditorUtility.DisplayDialog("Choose LOD number", "Choose the number of levels of detail for the model before creating the volumetric model", "Ok");
            if (GameObject.Find("LOD2Group"))
                DestroyImmediate(GameObject.Find("LOD2Group"));
        }

        //If the user already selected LOD and platform, take shots and create LODGroups
        if ((Menu.GetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices") == true || Menu.GetChecked("Tools/VMC Tool/Choose Platform.../PC") == true) && (Menu.GetChecked("Tools/VMC Tool/Level of Detail/Other...") == true || Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD") == true || Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD") == true))
        {
            //Instantiate camera and tags/layers
            load.Charge();

            //Create objects for mobile/PC platform
            if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../PC") == true)
            {
                platform = false;
            }
            else if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices") == true)
            {
                platform = true;

                if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD") == true)
                    vmc.createVMCObject("Texture0");
                if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD") == true)
                {
                    for (int i = 0; i <= 2; i++)
                    {
                        vmc.createVMCObject("Texture" + i);
                    }
                }
                if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/Other...") == true)
                {
                    for (int i = 0; i < LODEditor.customLOD; i++)
                    {
                        vmc.createVMCObject("Texture" + i);
                    }
                }
            }

            //Check LOD levels for mobile/PC platform
            //2 LOD
            if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD") == true)
            {
                textures.generateTextures("texture", size, true, ResolutionEditor.customShots);
                if (platform == false)
                    lod.LOD2Level();
                else if (platform == true)
                {
                    lod.LOD2LevelMob();
                    vmc.applyTexturesToGeometry("texture", "Texture0", "2", 2);
                }
                DestroyImmediate(GameObject.Find("ShotCamera"));
            }
            //3 LOD
            else if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD") == true)
            {
                textures.generateTextures("texture", size, false, ResolutionEditor.customShots);
                textures.generateTextures("texture21", size / 2, true, ResolutionEditor.customShots);
                if (platform == false)
                {
                    lod.LOD3Level();
                }
                else if (platform == true)
                {
                    lod.LOD3LevelMob();
                    vmc.applyTexturesToGeometry("texture", "Texture0", "3", 3);
                    vmc.applyTexturesToGeometry("texture21", "Texture1", "3", 3);
                }
                DestroyImmediate(GameObject.Find("ShotCamera"));
            }
            //Custom LOD
            else if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/Other...") == true) //REVISAR
            {
                if (platform == false)
                {
                    /*textures.generateTextures("texture", size, false, ResolutionEditor.customShots);
                    lod.LODCustomLevel(LODEditor.customLOD);
                    for (int i = 2; i < LODEditor.customLOD + 1; i++)
                    {
                        if (i != LODEditor.customLOD)
                            textures.generateTextures("texture" + i + "1", size / i, false, ResolutionEditor.customShots);
                        else
                            textures.generateTextures("texture" + i + "1", size / i, true, ResolutionEditor.customShots);
                    }*/
                    textures.generateTextures("texture", size, false, ResolutionEditor.customShots);
                    textures.generateTextures("texture21", size / 2, true, ResolutionEditor.customShots);
                    lod.LOD3Level();
                    var option = EditorUtility.DisplayDialog("Choose LOD number", "You can't add more than 3 LOD levels in PC platform", "Ok");
                }
                else if (platform == true)
                {
                    textures.generateTextures("texture", size, false, ResolutionEditor.customShots);
                    lod.LODCustomLevelMob(LODEditor.customLOD);
                    for (int i = 2; i < LODEditor.customLOD + 1; i++)
                    {
                        if (i != LODEditor.customLOD)
                            textures.generateTextures("texture" + i + "1", size / i, false, ResolutionEditor.customShots);
                        else
                            textures.generateTextures("texture" + i + "1", size / i, true, ResolutionEditor.customShots);
                    }
                    //vmc.applyTexturesToGeometry("texture", "Texture0", "Custom", WindowEditor.customLOD);
                    int count = 0;
                    for (int i = 2; i <= LODEditor.customLOD; i++)
                    {
                        if (count == 0)
                        {
                            vmc.applyTexturesToGeometry("texture", "Texture0", "Custom", LODEditor.customLOD);
                            count++;
                        }
                        else
                            vmc.applyTexturesToGeometry("texture" + (i - 1) + "1", "Texture" + (i - 2), "Custom", LODEditor.customLOD);
                    }
                }
                DestroyImmediate(GameObject.Find("ShotCamera"));
            }

            if (Menu.GetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices") == true)
                if (GameObject.Find("VMCMob"))
                    DestroyImmediate(GameObject.Find("VMCMob"));
            GameObject.FindGameObjectWithTag("model").layer = 0;
        }
    }

    //Generate .txt with info of the options
    private static void generateTXT()
    {
        var fileName = "LODProperties.txt";
        var sr = File.CreateText(fileName);

        if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD") == true)
            sr.WriteLine("YES");
        else
            sr.WriteLine("NO");
        if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD") == true)
            sr.WriteLine("YES");
        else
            sr.WriteLine("NO");
        if (Menu.GetChecked("Tools/VMC Tool/Level of Detail/Other...") == true)
            sr.WriteLine("YES");
        else
            sr.WriteLine("NO");
        sr.WriteLine(LODEditor.customLOD);
        sr.WriteLine(ResolutionEditor.customShots);
        sr.Close();
    }

    //Select PC platform
    [MenuItem("Tools/VMC Tool/Choose Platform.../PC", false, 4)]
    private static void PlatformPC()
    {
        Menu.SetChecked("Tools/VMC Tool/Choose Platform.../PC", true);
        Menu.SetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices", false);
    }

    //Select mobile platform
    [MenuItem("Tools/VMC Tool/Choose Platform.../Mobile Devices", false, 5)]
    private static void PlatformMobile()
    {
        Menu.SetChecked("Tools/VMC Tool/Choose Platform.../PC", false);
        Menu.SetChecked("Tools/VMC Tool/Choose Platform.../Mobile Devices", true);
    }

    //Select 2 LOD Levels
    [MenuItem("Tools/VMC Tool/Level of Detail/ 2 LOD", false, 6)]
    private static void LevelOfDetail2()
    {
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD", true);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD", false);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/Other...", false);
    }

    //Select 3 LOD Levels
    [MenuItem("Tools/VMC Tool/Level of Detail/ 3 LOD", false, 6)]
    private static void LevelOfDetail3()
    {
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 2 LOD", false);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/ 3 LOD", true);
        Menu.SetChecked("Tools/VMC Tool/Level of Detail/Other...", false);
    }
}
