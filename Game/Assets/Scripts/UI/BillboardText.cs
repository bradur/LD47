using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardText : MonoBehaviour
{

    private Transform camTransform;
    private Quaternion originalRotation;
    private RectTransform rt;

    private Text txtTarget;

    private Animator animator;

    private Image imgIcon;
    private Color defaultColor = new Color(1, 1, 1, 1);

    public void Initialize(string text, Vector3 pos, Transform parent, Sprite icon=null, Color color=default(Color))
    {
        txtTarget = this.FindChildObject("txtTarget").GetComponent<Text>();
        imgIcon = this.FindChildObject("imgIcon").GetComponent<Image>();
        if (icon == null) {
            imgIcon.enabled = false;
        } else {
            imgIcon.sprite = icon;
            imgIcon.color = color;
        }
        rt = GetComponent<RectTransform>();
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        txtTarget.text = text;
        transform.SetParent(parent);
        transform.position = pos;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.rotation = originalRotation * camTransform.rotation;
    }

}
