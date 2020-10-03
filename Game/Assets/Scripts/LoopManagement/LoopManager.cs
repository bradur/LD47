using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager main;

    private int loopCount = 0;

    void Awake()
    {
        this.transform.parent = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoopManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        main = this;
        DontDestroyOnLoad(this.gameObject);
        Configs.main.PlayerInventory.Init();
    }

    void Start()
    {
        HUDManager.main.Init();
        PlayerResources.main.Init();
        Reset();
    }

    public void Reset(bool reload = false)
    {
        PlayerResources.main.Reset();
        HUDManager.main.Refresh();

        if (reload)
        {
            loopCount++;
            Debug.Log("Loop #" + loopCount + " starting");
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
