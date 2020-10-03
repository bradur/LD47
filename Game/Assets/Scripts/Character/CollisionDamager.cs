using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamager : MonoBehaviour
{
    [SerializeField]
    public float Damage;

    public Dictionary<DamageableType, float> DamageMultipliers = new Dictionary<DamageableType, float>()
    {
        { DamageableType.CHARACTER, 1.0f },
        { DamageableType.WALL, 1.0f }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            var multiplier = DamageMultipliers[damageable.type];
            damageable.Hurt(multiplier * Damage);
            PlayerResources.main.Gain(PlayerResourceType.StrengthSkill, Configs.main.Game.StrengthXPPerHit);
        }
    }
}
