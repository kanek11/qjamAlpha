using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapManager : MonoBehaviour
{

    //the data that hold all prefabs assets
    private Dictionary<int, TilemapPrefab> _tilemapPrefabsData;
     
    //dependencies
    [SerializeField]
    private GameObject _playerPrefab; 
    public GameObject PlayerPrefab { get { return _playerPrefab; }  set { _playerPrefab = value; } }
    
    [SerializeField]
    private PlayerManager _playerManager;
    public PlayerManager PlayerManager { get { return _playerManager; } set { _playerManager = value; } }




    //===data for current tilemap
    private LevelsData.LevelData _currentLevelData;
    public LevelsData.LevelData CurrentLevelData { get { return _currentLevelData; } }   
    public int CurrentLevelNumber { get { return _currentLevelData.number; } }
    public int CurrentMapWidth { get { return _currentLevelData.map.GetLength(0); } }
    public int CurrentMapHeight { get { return _currentLevelData.map.GetLength(1); } }


    public Vector2 PlayerSpawnPoint;


    void Awake()
    {
       
    }

    void Start()
     { 

    }

    public void ReadTilemapPrefabs()
    {
        //=================================================================================================
        //read all the tilemap prefabs data and store them in the dictionary, key is the id property.
        _tilemapPrefabsData = new Dictionary<int, TilemapPrefab>();

        GameObject[] tilemapPrefabs = Resources.LoadAll<GameObject>("TilemapPrefabs");

        foreach (GameObject tilemapPrefab in tilemapPrefabs)
        {
            TilemapPrefab tilemapPrefabBase = tilemapPrefab.GetComponent<TilemapPrefab>();
            _tilemapPrefabsData.Add(tilemapPrefabBase.Id, tilemapPrefabBase);

        }

        Debug.Log("TilemapManager: " + _tilemapPrefabsData.Count + " tilemap prefabs loaded.");
    }



    //public methods to access the data, get prefab by id
    public TilemapPrefab GetTilemapPrefabById(int id)
    {
        //if the id is not in the dictionary, return null
        if (!_tilemapPrefabsData.ContainsKey(id))
        {
            return null;
        }
        else
        //return by dictionary key
        return _tilemapPrefabsData[id];
    }


    public TilemapPrefab GetTilemapPrefabByPosition(int x, int y)
    {
        //return by dictionary key
        return GetTilemapPrefabById(_currentLevelData.map[x, y]);
    }


    //set up the tilemap
    public void SetupTilemap (LevelsData.LevelData levelData)
    {
        //store the level data
        _currentLevelData = levelData;

        //get the map data
        int[,] map = levelData.map;

        //initiate the tilemap by the map data
        //the map data is a 2D array of integers, each integer represents a tilemap prefab.

        //specifications:
        //the 0s are the background grass
        //we don't draw background grass one at a time, but draw a big background in tiled drawmode, size of the map size

        //====draw background 
        //GameObject Grass = Instantiate(GetTilemapPrefabById(0), new Vector2(0, 0), Quaternion.identity, this.transform).gameObject; 

 

        //0 is row of matrix

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++) 
            { 

                // Get the prefab for the current tile id, skip if it's 0
                if(map[x,y] == 0)
                {
                    continue;
                }
                else if(map[x,y] == -1)
                {
                    _playerManager.SpawnPoint = new Vector2(x, y);
                    _playerManager.PlayerInstance = Instantiate(_playerPrefab, _playerManager.SpawnPoint, Quaternion.identity);
                    _playerManager.SetupPlayer();
                    Debug.Log("Player spawn point: " + _playerManager.SpawnPoint);

                    //set to 0 for the tilemap as normal background;
                    map[x, y] = 0;
                    continue;
                }
                
                TilemapPrefab prefab = GetTilemapPrefabById(map[x,y]);
                if (prefab == null)
                {
                    Debug.Log("Tilemap prefab not found for id " + map[x,y]);
                    continue;
                }

                // Instantiate at its position, the tiles are assumed 1x1 unit in size
                Vector2 position = new Vector2(x,y);
                Instantiate(prefab, position, Quaternion.identity, this.transform);
            }
        }


    }

    
}
