using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PhoneGenerator : MonoBehaviour
{
    public Camera firstPersonCamera;
    public GameObject template;
    public Material emptyMaterial;
    public Canvas canvasPrefab;

    byte[] GetImage(string path)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }
            return reader.bytes;
        }
        else
        {
            return File.ReadAllBytes(path);
        }
    }

    string[] GetText(string path)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }
            return reader.text.Split('\n');
        }
        else
        {
            return File.ReadAllLines(path);
        }
    }

    public void GeneratePhoneGO(string dir, Transform anchor, Vector3 position, Quaternion rotation)
    {
        GameObject ret = Instantiate(template);

        var frontQuad = ret.transform.Find("Front").GetComponent<Renderer>();
        var backQuad = ret.transform.Find("Back").GetComponent<Renderer>();
        var leftQuad = ret.transform.Find("Left").GetComponent<Renderer>();
        var rightQuad = ret.transform.Find("Right").GetComponent<Renderer>();

        var phonePath = Path.Combine(Application.streamingAssetsPath, dir);
        //var frontJpg = Resources.Load<Texture2D>(dir + "/" + "front");



        if (true)
        {
            var tex = new Texture2D(2, 2);
            var frontPath = Path.Combine(phonePath, "front.jpg");

            tex.LoadImage(GetImage(frontPath));

            frontQuad.material = new Material(emptyMaterial);
            frontQuad.material.SetTexture("_MainTex", tex);
        }
        if (true)
        {
            var tex = new Texture2D(2, 2);
            var backPath = Path.Combine(phonePath, "back.jpg");

            tex.LoadImage(GetImage(backPath));

            backQuad.material = new Material(emptyMaterial);
            backQuad.material.SetTexture("_MainTex", tex);
        }
        //if (File.Exists(Path.Combine(dir, "left.jpg")))
        //{
        //    var tex = new Texture2D(2, 2);
        //    tex.LoadImage(File.ReadAllBytes(Path.Combine(phonePath, "left.jpg")));

        //    leftQuad.material = new Material(emptyMaterial);
        //    leftQuad.material.SetTexture("_MainTex", tex);
        //}
        //if (File.Exists(Path.Combine(dir, "right.jpg")))
        //{
        //    var tex = new Texture2D(2, 2);
        //    tex.LoadImage(File.ReadAllBytes(Path.Combine(phonePath, "right.jpg")));

        //    rightQuad.material = new Material(emptyMaterial);
        //    rightQuad.material.SetTexture("_MainTex", tex);
        //}

        var dimensionsPath = Path.Combine(phonePath, "dimensions.txt");
        var lines = GetText(dimensionsPath);

        var width = float.Parse(lines[0]);
        var height = float.Parse(lines[1]);
        var depth = float.Parse(lines[2]);

        var specsPath = Path.Combine(phonePath, "specs.txt");
        var linesSpecs = GetText(specsPath);

        var diag = linesSpecs[0];
        var rez = linesSpecs[1];
        var baterie = linesSpecs[2];
        var spatiu = linesSpecs[3];
        var ram = linesSpecs[4];

        ret.transform.rotation = rotation;
        ret.transform.localScale = new Vector3(width / 1000f, height / 1000f, depth / 1000f);
        //ret.transform.position = position + Vector3.up * ret.transform.localScale.z / 2;
        ret.transform.position = position + Vector3.up * ret.transform.localScale.y / 1.5f;
        ret.transform.parent = anchor;


        var canvas = Instantiate(canvasPrefab);
        canvas.transform.GetChild(1).GetComponent<Text>().text = "Diagonal: " + diag;
        canvas.transform.GetChild(2).GetComponent<Text>().text = "Camera Resolution: " + rez;
        canvas.transform.GetChild(3).GetComponent<Text>().text = "Battery: " + baterie;
        canvas.transform.GetChild(4).GetComponent<Text>().text = "Storage: " + spatiu;
        canvas.transform.GetChild(5).GetComponent<Text>().text = "RAM: " + ram;

        canvas.GetComponent<LookAtCamera>().phoneTransform = ret.transform;
        canvas.GetComponent<LookAtCamera>().firstPersonCamera = firstPersonCamera.transform;

        ret.GetComponent<RotationScript>().infoCanvas = canvas;
    }

    void Start()
    {
        //GeneratePhoneGO("huawei", transform, new Vector3(10f, 0, 0), Quaternion.identity);
    }
}
