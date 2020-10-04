using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public delegate void ReadyCallback();

public class AnimateTextList : MonoBehaviour
{
    private Text txtTarget;

    private List<string> textList;

    private int currentText = 0;

    private float currentAlpha;

    private bool isRunning = false;
    private ReadyCallback readyCallback;
    private UIConfig config;

    private Text txtInfo;

    public void StartFading(ResetCause cause, ReadyCallback readyCallback)
    {
        config = Configs.main.UI;
        string text = DetermineText(cause);
        if (text == "") {
            readyCallback();
        }
        currentText = 0;
        textList = new List<string>();
        foreach (string line in text.Split('\n'))
        {
            textList.Add(line.Trim());
        }
        if (txtTarget == null)
        {
            txtTarget = this.FindChildObject("txtTarget").GetComponent<Text>();
        }
        if (txtInfo == null) {
            txtInfo = this.FindChildObject("txtInfo").GetComponent<Text>();
            txtInfo.text = "- Press {0} to skip -".Format(config.SkipKey);
        }
        txtInfo.enabled = true;
        this.readyCallback = readyCallback;
        isRunning = true;
        FadeInText();
    }

    private string DetermineText(ResetCause cause)
    {
        string text = "";
        if (cause == ResetCause.Death)
        {
            text = config.DeathResetText[Random.Range(0, config.DeathResetText.Count)];
        }
        else if (cause == ResetCause.EnergyLoss)
        {
            text = config.EnergyLossRestText[Random.Range(0, config.EnergyLossRestText.Count)];
        }
        return text;
    }

    private void FadeOutText()
    {
        currentAlpha = 1f;
        StartCoroutine(FadeOut());
    }

    private void FadeInText()
    {
        if (currentText < textList.Count)
        {
            txtTarget.text = textList[currentText];
            currentAlpha = 0f;
            currentText += 1;
            StartCoroutine(FadeIn());
        }
        else
        {
            isRunning = false;
            txtInfo.enabled = false;
            readyCallback();
        }
    }

    void Update()
    {
        if (isRunning && Input.GetKeyDown(config.SkipKey))
        {
            StopAllCoroutines();
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, 0f);
            FadeInText();
        }
    }

    private IEnumerator FadeIn()
    {
        float startAlpha = txtTarget.color.a;
        while (currentAlpha < 1f)
        {
            currentAlpha += Time.unscaledDeltaTime / config.TextFadeInDuration;
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, currentAlpha);
            yield return null;
        }
        yield return StartCoroutine(Tools.WaitForRealTime(config.TextStayDuration));
        StartCoroutine("FadeOut");
    }

    private void StartFadeOut()
    {
        FadeOutText();
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = txtTarget.color.a;
        while (currentAlpha > 0)
        {
            currentAlpha -= startAlpha * Time.unscaledDeltaTime / config.TextFadeOutDuration;
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, currentAlpha);
            yield return null;
        }
        FadeInText();
    }
}
