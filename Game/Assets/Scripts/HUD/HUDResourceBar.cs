using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDResourceBar : MonoBehaviour
{
    private Text txtValue;
    private Image imgIcon;
    private Image imgBarFill;
    private PlayerResource resource;
    public void Init(PlayerResource resource, Transform container) {
        this.resource = resource;
        imgBarFill = this.FindChildObject("fill").GetComponent<Image>();
        imgIcon = this.FindChildObject("icon").GetComponent<Image>();
        imgIcon.sprite = resource.Icon;
        imgIcon.color = resource.Color;
        txtValue = this.FindChildObject("value").GetComponent<Text>();
        transform.SetParent(container, false);
        Refresh();
    }

    public void Refresh() {
        txtValue.text = "{0} / {1}".Format(
            resource.InitialValue,
            resource.Value
        );
    }


}
