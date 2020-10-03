using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UILoopResetDialog : MonoBehaviour
{
    private AnimateTextList animateTextList;
    private UIConfig config;
    private GameConfig gameConfig;

    private Transform uiSkillXpGainBarContainer;
    private ReadyCallback allReadyCallback;
    private Text txtPressAnyKeyToContinue;

    [SerializeField]
    private GameObject container;

    private GameObject skillDisplay;

    void Start() {
        container.SetActive(true);
        config = Configs.main.UI;
        gameConfig = Configs.main.Game;
        uiSkillXpGainBarContainer = this.FindChildObject("xpBarContainer");
        animateTextList = this.FindChildObject("animateTextList").GetComponent<AnimateTextList>();
        skillDisplay = this.FindChildObject("SkillDisplay").gameObject;
        txtPressAnyKeyToContinue = this.FindChildObject("txtPressAnyKeyToContinue").GetComponent<Text>();
        txtPressAnyKeyToContinue.text = "- Press {0} to continue -".Format(config.SkipKey);
        txtPressAnyKeyToContinue.enabled = false;
        skillDisplay.SetActive(false);
        container.SetActive(false);
    }


    int xpBarsToUpdate = 0;
    int xpBarsUpdated = 0;

    private bool waitForKey = false;

    public void DisplayGainedXp() {
        skillDisplay.SetActive(true);
        xpBarsToUpdate = gameConfig.Resources.FindAll(resource => resource.IsSkill).Count;
        xpBarsUpdated = 0;
        foreach(PlayerResource resource in gameConfig.Resources) {
            if (resource.IsSkill) {
                UISkillXPGainBar uiSkillXpGainBar = Prefabs.Instantiate<UISkillXPGainBar>();
                uiSkillXpGainBar.Init(resource, uiSkillXpGainBarContainer, false);
                uiSkillXpGainBar.AnimateXpGain(XpGainAnimateReady);
            }
        }
    }

    public void XpGainAnimateReady() {
        xpBarsUpdated += 1;
        if (xpBarsUpdated >= xpBarsToUpdate) {
            txtPressAnyKeyToContinue.enabled = true;
            waitForKey = true;
        }
    }

    public void AfterTextList() {
        DisplayGainedXp();
    }

    public void Reset(ResetCause cause, ReadyCallback readyCallback) {
        container.SetActive(true);
        allReadyCallback = readyCallback;
        animateTextList.StartFading(cause, AfterTextList);
    }

    void Update() {
        if (waitForKey && Input.GetKeyDown(config.SkipKey)) {
            txtPressAnyKeyToContinue.enabled = false;
            waitForKey = false;
            allReadyCallback();
            skillDisplay.SetActive(false);
            container.SetActive(false);
        }
    }
}

public enum ResetCause {
    None,
    Death,
    EnergyLoss
}
