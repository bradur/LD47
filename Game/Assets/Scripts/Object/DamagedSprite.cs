using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    SpriteRenderer rend;

    Damageable damageable;

    int destructionLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        var healthPercentage = Mathf.Clamp(damageable.Health / damageable.MaxHealth, 0f, 1f);
        var newDestructionlevel = (int)((sprites.Length - 1) * (1.0f - healthPercentage));
        if (newDestructionlevel > destructionLevel)
        {
            destructionLevel = newDestructionlevel;
            rend.sprite = sprites[destructionLevel];
        }
    }
}
