using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : MonoBehaviour
{
    private Text txtTarget;

    private Animator animator;

    public void Initialize(string text, Transform parent)
    {
        txtTarget = this.FindChildObject("txtTarget").GetComponent<Text>();
        txtTarget.enabled = true;
        txtTarget.text = text;
        animator = GetComponent<Animator>();
        transform.SetParent(parent, false);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
