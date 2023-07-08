using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

//read me:

//[] open for rewrite

//LevelsManager.ReadLevelsData() returns a dictionary of level number and level data

//the csv files are in a folder "LevelsData" in the Resources folder, read as TextAsset
//the names are "Level_1", "Level_2", etc. 


public class LevelsManager : MonoBehaviour
{

    //path to the levels data file
    //[forNow] only one level in csv file, a 2D array.
    private const string LEVELS_DATA_PATH = "LevelsData";



    //======to be initialized.

    //data required to set up a level
    private Dictionary<int, LevelsData.LevelData> _levelsData; 


    //====the managers
    private TilemapManager _tilemapManager;
    private PlayerManager _playerManager;

    //caution: retrieve the camera after the scene is loaded.
    private CameraController _cameraController;


    void Awake()
    {
        //get the tilemap manager component
        _tilemapManager = this.GetComponent<TilemapManager>();
        _playerManager = this.GetComponent<PlayerManager>();

    }
     

    //on start, read the levels data
    private void Start()
    { 
    } 


    //[alpha]
    //a public method to load the levels data , called as the game starts 
    public Dictionary<int , LevelsData.LevelData> ReadLevelsData()
    {

        //the dictionary to hold the levels data
        Dictionary<int, LevelsData.LevelData> levelsData = new Dictionary<int, LevelsData.LevelData>();


        TextAsset[] mapFiles = Resources.LoadAll<TextAsset>("LevelsData");


        //parse the file name , if it's called "Level_1", then the level number is 1
         
        //caution: the world xy is "flip" from a matrix index,
        //to get consistent view as the map specification,  we flip the y axis when reading the map data.

        foreach (TextAsset mapFile in mapFiles)
        {
            int levelNumber;
            int[,] map;

            //create a new level data
            LevelsData.LevelData levelData = new LevelsData.LevelData();

            string fileName = mapFile.name;
            string[] fileNameParts = fileName.Split('_');
            levelNumber = int.Parse(fileNameParts[1]);

            //read the map data,  read the rows
            string[] rows = mapFile.text.Split('\n');

            //important : make sure the size matches;  
            map = new int[rows[0].Split(',').Length, rows.Length]; 
         
            for (int i = 0; i < rows.Length ; i++)
            {
                string[] line = rows[i].Split(',');
                for (int j = 0; j < line.Length; j++)
                {
                    //important: flip the y axis
                    if(line[j] != "")
                    map[j, rows.Length-1 -i] = int.Parse(line[j]);
                }
            }

      
            levelData.number = levelNumber;
            levelData.map = map;

            //add the level data to the dictionary
            levelsData.Add(levelNumber, levelData);

        }

        return levelsData;  

    }
     

    //set up the level by level number
    //awake, ready the level before any Start() is called.
    public void SetupLevel(int levelNumber)
    {
        //read the levels data
        _levelsData = ReadLevelsData();

        //get the level data by level number
        LevelsData.LevelData levelData = _levelsData[levelNumber];

      
        _tilemapManager.ReadTilemapPrefabs();

        _tilemapManager.SetupTilemap(levelData);  


        /*  //set up the player
          _playerManager.SetupPlayer();

          //set up the enemies
          _enemiesManager.SetupEnemies();

          //set up the camera
          _cameraManager.SetupCamera();

          //set up the UI
          _uiManager.SetupUI();

          //set up the game logic
          _gameLogicManager.SetupGameLogic();
  */
    }




}
