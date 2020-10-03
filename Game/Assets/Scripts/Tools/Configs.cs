using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Configs : MonoBehaviour
{

    public static Configs main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private GameConfig gameConfig;
    public GameConfig Game { get { return gameConfig; } }

    [SerializeField]
    private UIConfig uiConfig;
    public UIConfig UI { get { return uiConfig; } }

    [SerializeField]
    private TilePrefabConfigScriptableObject tileConfig;
    public TilePrefabConfigScriptableObject TileConfig { get { return tileConfig; } }

}
