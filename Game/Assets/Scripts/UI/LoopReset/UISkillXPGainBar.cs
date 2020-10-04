using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillXPGainBar : HUDResourceBar
{

    private float runningValue = 0;
    private float currentTarget = 0;
    private int xpGained = 0;
    private float updateSpeed = 1;
    private ReadyCallback xpReadyCallback;
    private int levelsGained = 0;
    private int levelsAtStart = 0;
    private List<int> xpsPerLevel;

    public void AnimateXpGain(ReadyCallback readyCallback)
    {
        xpReadyCallback = readyCallback;
        updateSpeed = Configs.main.UI.XpBarUpdateSpeed;
        xpGained = Resource.Value;
        levelsAtStart = Resource.Level;

        levelsGained = 0;
        xpsPerLevel = new List<int>();
        while (xpGained >= Resource.XpPerLevel)
        {
            levelsGained++;
            xpGained -= Resource.XpPerLevel;
            xpsPerLevel.Add(Resource.XpPerLevel);
            Resource.Level++;
        }

        SetCurrentTarget();
        StartCoroutine("GainXP");
    }

    public void SetCurrentTarget()
    {
        if (xpGained > Resource.XpPerLevel)
        {
            currentTarget = Resource.XpPerLevel;
            xpGained -= Resource.XpPerLevel;
        }
        else
        {
            currentTarget = xpGained;
        }
    }

    public void Skip()
    {
        StopAllCoroutines();
        Resource.Level = levelsAtStart + levelsGained;
        Resource.Value = xpGained;
        Refresh();
    }

    private IEnumerator GainXP()
    {
        while (runningValue < currentTarget)
        {
            runningValue += 1;
            float percentage = (runningValue / (Resource.XpPerLevel * 1.0f));
            UpdateView(percentage, runningValue, Resource.XpPerLevel);
            yield return StartCoroutine(Tools.WaitForRealTime(1.0f / updateSpeed));
        }
        if (runningValue >= Resource.XpPerLevel)
        {
            GainLevel();
        }
        else
        {
            xpReadyCallback();
        }
    }


    private void GainLevel()
    {
        runningValue = 0;
        Resource.Level += 1;
        SetCurrentTarget();
        StartCoroutine("GainXP");
    }

}
