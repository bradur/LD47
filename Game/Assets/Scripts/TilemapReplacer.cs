using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapReplacer : MonoBehaviour
{
    void Start()
    {
        List<TilePrefab> tilePrefabMap = Configs.main.TileConfig.mapping;
        foreach (Tilemap map in FindObjectsOfType<Tilemap>())
        {
            if (map.tag == "ReplaceableMap")
            {
                BoundsInt bounds = map.cellBounds;
                Vector3Int origin = map.origin;

                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    for (int y = bounds.min.y; y < bounds.max.y; y++)
                    {
                        for (int z = bounds.min.z; z < bounds.max.z; z++)
                        {
                            TileBase tile = map.GetTile(new Vector3Int(x, y, z));
                            if (tile == null) continue;

                            TilePrefab tileObj = tilePrefabMap.Where(t => t.tile.name == tile.name).FirstOrDefault();

                            if (tileObj == null)
                            {
                                Debug.Log("Tile with the name " + tile.name + " is not defined in TilePrefabConfig.");
                                continue;
                            }

                            GameObject prefab = tileObj.prefab;
                            GameObject instance = Instantiate(prefab);
                            //instance.transform.position = new Vector3(origin.x + x, origin.y + y, origin.z + z);
                            instance.transform.position = map.CellToWorld(new Vector3Int(x, y, z)) + new Vector3(map.tileAnchor.x, map.tileAnchor.y, map.tileAnchor.z);

                            map.SetTile(new Vector3Int(x, y, z), null);
                        }
                    }
                }
            }
        }
    }
}
