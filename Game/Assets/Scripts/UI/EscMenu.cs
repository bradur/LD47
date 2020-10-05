using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public KeyCode EscKey = KeyCode.Q;
    public KeyCode FinalKey = KeyCode.Y;
    public GameObject Container;

    // Start is called before the first frame update
    void Start()
    {
        Container.SetActive(false);
    }

    bool opened = false;
    void Update()
    {
        if (Input.GetKeyDown(EscKey) || Input.GetKeyDown(KeyCode.Escape)) {
            if (opened) {
                Container.SetActive(false);
                Time.timeScale = 1;
                opened = false;
            } else {
                Container.SetActive(true);
                Time.timeScale = 0f;
                opened = true;
            }
        }
        if (Input.GetKeyDown(FinalKey)) {
            if (opened) {
                Application.Quit();
            }
        }
    }
}
