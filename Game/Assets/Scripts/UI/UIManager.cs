using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    void Awake()
    {
        main = this;
    }

    private UILoopResetDialog loopResetDialog;
    private ReadyCallback readyCallback;

    private Transform worldSpaceCanvas;


    void Start()
    {
        loopResetDialog = this.FindChildObject("LoopEndDialog").GetComponent<UILoopResetDialog>();
        worldSpaceCanvas = this.FindChildObject("WorldSpaceCanvas");
    }

    public void AfterReset()
    {
        Time.timeScale = 1f;
        readyCallback();
    }

    public void ShowBillboardText(string text, Vector3 worldPosition, Sprite icon=null, Color color=default(Color)) {
        BillboardText bbText = Prefabs.Instantiate<BillboardText>();
        bbText.Initialize(text, worldPosition, worldSpaceCanvas, icon, color);
    }

    public void OpenResetDialog(ResetCause cause, ReadyCallback callback)
    {
        Time.timeScale = 0f;
        readyCallback = callback;
        loopResetDialog.Reset(cause, AfterReset);
    }
}
