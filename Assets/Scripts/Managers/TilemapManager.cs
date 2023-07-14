using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//readme:
//TilemapManager is responsible for the tilemap stuff
//load prefabs, store the data in the dictionary.
//setup the map by the data from the level manager.  and set the data matrix as current level data.


//caution : load the prefabs before using them.
//as always, the calling order should be managed by one script,  instead of use Awake() blindly.


//bug history:
//when the tile is not found, or when the tile is spawn,  specify the tile as background tile. 



public class TilemapManager  : MonoBehaviour
{

    //the data that hold all prefabs assets
    private Dictionary<int, TilemapPrefab> _tilemapPrefabsData;

    public Vector2 PlayerSpawnPoint; 


    //===data for current tilemap
    private LevelsData.LevelData _currentLevelData;
    public LevelsData.LevelData CurrentLevelData { get { return _currentLevelData; } } 
    
    public int CurrentLevelNumber { get { return _currentLevelData.number; } }
    public int CurrentMapWidth { get { return _currentLevelData.map.GetLength(0); } }
    public int CurrentMapHeight { get { return _currentLevelData.map.GetLength(1); } }


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
                    PlayerSpawnPoint = new Vector2(x, y); 
                    Debug.Log("Player spawn point: " + PlayerSpawnPoint);

                    //set to 0 for the tilemap as normal background;
                    map[x, y] = 0;
                    continue;
                }
                
                TilemapPrefab prefab = GetTilemapPrefabById(map[x,y]);
                if (prefab == null)
                {
                    Debug.Log("Tilemap prefab not found for id " + map[x, y] + "set as background") ;
                    map[x, y] = 0;
                    continue;
                }

                Vector3 position = new Vector3(x, y, 0); // The z-value is typically 0 in 2D games
                GameObject prefabGameObject = prefab.gameObject;
                Instantiate(prefabGameObject, position, Quaternion.identity);



            }
        }


    }

    
}
