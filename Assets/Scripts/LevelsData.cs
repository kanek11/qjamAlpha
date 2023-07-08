
using System;
using System.Collections;
using System.Collections.Generic;


//read me:
//read the levels data so that returns a dictionary of level number and level data
//the leveldata itself could be read from a csv file or a json file;


[System.Serializable]
public class LevelsData
{

    public List<LevelData> levels;

    [Serializable]
    public class LevelData
    {
        public int number;
        //a 2D array to hold the map data
        public int[,] map;

    }

}
