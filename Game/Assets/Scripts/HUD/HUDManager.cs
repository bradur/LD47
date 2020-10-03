using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager main;
    void Awake() {
        main = this;
    }
    private List<HUDResourceBar> resourceBars = new List<HUDResourceBar>();
    private Transform resourceBarContainer;
    private Transform energyBarContainer;
    private Transform xpBarContainer;
    private GameConfig config;

    public void Init() {
        config = Configs.main.Game;
        resourceBarContainer = this.FindChildObject("ResourceBarContainer");
        energyBarContainer = this.FindChildObject("EnergyBarContainer");
        xpBarContainer = this.FindChildObject("XPBarContainer");
        foreach(PlayerResource resource in config.Resources) {
            CreaterResourceBar(resource);
        }
    }

    public void Refresh() {
        foreach(HUDResourceBar resourceBar in resourceBars) {
            resourceBar.Refresh();
        }
    }

    private void CreaterResourceBar(PlayerResource resource) {
        Debug.Log("Creating a resource bar");
        HUDResourceBar newBar = Prefabs.Instantiate<HUDResourceBar>();
        if (resource.Type == PlayerResourceType.Energy) {
            newBar.Init(resource, energyBarContainer);
        } else if (resource.IsSkill) {
            newBar.Init(resource, xpBarContainer);
        } else {
            newBar.Init(resource, resourceBarContainer);
        }
        resourceBars.Add(newBar);
    }
}
