using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDResourceBar : MonoBehaviour
{
    private Text txtValue;
    private Image imgIcon;
    private Image imgBarFill;
    private Image imgBorder;
    private Image imgBackground;
    private PlayerResource resource;
    public PlayerResource Resource { get { return resource; } }
    private RectTransform rtBarFill;
    private Vector2 rtBarOriginalSize;
    private Gradient gradient;

    private Text txtLevel;
    private GameObject levelContainer;
    private GameObject iconContainer;

    public void Init(PlayerResource resource, Transform container, bool refresh = true)
    {
        gradient = Configs.main.Game.XPBarGradient;
        this.resource = resource;
        imgBorder = this.FindChildObject("border").GetComponent<Image>();
        imgBackground = this.FindChildObject("background").GetComponent<Image>();
        imgBarFill = this.FindChildObject("fill").GetComponent<Image>();
        rtBarFill = imgBarFill.GetComponent<RectTransform>();
        rtBarOriginalSize = rtBarFill.localScale;
        imgIcon = this.FindChildObject("icon").GetComponent<Image>();
        imgIcon.sprite = resource.Icon;
        imgIcon.color = resource.Color;
        levelContainer = this.FindChildObject("levelContainer").gameObject;
        iconContainer = this.FindChildObject("iconContainer").gameObject;
        if (resource.IsSkill)
        {
            levelContainer.SetActive(true);
            txtLevel = this.FindChildObject("levelTxt").GetComponent<Text>();
        }
        else
        {
            levelContainer.SetActive(false);
        }

        txtValue = this.FindChildObject("value").GetComponent<Text>();
        transform.SetParent(container, false);
        if (resource.IsSkill) {
            Hide();
        }
        if (refresh)
        {
            Refresh();
        }

    }

    public void Show()
    {
        imgBorder.enabled = true;
        imgBackground.enabled = true;
        txtValue.enabled = true;
        imgIcon.enabled = true;
        imgBarFill.enabled = true;
        if (resource.IsSkill)
        {
            levelContainer.SetActive(true);
        }
        iconContainer.SetActive(true);
    }

    public void Hide()
    {
        imgBorder.enabled = false;
        imgBackground.enabled = false;
        txtValue.enabled = false;
        imgIcon.enabled = false;
        imgBarFill.enabled = false;
        levelContainer.SetActive(false);
        iconContainer.SetActive(false);
    }


    public void Refresh()
    {
        float percentage = 1;
        float current = resource.Value;
        float max = 1;
        if (resource.IsSkill)
        {
            max = resource.XpPerLevel;
            percentage = (resource.Value * 1.0f) / (resource.XpPerLevel * 1.0f);
        }
        else
        {
            max = resource.InitialValue;
            percentage = ((resource.Value * 1.0f) / (resource.InitialValue * 1.0f));
        }
        UpdateView(percentage, current, max);
    }

    public void UpdateView(float percentage, float current, float max)
    {
        if (resource.IsSkill && resource.TotalXp > 0) {
            Show();
        }
        txtValue.text = "{0} / {1}".Format(
            current,
            max
        );
        if (resource.IsSkill)
        {
            txtLevel.text = (resource.Level + 1).ToString();
        }

        float xSize = Mathf.Clamp(percentage * rtBarOriginalSize.x, 0, 1);
        rtBarFill.localScale = new Vector2(xSize, rtBarOriginalSize.y);
        imgBarFill.color = gradient.Evaluate(xSize);
    }


}
