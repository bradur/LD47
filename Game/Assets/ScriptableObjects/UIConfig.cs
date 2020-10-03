
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UIConfig", menuName = "New UIConfig")]
public class UIConfig : ScriptableObject
{
    [SerializeField]
    private KeyCode skipKey;
    public KeyCode SkipKey { get { return skipKey; } }
    

    [SerializeField]
    private float textFadeInDuration = 1;
    public float TextFadeInDuration { get { return textFadeInDuration; } }
    [SerializeField]
    private float textFadeOutDuration = 1;
    public float TextFadeOutDuration { get { return textFadeOutDuration; } }
    [SerializeField]
    private float textStayDuration = 1;
    public float TextStayDuration { get { return textStayDuration; } }

    [SerializeField]
    private float xpBarUpdateSpeed = 1;
    public float XpBarUpdateSpeed { get { return xpBarUpdateSpeed; } }

    [TextArea(1, 15)]
    [SerializeField]
    private List<string> energyLossResetText;
    public List<string> EnergyLossRestText { get { return energyLossResetText; } }
    [TextArea(1, 15)]
    [SerializeField]
    private List<string> deathResetText;
    public List<string> DeathResetText { get { return deathResetText; } }
}