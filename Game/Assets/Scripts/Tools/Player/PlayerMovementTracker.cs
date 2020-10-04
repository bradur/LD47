using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTracker : MonoBehaviour
{

    private Vector2 prevPosition;
    private float distanceMovedForXP = 0;
    private float distanceMovedForEnergy = 0;

    private GameConfig config;
    void Start()
    {
        config = Configs.main.Game;
        prevPosition = transform.position;
    }

    void LateUpdate()
    {
        distanceMovedForXP += Vector2.Distance(prevPosition, transform.position);
        distanceMovedForEnergy += Vector2.Distance(prevPosition, transform.position);
        if (distanceMovedForXP >= config.DistanceMovedPerXP) {
            distanceMovedForXP -= config.DistanceMovedPerXP;
            PlayerResources.main.Gain(PlayerResourceType.AthleticsSkill, 1);
        }
        var distancePerEnergy = PlayerResources.main.GetDistanceTraveledPerEnergy();
        if (distanceMovedForEnergy >= distancePerEnergy) {
            distanceMovedForEnergy -= distancePerEnergy;
            PlayerResources.main.SpendEnergy(1);
        }
        prevPosition = transform.position;
    }
}
