
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
    public float EnergyPerDistanceMoved { get { return EnergyPerDistanceMoved; } }

    [SerializeField]
    private Gradient xpBarGradient;
    public Gradient XPBarGradient { get { return xpBarGradient; } }
}