using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
 
using UnityEngine.UI;

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



    //====public UI
    public Sprite[] countdownSprites;
    public Image countdownImage;

    private AudioSource[] _audioSources;


    //====dependencies 
    private LevelsManager _levelsManager;   
    private TilemapManager _tilemapManager;

    
    private PlayerManager _playerManager;
 


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

        //=======prepare dependencies
        //load levels data on awake
        //get from ManegerComponent 
        _levelsManager = this.GetComponent<LevelsManager>();
        _tilemapManager = this.GetComponent<TilemapManager>();

         
        _audioSources = this.GetComponents<AudioSource>();
        if (_audioSources == null)
        {
            Debug.LogError("GameManager: audioSources not found");
        }


        //====prepare data
        //read the levels data and tilemap prefabs , independent of the level
        _levelsManager.ReadLevelsData(); 
        _tilemapManager.ReadTilemapPrefabs();

        //load certain level
        _levelsManager.SetupLevel(1);

    }


    //-------game logic
    void Start()
    {   
        _playerManager = _levelsManager.PlayerManager;

        StartCoroutine(GameLoop());  
    }

    void Update()
    { 

    }
     
     
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameEntry());
        yield return StartCoroutine(StartCountDown());


        //increment the timer

        bool finalCountDownStarted = false;

        while (_elapsedTime < _timeLimit)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _timeLimit - 10 && !finalCountDownStarted)
            {
                finalCountDownStarted = true;
                StartCoroutine(FinalCountDown());
            }
            yield return null;
        }
    
        //if the game remains 10 seconds, start the final countdown
       

        //game over
        // OnTimeUp()?.Invoke();
        _playerManager.DisablePlayerControl();
        SceneManager.LoadScene("BossFight");
        Debug.Log("Level1: game over, enter boss fight scene");


    }

    private IEnumerator GameEntry()
    {
        //disable player movement
        _playerManager.DisablePlayerControl();

        //wait for 3 seconds
        yield return new WaitForSeconds(2f); 
    }
 
     private IEnumerator StartCountDown()
     {
        //


        //drive UI and audio
        //play audio
        _audioSources[0].Stop();
        _audioSources[1].Play();
         
         for (int i = 0; i < countdownSprites.Length; i++)
         {
             countdownImage.sprite = countdownSprites[i];
             yield return new WaitForSeconds(1f);
         }

         // Optionally, hide the image after countdown ends
         countdownImage.enabled = false;

        //enable player movement
        _playerManager.EnablePlayerControl();

        //relay audio
        _audioSources[1].Stop();
        _audioSources[2].Play(); 


     }


    private IEnumerator FinalCountDown() 
    {
        //play final countdown audio
        _audioSources[2].Stop();
        _audioSources[3].Play();
         
        yield return new WaitForSeconds(10f);

    } 

}
