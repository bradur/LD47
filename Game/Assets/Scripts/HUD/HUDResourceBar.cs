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
    private RectTransform rtBarFill;
    private Vector2 rtBarOriginalSize;
    private Gradient gradient;
    public void Init(PlayerResource resource, Transform container)
    {
        gradient = Configs.main.Game.XPBarGradient;
        this.resource = resource;
        imgBarFill = this.FindChildObject("fill").GetComponent<Image>();
        rtBarFill = imgBarFill.GetComponent<RectTransform>();
        rtBarOriginalSize = rtBarFill.localScale;
        imgIcon = this.FindChildObject("icon").GetComponent<Image>();
        imgIcon.sprite = resource.Icon;
        imgIcon.color = resource.Color;
        txtValue = this.FindChildObject("value").GetComponent<Text>();
        transform.SetParent(container, false);
        Refresh();
    }

    public void Refresh()
    {
        float percentage = 1;
        if (resource.IsSkill)
        {
            percentage = (resource.Value * 1.0f) / (resource.XpPerLevel * 1.0f);
            txtValue.text = "{0} / {1}".Format(
                resource.Value,
                resource.XpPerLevel
            );

        }
        else
        {
            percentage = ((resource.Value * 1.0f) / (resource.InitialValue * 1.0f));
            txtValue.text = "{0} / {1}".Format(
                resource.Value,
                resource.InitialValue
            );
        }

        float xSize = Mathf.Clamp(percentage * rtBarOriginalSize.x, 0, 1);
        rtBarFill.localScale = new Vector2(xSize, rtBarOriginalSize.y);
        imgBarFill.color = gradient.Evaluate(xSize);
    }


}
