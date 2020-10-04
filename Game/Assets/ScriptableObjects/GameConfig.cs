
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameConfig", menuName = "New GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private List<PlayerResource> resources = new List<PlayerResource>();
    public List<PlayerResource> Resources { get { return resources; } }

    [SerializeField]
    private float distanceMovedPerXp = 2;
    public float DistanceMovedPerXP { get { return distanceMovedPerXp; } }

    [SerializeField]
    private float baseDistanceWalkedPerEnergy = 1;
    public float BaseDistanceWalkedPerEnergy { get { return baseDistanceWalkedPerEnergy; } }

    [SerializeField]
    private int strengthXPPerHit = 1;
    public int StrengthXPPerHit { get { return strengthXPPerHit; } }

    [SerializeField]
    private int energySpentByHit = 5;
    public int EnergySpentByHit { get { return energySpentByHit; } }

    [SerializeField]
    private Gradient xpBarGradient;
    public Gradient XPBarGradient { get { return xpBarGradient; } }

    [SerializeField]
    private float[] moveSpeeds;
    public float[] MoveSpeeds { get { return moveSpeeds; } }

    [SerializeField]
    private float[] bootsEfficiency;
    public float[] BootsEfficiency { get { return bootsEfficiency; } }
}