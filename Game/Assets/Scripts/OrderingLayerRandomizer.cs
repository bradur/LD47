using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerRandomizer : MonoBehaviour
{
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sortingOrder = Random.Range(int.MinValue, int.MaxValue);
    }
}
