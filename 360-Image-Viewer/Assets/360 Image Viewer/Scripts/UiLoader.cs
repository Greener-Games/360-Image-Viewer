using System;
using UnityEngine;
using UnityEngine.UI;

public class UiLoader : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public Image fill;
    
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    public void StartLoad()
    {
        fill.fillAmount = 0;
        canvasGroup.alpha = 1;
    }

    public void SetPercentage(float wwwDownloadProgress)
    {
        fill.fillAmount = wwwDownloadProgress / 100f;
    }

    public void EndLoad()
    {
        canvasGroup.alpha = 0;
    }
    
    public void UpdateDownloadSize(int downloadSize, ulong DownloadedBytes)
    {
        //TODO:Implement this logic
    }
}