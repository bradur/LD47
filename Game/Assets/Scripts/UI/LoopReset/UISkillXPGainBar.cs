using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillXPGainBar : HUDResourceBar
{

    private float runningValue = 0;
    private float currentTarget = 0;
    private float xpGained = 0;
    private float updateSpeed = 1;
    private ReadyCallback xpReadyCallback;
    public void AnimateXpGain(ReadyCallback readyCallback) {
        xpReadyCallback = readyCallback;
        updateSpeed = Configs.main.UI.XpBarUpdateSpeed;
        xpGained = Resource.Value;
        SetCurrentTarget();
        StartCoroutine("GainXP");
    }

    public void SetCurrentTarget() {
        if (xpGained > Resource.XpPerLevel) {
            currentTarget = Resource.XpPerLevel;
            xpGained -= Resource.XpPerLevel;
        } else {
            currentTarget = xpGained;
        }
        
    }

    private IEnumerator GainXP()
    {
        while (runningValue < currentTarget) {
            runningValue += 1;
            float percentage = (runningValue / (Resource.XpPerLevel * 1.0f));
            UpdateView(percentage, runningValue, Resource.XpPerLevel);
            yield return StartCoroutine(Tools.WaitForRealTime(1.0f / updateSpeed));
        }
        if (runningValue >= Resource.XpPerLevel) {
            GainLevel();
        } else {
            xpReadyCallback();
        }
    }


    private void GainLevel() {
        runningValue = 0;
        Resource.Level += 1;
        SetCurrentTarget();
        StartCoroutine("GainXP");
    }

}
