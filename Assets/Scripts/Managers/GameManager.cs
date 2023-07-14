using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using UnityEngine.SceneManagement; 


//readme:

//game manager is responsible for the game logic, such as time limit, score, etc.


//bug history:

//the original loadScene is async method. 
//sync: the problem will freeze until the scene is done.
//async: the problem will continue to run while the scene is loading.  

// means loadScene will not wait for the scene to load completely,  we have to handle it ourselves.
//to make the game black out while loading, we need to use async + coroutine.  by query it's progress, you can make a loading bar etc.


public class GameManager : MonoBehaviour
{
   

    //impose a singleton pattern
    public static GameManager Instance { get;  private set; }

    //events
    //public UnityEvent OnTimeUp;

    //game states
    //private enum _difficulty{ easy, medium, hard };/


    //====game logic

    [SerializeField]
    private float _timeLimit ;  //seconds
    public float TimeLimit { get => _timeLimit; set => _timeLimit = value; }

    [SerializeField]
    private float _elapsedTime = 0;
    public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }


    //====dependencies 
    private LevelsManager _levelsManager;   
    private TilemapManager _tilemapManager;



    private void Awake()
    {
        //singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        } 

        //load levels data on awake
        //get from ManegerComponent 
        _levelsManager = this.GetComponent<LevelsManager>();
        _tilemapManager = this.GetComponent<TilemapManager>();


        //read the levels data and tilemap prefabs , independent of the level
        _levelsManager.ReadLevelsData(); 
        _tilemapManager.ReadTilemapPrefabs();

        //load certain level
        _levelsManager.SetupLevel(1);

    }


    //-------game logic
    void Start()
    {  

        StartCoroutine(GameLoop());

    }

    void Update()
    { 

    }
     
     
    private IEnumerator GameLoop()
    {

        //increment the timer

        while(_elapsedTime < _timeLimit)
        {
            _elapsedTime += Time.deltaTime; 
            yield return null;
          
        }

        //game over
        // OnTimeUp()?.Invoke();
        SceneManager.LoadScene("BossFight");
        Debug.Log("Level1: game over, enter boss fight scene");

    }


     


}
