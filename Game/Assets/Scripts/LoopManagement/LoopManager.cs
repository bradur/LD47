using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager main;
    
    void Awake() {
        main = this;
    }

    void Start () {
        HUDManager.main.Init();
        PlayerResources.main.Init();
        Reset();
    }

    public void AfterReset() {

    }

    public void Reset() {
        PlayerResources.main.Reset();
        HUDManager.main.Refresh();
    }
}
