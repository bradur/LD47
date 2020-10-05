using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerSorter : MonoBehaviour
{
    SpriteRenderer rend;

    int originalOrder;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        originalOrder = rend.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        rend.sortingOrder = originalOrder * 100 + (int)(-transform.position.y * 100);
    }
}
