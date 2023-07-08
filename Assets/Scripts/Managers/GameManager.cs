using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

using UnityEngine.SceneManagement;


//readme:
//the original loadScene is async method.
//sync: the problem will freeze until the scene is done.
//async: the problem will continue to run while the scene is loading.

//that means by default, Unity native loadScene will not wait for the scene to load like in normal games.
//to make the game black out while loading, we need to use async + coroutine.  by query it's progress, you can make a loading bar etc.


public class GameManager : MonoBehaviour
{
    //events
    //public UnityEvent OnTimeUp;

    //impose a singleton pattern
    public static GameManager Instance { get;  private set; }


    //game states
   // private enum _difficulty{ easy, medium, hard };/

    //for other settings

    [SerializeField]
    private float _timeLimit ;  //seconds
    public float TimeLimit { get => _timeLimit; set => _timeLimit = value; }

    [SerializeField]
    private float _elapsedTime = 0;
    public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }


    //levels manager
    private LevelsManager _levelsManager;  



    private void Awake()
    {
        //singleton pattern
        if (Instance == null) 
        {

            Instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }

        else  
            Destroy(this.gameObject);


        //load levels data 
        //get from ManegerComponent 
        _levelsManager = GetComponent<LevelsManager>(); 

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
        Debug.Log("Level1:game ove, enter boss fight scene");

    }


     


}
