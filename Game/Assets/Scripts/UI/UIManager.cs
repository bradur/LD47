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
    private Transform titleTextContainer;


    void Start()
    {
        loopResetDialog = this.FindChildObject("LoopEndDialog").GetComponent<UILoopResetDialog>();
        worldSpaceCanvas = this.FindChildObject("WorldSpaceCanvas");
        titleTextContainer = this.FindChildObject("TitleTextContainer");
    }

    public void AfterReset()
    {
        Time.timeScale = 1f;
        readyCallback();
    }

    public void ShowBillboardText(string text, Vector3 worldPosition, Sprite icon=null, Color color=default(Color), bool isDialog=false, int fontSize=-1) {
        BillboardText bbText = Prefabs.Instantiate<BillboardText>();
        bbText.Initialize(text, worldPosition, worldSpaceCanvas, icon, color, isDialog, fontSize);
    }

    public void PlayStartAnimation(bool gameStart) {
        if (loopResetDialog == null) {
            loopResetDialog = this.FindChildObject("LoopEndDialog").GetComponent<UILoopResetDialog>();
        }
        loopResetDialog.PlayStartAnimation(gameStart);
    }
    
    public void CallLoopResetDialogSceneStart() {
        if (loopResetDialog == null) {
            loopResetDialog = this.FindChildObject("LoopEndDialog").GetComponent<UILoopResetDialog>();
        }
        loopResetDialog.SceneLoaded();
    }

    public void ShowDialog(string text, Vector3 worldPosition) {
        BillboardText bbText = Prefabs.Instantiate<BillboardText>();
        bbText.Initialize(text, worldPosition, worldSpaceCanvas, null, Color.white, true, -1);
    }
    public void ShowTitleText(string text) {
        TitleText titleText = Prefabs.Instantiate<TitleText>();
        if (titleTextContainer == null) {
            titleTextContainer = this.FindChildObject("TitleTextContainer");
        }
        titleText.Initialize(text, titleTextContainer);
    }

    public void OpenResetDialog(ResetCause cause, ReadyCallback callback)
    {
        Time.timeScale = 0f;
        readyCallback = callback;
        loopResetDialog.Reset(cause, AfterReset);
    }
}
