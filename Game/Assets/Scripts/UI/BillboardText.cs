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
    private Text txtMessage;

    private Animator animator;

    private Image imgIcon;
    private Color defaultColor = new Color(1, 1, 1, 1);

    public void Initialize(string text, Vector3 pos, Transform parent, Sprite icon=null, Color color=default(Color), bool isDialog=false, int fontSize=-1)
    {
        txtTarget = this.FindChildObject("txtTarget").GetComponent<Text>();
        txtMessage = this.FindChildObject("txtMessage").GetComponent<Text>();
        imgIcon = this.FindChildObject("imgIcon").GetComponent<Image>();
        if (icon == null) {
            imgIcon.enabled = false;
        } else {
            imgIcon.sprite = icon;
            imgIcon.color = color;
        }
        if (isDialog) {
            txtTarget.enabled = false;
            txtMessage.enabled = true;
        } else {
            txtTarget.enabled = true;
            txtMessage.enabled = false;
        }
        if (fontSize > 0) {
            if (isDialog) {
                txtMessage.fontSize = fontSize;
            } else {
                txtTarget.fontSize = fontSize;
            }
        }
        rt = GetComponent<RectTransform>();
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        if (isDialog) {
            animator.SetBool("IsDialog", true);
            txtMessage.text = text;
        } else {
            txtTarget.text = text;
        }
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
