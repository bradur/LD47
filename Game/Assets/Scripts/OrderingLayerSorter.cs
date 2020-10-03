using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderingLayerSorter : MonoBehaviour
{
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rend.sortingOrder = (int)(-transform.position.y * 100);
    }
}
