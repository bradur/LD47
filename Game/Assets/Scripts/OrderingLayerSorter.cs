﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerSorter : MonoBehaviour
{
    SpriteRenderer rend;

    int originalOrder;

    [SerializeField]
    GameObject SortingOrderOrigin;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalOrder = rend.sortingOrder;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var sortinOrderOrigin = SortingOrderOrigin == null ? transform : SortingOrderOrigin.transform;
        rend.sortingOrder = originalOrder + (int)(-sortinOrderOrigin.position.y * 100);
    }
}
