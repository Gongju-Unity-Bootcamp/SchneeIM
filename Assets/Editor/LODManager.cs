using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LODManager : MonoBehaviour {
    
	GameObject go;
    SerializedObject obj;
    bool enter;
    int entered = 0;

    public void LOD2Level()
	{
		GameObject lodObject = new GameObject("LOD2Group");
		LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

		//Add 2 LOD levels
        LOD[] lods = new LOD[2];
        for (int i = 0; i < 2; i++)
        {
            PrimitiveType primType = PrimitiveType.Cube;
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            //Add cubes to the rest of LOD levels to create 3D texture
            if (i != 0)
            {
                go = GameObject.CreatePrimitive(primType);
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.tag = "LOD" + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2 + 0.4f, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size;
                go.transform.Rotate(new Vector3(180, 0, 0));
                Material n = new Material(Shader.Find("Ray Marching/Volume"));
                n.name = "RayCasting" + i;
                go.GetComponent<Renderer>().sharedMaterial = n;
            }

            //Add all renderers of models to their LOD level
			Renderer[] renderers = new Renderer[1];
			renderers[0] = go.GetComponent<Renderer>();
			lods[i] = new LOD(1.0F / (i + 1), renderers);
		}
		group.SetLODs(lods);
		group.RecalculateBounds();
        lodObject.transform.localPosition = GameObject.FindGameObjectWithTag("model").transform.position;
    }

    public void LOD3Level()
    {
        GameObject lodObject = new GameObject("LOD3Group");
        LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

        //Add 3 LOD levels
        LOD[] lods = new LOD[3];
        for (int i = 0; i < 3; i++)
        {
            PrimitiveType primType = PrimitiveType.Cube;
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            //Add cubes to the rest of LOD levels to create 3D texture
            if (i != 0)
            {
                go = GameObject.CreatePrimitive(primType);
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.tag = "LOD" + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2 + 0.4f, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size;
                go.transform.Rotate(new Vector3(180, 0, 0));
                Material n = new Material(Shader.Find("Ray Marching/Volume"));
                n.name = "RayCasting" + i;
                go.GetComponent<Renderer>().sharedMaterial = n;
            }

            //Add all renderers of models to their LOD level
            Renderer[] renderers = new Renderer[1];
            renderers[0] = go.GetComponent<Renderer>();
            lods[i] = new LOD(1.0F / (i + 1), renderers);
        }
        group.SetLODs(lods);
        group.RecalculateBounds();
        lodObject.transform.localPosition = GameObject.FindGameObjectWithTag("model").transform.position;
    }

    public void LODCustomLevel(int n)
    {
        GameObject lodObject = new GameObject("LODCustomGroup");
        LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

        //Add custom LOD levels
        LOD[] lods = new LOD[n];
        for (int i = 0; i < n; i++)
        {
            PrimitiveType primType = PrimitiveType.Cube;
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            //Add cubes to the rest of LOD levels to create 3D texture
            if (i != 0)
            {
                go = GameObject.CreatePrimitive(primType);
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.tag = "LOD" + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2 + 0.4f, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size;
                go.transform.Rotate(new Vector3(180, 0, 0));
                Material nShader = new Material(Shader.Find("Ray Marching/Volume"));
                nShader.name = "RayCasting" + i;
                go.GetComponent<Renderer>().sharedMaterial = nShader;
            }

            //Add all renderers of models to their LOD level
            Renderer[] renderers = new Renderer[1];
            renderers[0] = go.GetComponent<Renderer>();
            lods[i] = new LOD(1.0F / (i + 1), renderers);
        }
        group.SetLODs(lods);
        group.RecalculateBounds();
        lodObject.transform.localPosition = GameObject.FindGameObjectWithTag("model").transform.position;
    }

    /*----------------------------------------*/
    /*------------MOBILE DEVICES--------------*/
    /*----------------------------------------*/

    public void LOD2LevelMob()
    {
        GameObject lodObject = new GameObject("LOD2Group");
        LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

        //Add 2 LOD levels
        LOD[] lods = new LOD[2];
        for (int i = 0; i < 2; i++)
        {
            enter = false;
            go = GameObject.Find("VMCMob");
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            if (i != 0)
            {
                go = GameObject.Find("VMCMob");
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.x);
            }

            //Add all the geometry to the LOD level
            Renderer[] renderers = new Renderer[PlayerPrefs.GetInt("CustomShots")+1];
            renderers[0] = go.GetComponent<Renderer>();
            if (enter == false)
            {
                for (int j = 1; j < PlayerPrefs.GetInt("CustomShots")+1; j++)
                {
                    if (go.transform.childCount > 0)
                    {
                        renderers[j] = go.transform.GetChild(j-1).GetComponent<Renderer>();
                    }
                    enter = true;
                }
            }
            lods[i] = new LOD(1.0F / (i + 1), renderers);
        }
        group.SetLODs(lods);
        group.RecalculateBounds();
    }

    public void LOD3LevelMob()
    {
        GameObject lodObject = new GameObject("LOD3Group");
        LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

        //Add 3 LOD levels
        LOD[] lods = new LOD[3];
        for (int i = 0; i < 3; i++)
        {
            go = GameObject.Find("VMCMob");
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            if (i != 0)
            {
                go = GameObject.Find("VMCMob");
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.x);
            }

            //Add all the geometry to the LOD level
            Renderer[] renderers = new Renderer[PlayerPrefs.GetInt("CustomShots")+1];
            renderers[0] = go.GetComponent<Renderer>();
            if (entered <= 2)
            {
                for (int j = 1; j < PlayerPrefs.GetInt("CustomShots")+1; j++)
                {
                    if (go.transform.childCount > 0)
                    {
                        renderers[j] = go.transform.GetChild(j - 1).GetComponent<Renderer>();
                    }
                    enter = true;
                }
                entered++;
            }
            lods[i] = new LOD(1.0F / (i + 1), renderers);
        }
        group.SetLODs(lods);
        group.RecalculateBounds();
    }

    public void LODCustomLevelMob(int n)
    {
        GameObject lodObject = new GameObject("LODCustomGroup");
        LODGroup group = lodObject.AddComponent<LODGroup>();
        obj = new SerializedObject(group);

        //Add custom LOD levels
        LOD[] lods = new LOD[n];
        for (int i = 0; i < n; i++)
        {
            go = GameObject.Find("VMCMob");
            switch (i)
            {
                //Add the original model to LOD0
                case 0:
                    if (!GameObject.FindGameObjectWithTag("model"))
                    {
                        var option = EditorUtility.DisplayDialog("Model with tag 'model' not found", "Please make sure you tag the model correctly", "Ok");
                        if (option.Equals(1))
                            LOD2Level();
                    }
                    go = GameObject.FindGameObjectWithTag("model");
                    go.transform.SetParent(lodObject.transform);
                    break;
            }
            if (i != 0)
            {
                go = GameObject.Find("VMCMob");
                go.name = GameObject.FindGameObjectWithTag("model").name + i;
                go.transform.SetParent(lodObject.transform);
                go.transform.localPosition = (new Vector3(GameObject.FindGameObjectWithTag("model").transform.position.x, GameObject.FindGameObjectWithTag("model").transform.position.y + GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y / 2, GameObject.FindGameObjectWithTag("model").transform.position.z));
                go.transform.localScale = new Vector3(GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.z, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.y, GameObject.FindGameObjectWithTag("model").GetComponent<Renderer>().bounds.size.x);
            }

            //Add all the geometry to the LOD level
            Renderer[] renderers = new Renderer[PlayerPrefs.GetInt("CustomShots")+1];
            renderers[0] = go.GetComponent<Renderer>();
            if (entered <= n-1)
            {
                for (int j = 1; j < PlayerPrefs.GetInt("CustomShots")+1; j++)
                {
                    if (go.transform.childCount > 0)
                    {
                        renderers[j] = go.transform.GetChild(j - 1).GetComponent<Renderer>();
                    }
                    enter = true;
                }
                entered++;
            }
            lods[i] = new LOD(1.0F / (i + 1), renderers);
        }
        group.SetLODs(lods);
        group.RecalculateBounds();
    }
}
