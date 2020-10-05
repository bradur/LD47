using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour
{
    public static LoopManager main;

    private int loopCount = 0;
    public int LoopCount { get { return loopCount; } }

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
        PlayerResources.main.Init();
        loopCount = 0;
        Reset();
        UIManager.main.PlayStartAnimation(true);
    }

    public void AfterReset()
    {

    }

    public void Reset(bool reload = false)
    {
        HUDManager.main.Refresh();

        if (reload)
        {
            loopCount++;

            UIManager.main.OpenResetDialog(PlayerResources.main.GetResetCause(), AfterDialog);
        }
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        UIManager.main.CallLoopResetDialogSceneStart();
    }

    public void AfterDialog()
    {
        //Debug.Log("Loop #" + loopCount + " starting");
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        if (loopCount > 1)
        {
            UIManager.main.PlayStartAnimation(false);
        }
    }


}
