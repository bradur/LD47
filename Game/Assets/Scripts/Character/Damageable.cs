using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    Color HurtTint = Color.red;

    SpriteRenderer[] renderers;
    Color[] originalColors;

    float hurtTimer;
    public static readonly float HURT_DURATION = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        originalColors = new Color[renderers.Length];
        for (var i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hurtTimer > Time.time)
        {
            float amount = (hurtTimer - Time.time) / HURT_DURATION;
            tint(HurtTint, amount);
        }
        else
        {
            tint(HurtTint, 0.0f);
        }
    }

    public void Hurt(float damage)
    {
        hurtTimer = Time.time + HURT_DURATION;
    }

    private void tint(Color color, float amount)
    {
        for (var i = 0; i < renderers.Length; i++)
        {
            var finalColor = Color.Lerp(originalColors[i], color, amount);
            renderers[i].color = finalColor;
        }
    }
}
