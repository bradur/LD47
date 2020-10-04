using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DamageListener
{
    void DamageableHit(Damageable damageable);
}
