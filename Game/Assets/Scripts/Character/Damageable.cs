﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    public DamageableType type = DamageableType.CHARACTER;

    [SerializeField]
    Color HurtTint = Color.red;

    [SerializeField]
    public float Health = 30.0f;

    public float MaxHealth;

    [SerializeField]
    float HurtShakeX = 1.0f;

    [SerializeField]
    float HurtShakeY = 0.0f;

    [SerializeField]
    public float XpPerHitPoint = 0;

    [SerializeField]
    public int XpOnDie = 0;

    public bool DontDestroyOnDie = false;

    public bool IsWinCondition = false;

    [SerializeField]
    GameObject DestroyEffect;

    [SerializeField]
    GameObject HurtEffect;

    [SerializeField]
    GameObject DestroyEffectOrigin;

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
        MaxHealth = Health;
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

        if (HurtEffect != null)
        {
            var effectPos = DestroyEffectOrigin == null ? transform.position : DestroyEffectOrigin.transform.position;
            var effect = Instantiate(HurtEffect);
            effect.transform.position = effectPos;
        }

        SoundManager.main.PlaySound(SoundType.Whack);
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
        if (gameObject.tag != "Player")
        {
            if (XpOnDie > 0)
            {
                PlayerResources.main.Gain(PlayerResourceType.StrengthSkill, XpOnDie);
            }
            if (!DontDestroyOnDie)
            {
                if (DestroyEffect != null)
                {
                    var effectPos = DestroyEffectOrigin == null ? transform.position : DestroyEffectOrigin.transform.position;
                    var effect = Instantiate(DestroyEffect);
                    effect.transform.position = effectPos;
                }
                Destroy(gameObject);
            }
            else
            {
                enabled = false;
            }
            SoundManager.main.PlaySound(SoundType.Explode);
            if (IsWinCondition)
            {
                LoopManager.main.Win();
            }
        }
    }

    private void tint(Color color, float amount)
    {
        for (var i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].gameObject.name.Contains("Hay"))
            {
                continue;
            }
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
