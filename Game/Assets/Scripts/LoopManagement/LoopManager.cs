using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager main;

    void Awake()
    {
        this.transform.parent = null;
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LoopManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        main = this;
        DontDestroyOnLoad(this.gameObject);
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
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
