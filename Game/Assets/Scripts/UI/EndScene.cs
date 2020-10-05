using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    private Text loopCounterText;

    void Start()
    {
        LoopManager loopManager = LoopManager.main;
        string loopCounterString = "You escaped the wicked timeloop in {0} loops.";
        if (loopManager == null)
        {
            loopCounterString = loopCounterString.Format("∞");
            loopCounterText.text = loopCounterString;
        }
        else
        {
            loopCounterString = loopCounterString.Format(loopManager.LoopCount.ToString());
            loopCounterText.text = loopCounterString;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        /*else if (Input.GetKeyDown(KeyCode.R))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }*/
    }
}
