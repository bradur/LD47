using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillXPGainBar : HUDResourceBar
{

    private float runningValue = 0;
    private int currentTarget = 0;
    private int xpGained = 0;
    private float updateSpeed = 1;
    private ReadyCallback xpReadyCallback;
    private int levelsGained = 0;
    private int levelsAtStart = 0;
    private int currentLevel = 0;
    private int currentLevelXpBound = 0;
    private List<int> xpsPerLevel;

    public void AnimateXpGain(ReadyCallback readyCallback)
    {
        xpReadyCallback = readyCallback;
        updateSpeed = Configs.main.UI.XpBarUpdateSpeed;
        xpGained = Resource.Value;
        levelsAtStart = Resource.Level;
        currentLevel = levelsAtStart;

        levelsGained = 0;
        XPContainer.SetActive(false);
        if (xpGained > 0) {
            BarContainer.SetActive(true);
            IconContainer.SetActive(true);
            LevelContainer.SetActive(true);
        }
        xpsPerLevel = new List<int>();
        currentLevelXpBound = Resource.XpPerLevel;
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
        if (xpsPerLevel.Count > 0) {
//            Debug.Log("Settings currentTarget to: {0} (count: {1})".Format(xpsPerLevel[0], xpsPerLevel.Count));
            currentTarget = xpsPerLevel[0];
            currentLevelXpBound = currentTarget;
            xpsPerLevel.RemoveAt(0);
        } else {
            currentLevelXpBound = Resource.XpPerLevel;
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
            float percentage = (runningValue / (currentLevelXpBound * 1.0f));
            UpdateView(percentage, runningValue, currentLevelXpBound, (currentLevel + 1).ToString());
            XPContainer.SetActive(false);
            yield return StartCoroutine(Tools.WaitForRealTime(1.0f / updateSpeed));
        }
        if (runningValue >= currentLevelXpBound)
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
        currentLevel += 1;
        XPContainer.SetActive(false);
        SetCurrentTarget();
        UpdateView(0, runningValue, currentLevelXpBound, (currentLevel + 1).ToString());
        StartCoroutine("GainXP");
    }

}
