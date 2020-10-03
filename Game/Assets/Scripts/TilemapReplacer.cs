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
                replaceMapTiles(map, tilePrefabMap);
            }
        }
    }

    private void replaceMapTiles(Tilemap map, List<TilePrefab> tilePrefabMap)
    {
        BoundsInt bounds = map.cellBounds;
        Vector3Int origin = map.origin;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int z = bounds.min.z; z < bounds.max.z; z++)
                {
                    replaceMapTile(map, tilePrefabMap, new Vector3Int(x, y, z));
                }
            }
        }
    }

    private void replaceMapTile(Tilemap map, List<TilePrefab> tilePrefabMap, Vector3Int tilePos)
    {
        TileBase tile = map.GetTile(tilePos);
        if (tile == null) return;

        TilePrefab tileObj = tilePrefabMap.Where(t => t.tile.name == tile.name).FirstOrDefault();

        if (tileObj == null)
        {
            Debug.Log("Tile with the name " + tile.name + " is not defined in TilePrefabConfig.");
            return;
        }

        Item item = tileObj.prefab.GetComponent<Item>();
        if (item != null) {
            PlayerInventory invConf = Configs.main.PlayerInventory;
            if (invConf.PlayerItems.Any(x => x.Type == item.GetItemType())) {
                Debug.Log("Removed item");
                return;
            }
        }

        GameObject prefab = tileObj.prefab;
        GameObject instance = Instantiate(prefab);
        instance.transform.position = 
            map.CellToWorld(tilePos) + new Vector3(map.tileAnchor.x, map.tileAnchor.y, map.tileAnchor.z);

        map.SetTile(tilePos, null);
    }
}
