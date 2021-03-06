﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField]
    public float Damage;

    [SerializeField]
    public float WallDamage;

    [SerializeField]
    GameObject HitEffect;

    private List<DamageListener> listeners = new List<DamageListener>();

    public void RegisterListener(DamageListener listener)
    {
        listeners.Add(listener);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            if (HitEffect != null)
            {
                var effectPos = other.ClosestPoint(transform.position);
                var effect = Instantiate(HitEffect);
                effect.transform.position = effectPos;
            }

            var damage = damageable.type == DamageableType.WALL ? WallDamage : Damage;
            damageable.Hurt(damage);
            if (damageable.tag == "Player" && damageable.isActiveAndEnabled) {
                PlayerResources.main.Spend(PlayerResourceType.Health, (int)Damage);
            } else {
                if (damageable.XpPerHitPoint > 0)
                {
                    PlayerResources.main.Gain(PlayerResourceType.StrengthSkill, (int)(damageable.XpPerHitPoint * damage));
                }
            }

            foreach (var listener in listeners)
            {
                listener.DamageableHit(damageable);
            }
        }
    }
}
