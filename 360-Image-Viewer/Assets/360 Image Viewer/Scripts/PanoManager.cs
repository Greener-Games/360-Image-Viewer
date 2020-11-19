using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Random = System.Random;

public class PanoManager : MonoBehaviour
{
    public GameObject pano;

    MeshRenderer meshRenderer;
    Material panoMat;
    MaterialPropertyBlock properties;
    static readonly int MainTexture = Shader.PropertyToID("_MainTexture");

    public UiLoader uiLoader;
    
    public bool showDownloadSize;

    List<string> s = new List<string>()
    {
        "https://i.ibb.co/5FkkrzJ/8hw0clw-360-panorama-miami.jpg",
        "https://i.ibb.co/5FkkrzJ/8hw0clw-360-panorama-miami.jpg",
        "https://i.ibb.co/C5qcfbS/tu09tpc-a-large-bed-in-a-room.jpg",
    };
    
    void Awake()
    {
        panoMat = pano.GetComponent<Renderer>().material;
        meshRenderer = pano.GetComponent<MeshRenderer>();
        properties = new MaterialPropertyBlock();
    }

    void SetPanoImage(Texture tex)
    {
        properties.SetTexture(MainTexture, tex);
        meshRenderer.SetPropertyBlock(properties);
    }

    void SetPanoImage(string url)
    {
        StartCoroutine(GetTexture(url));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetPanoImage(s[UnityEngine.Random.Range(0, s.Count)]);
        }
    }

    IEnumerator GetTexture(string url)
    {
        int downloadSize = 0;
        if (showDownloadSize)
        {
            yield return StartCoroutine(GetFileSize(url, arg0 => downloadSize = arg0));
        }

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            UnityWebRequestAsyncOperation operation = www.SendWebRequest();

            uiLoader.StartLoad();
            while (!operation.isDone)
            { 
                uiLoader.SetPercentage(www.downloadProgress * 100f);

                if (showDownloadSize)
                {
                    uiLoader.UpdateDownloadSize(downloadSize, www.downloadedBytes);
                }
                yield return null;
            }
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log($"Image Downloaded {www.url}");
                Texture myTexture = DownloadHandlerTexture.GetContent(www);
                SetPanoImage(myTexture);
            }
            
            uiLoader.EndLoad();
        }
    }
    
    IEnumerator GetFileSize(string url, UnityAction<int> size)
    {
        UnityWebRequest uwr = UnityWebRequest.Head(url);
        yield return uwr.SendWebRequest();
        size.Invoke(int.Parse(uwr.GetResponseHeader("Content-Length")));
    }

}
