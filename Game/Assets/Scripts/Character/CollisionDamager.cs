using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField]
    public float Damage;

    [SerializeField]
    public float WallDamage;

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
            var damage = damageable.type == DamageableType.WALL ? WallDamage : Damage;
            damageable.Hurt(damage);
            if (damageable.tag == "Player") {
                PlayerResources.main.Spend(PlayerResourceType.Health, (int)Damage);
            } else {
                PlayerResources.main.Gain(PlayerResourceType.StrengthSkill, damageable.XpPerHit);
            }

            foreach (var listener in listeners)
            {
                listener.DamageableHit(damageable);
            }
        }
    }
}
