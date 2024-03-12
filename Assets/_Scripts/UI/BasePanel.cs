using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    
    #if UNITY_EDITOR
    [ContextMenu("Show Panel")]
    private void EditorShow()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    [ContextMenu("Hide Panel")]
    private void EditorHide()
    {
        var cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    #endif
}
