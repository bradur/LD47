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
    private Animator animator;

    private List<UISkillXPGainBar> xpSkillGainBars = new List<UISkillXPGainBar>();

    void Start() {
        animator = GetComponent<Animator>();
        container.SetActive(true);
        config = Configs.main.UI;
        gameConfig = Configs.main.Game;
        uiSkillXpGainBarContainer = this.FindChildObject("xpBarContainer");
        animateTextList = this.FindChildObject("animateTextList").GetComponent<AnimateTextList>();
        skillDisplay = this.FindChildObject("SkillDisplay").gameObject;
        txtPressAnyKeyToContinue = this.FindChildObject("txtPressAnyKeyToContinue").GetComponent<Text>();
        txtPressAnyKeyToContinue.enabled = false;
        skillDisplay.SetActive(false);
        container.SetActive(false);
    }

    public void PlayStartAnimation(bool gameStart){
        Time.timeScale = 0;
        container.SetActive(true);
        animator.enabled = true;
        animator.SetTrigger("Open");
        animator.SetBool("GameStart", gameStart);
    }


    int xpBarsToUpdate = 0;
    int xpBarsUpdated = 0;

    private bool isResetting = false;
    private bool waitForKey = false;
    private bool waitForSkillSkipKey = false;
    private ResetCause cause;

    public void DisplayGainedXp() {
        skillDisplay.SetActive(true);
        xpBarsToUpdate = gameConfig.Resources.FindAll(resource => resource.IsSkill).Count;
        xpBarsUpdated = 0;
        waitForSkillSkipKey = true;
        txtPressAnyKeyToContinue.enabled = true;
        txtPressAnyKeyToContinue.text = "- Press {0} to skip -".Format(config.SkipKey);
        foreach(PlayerResource resource in gameConfig.Resources) {
            if (resource.IsSkill) {
                UISkillXPGainBar uiSkillXpGainBar = Prefabs.Instantiate<UISkillXPGainBar>();
                uiSkillXpGainBar.Init(resource, uiSkillXpGainBarContainer, false);
                uiSkillXpGainBar.AnimateXpGain(XpGainAnimateReady);
                xpSkillGainBars.Add(uiSkillXpGainBar);
            }
        }
    }

    public void XpGainAnimateReady() {
        if (waitForSkillSkipKey) {
            xpBarsUpdated += 1;
            if (xpBarsUpdated >= xpBarsToUpdate) {
                WaitForExit();
            }
        }
    }

    private void WaitForExit() {
        txtPressAnyKeyToContinue.text = "- Press {0} to continue -".Format(config.SkipKey);
        waitForKey = true;
    }

    public void AfterTextList() {
        DisplayGainedXp();
    }

    public void Reset(ResetCause cause, ReadyCallback readyCallback) {
        if (!isResetting) {
            this.cause = cause;
            isResetting = true;
            animator.enabled = true;
            animator.SetTrigger("Open");
            allReadyCallback = readyCallback;
        }
    }

    public void ShowContainer() {
        container.SetActive(true);
    }

    public void StartTextList() {
        animateTextList.StartFading(cause, AfterTextList);
    }

    public void Exit() {
        skillDisplay.SetActive(false);
        container.SetActive(false);
        if (allReadyCallback != null) {
            allReadyCallback();
        }
        StartScene();
    }

    public void GameStartDialog() {
        animateTextList.StartFading(config.StartStory, AfterGameStart);
    }


    public void AfterGameStart() {
        animator.SetTrigger("GameStartFade");
    }

    public void AfterGameStartFade() {
        animator.SetBool("GameStart", false);
        animator.enabled = false;
        container.SetActive(false);
        StartScene();
    }

    public void StartScene() {
        Time.timeScale = 1f;
        if (LoopManager.main.LoopCount > 0) {
            UIManager.main.ShowTitleText("Loop {0}".Format(LoopManager.main.LoopCount));
        }

    }

    void Update() {
        if (waitForKey && Input.GetKeyDown(config.SkipKey)) {
            txtPressAnyKeyToContinue.enabled = false;
            waitForKey = false;
            animator.SetTrigger("Close");
        }
        if (!waitForKey && waitForSkillSkipKey && Input.GetKeyDown(config.SkipKey)) {
            foreach(UISkillXPGainBar xpBar in xpSkillGainBars) {
                xpBar.Skip();
            }
            WaitForExit();
        }
    }
}

public enum ResetCause {
    None,
    Death,
    EnergyLoss
}
