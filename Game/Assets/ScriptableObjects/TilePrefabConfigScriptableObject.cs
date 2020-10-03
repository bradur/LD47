using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TilePrefabConfig", menuName = "Game/TilePrefabConfig", order = 0)]
public class TilePrefabConfigScriptableObject : ScriptableObject {
    public List<TilePrefab> mapping;
}

[Serializable]
public class TilePrefab {
    public TileBase tile;
    public GameObject prefab;
}