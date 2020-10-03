using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTracker : MonoBehaviour
{

/*
    private float distanceMoved;
    private float distanceToMoveForXP;
*/
    private Vector2 prevPosition;
    private float distanceMovedForXP;
    private float distanceMovedForEnergy;

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
            distanceMovedForXP = 0;
            PlayerResources.main.SpendEnergy(1);
        }
        if (distanceMovedForEnergy >= 1) {
            distanceMovedForEnergy = 0;
            PlayerResources.main.SpendEnergy(1);
        }
        prevPosition = transform.position;
    }
}
