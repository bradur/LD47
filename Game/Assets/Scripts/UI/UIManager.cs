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

    void Start() {
        loopResetDialog = this.FindChildObject("LoopEndDialog").GetComponent<UILoopResetDialog>();
    }

    public void AfterReset() {
        Time.timeScale = 1f;
        readyCallback();
    }

    public void OpenResetDialog(ResetCause cause, ReadyCallback callback) {
        Time.timeScale = 0f;
        readyCallback = callback;
        loopResetDialog.Reset(cause, AfterReset);
    }
}
