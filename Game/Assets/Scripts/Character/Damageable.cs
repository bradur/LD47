using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    public DamageableType type = DamageableType.CHARACTER;

    [SerializeField]
    Color HurtTint = Color.red;

    [SerializeField]
    float Health = 30.0f;

    [SerializeField]
    float HurtShakeX = 1.0f;

    [SerializeField]
    float HurtShakeY = 0.0f;

    SpriteRenderer[] renderers;
    Color[] originalColors;

    float hurtTimer;
    public static readonly float HURT_DURATION = 0.5f;
    public static readonly float HURT_SHAKE_MAGNITUDE_X = 0.1f;
    public static readonly float HURT_SHAKE_MAGNITUDE_Y = 0.1f;
    Vector3 hurtPosition;
    bool positionsReset = true;

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
            if (HurtShakeX > 0.01f || HurtShakeY > 0.01f)
            {
                var shakeX = HurtShakeX * HURT_SHAKE_MAGNITUDE_X * Mathf.Sin(amount * Mathf.PI * 10);
                var shakeY = HurtShakeY * HURT_SHAKE_MAGNITUDE_Y * Mathf.Sin(amount * Mathf.PI * 10);
                transform.position = hurtPosition + new Vector3(shakeX, shakeY, 0.0f);
            }
        }
        else
        {
            tint(HurtTint, 0.0f);
            if ((HurtShakeX > 0.01f || HurtShakeY > 0.01f) && !positionsReset)
            {
                positionsReset = true;
                transform.position = hurtPosition;
            }
        }
    }

    public void Hurt(float damage)
    {
        if (hurtTimer < Time.time)
        {
            hurtPosition = transform.position;
            positionsReset = false;
        }

        Health -= damage;
        if (Health > 0)
        {
            hurtTimer = Time.time + HURT_DURATION;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
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

public enum DamageableType
{
    CHARACTER,
    WALL
}
