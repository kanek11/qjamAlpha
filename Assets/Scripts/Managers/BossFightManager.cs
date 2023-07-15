using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




enum Rating { F, A, S, SS };



public class BossFightManager : MonoBehaviour
{
    
    public Sprite[] ScoreSprites;
    public Image ScoreImage;
     
    public GameObject[] EnemyPrefabs;
    
    private GameObject _currentEnemy;
    public GameObject CurrentEnemy { get { return _currentEnemy; } set { _currentEnemy = value; } }

    public Image EnemyImage; 


    private AudioSource[] _audioSources;
     

    public Dictionary<Attributes, int> Potion;


    private float _initialDamage = 0;

    private float _currentDamage = 0;
    public float CurrentDamage { get { return _currentDamage; } set { _currentDamage = value; } }


    //for UI
    private int _buttonPressCount = 0;
    public int ButtonPressCount { get { return _buttonPressCount; } set { _buttonPressCount = value; } }



    [SerializeField]
    private float _timeLimit;
    public float TimeLimit { get { return _timeLimit; } set { _timeLimit = value; } }

    private float _elapsedTime = 0;
    public float ElapsedTime { get { return _elapsedTime; } set { _elapsedTime = value; } }



    private bool _hasReachedTimeLimit = false;

     


    // Start is called before the first frame update
    void Awake()
    { 
        if(GlobalState.EnemyIndex >= EnemyPrefabs.Length)
        {
            Debug.Log("Bossfight: _currentEnemy index out of range, reset to 0");
            GlobalState.EnemyIndex = 0;
        }
        if( EnemyPrefabs.Length == 0)
        {
            Debug.Log("Bossfight: EnemyPrefabs is empty");
        }

        _currentEnemy = EnemyPrefabs[GlobalState.EnemyIndex]; 
        Debug.Log("Bossfight: _currentEnemy index: " + GlobalState.EnemyIndex);

        //set the Image of the enemy
        if(EnemyImage == null) 
            Debug.Log("Bossfight: EnemyImage is null");

        EnemyImage.sprite = _currentEnemy.GetComponent<SpriteRenderer>().sprite;

       

        //get from the players - inventory - potion.potion. Attributes.RGB
        Potion = GlobalState.PotionAttributes; 

        //for debugging, if potion has length 0, initialize it
       
        if (Potion.Count == 0)
        {
            Debug.Log("Bossfight: Potion is not set, initialized by default ");
            Potion.Add(Attributes.R, 1200);
            Potion.Add(Attributes.G, 1000);
            Potion.Add(Attributes.B, 1000);
        } 
         
        _audioSources = GetComponents<AudioSource>();
        
    }


    void Start()
    {
        StartCoroutine(DisplayPotion());



    }
     

    // Update is called once per frame
    void Update()
    { 

    }
     
    IEnumerator EnterCountDown()
    {
        while (!_hasReachedTimeLimit)
        {

            //if time is up, game over
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= _timeLimit)
            {
                EnterRateScore();
                _hasReachedTimeLimit = true;
            }

            //if input enter, increment buttonPressCount
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _buttonPressCount++;
                _currentDamage = _initialDamage * (0.8f + _buttonPressCount * 0.02f);
                _currentEnemy.GetComponent<EnemyHealth>().SetEnemyHealth(_currentDamage);

                Debug.Log("BossFight: button pressed, count:" + _buttonPressCount);
            }

            yield return null;
        }

       
    }


    void CalculateDamage()
    {

        Attributes enemyAttribute = _currentEnemy.GetComponent<Enemy>().EnemyAttribute;

        Attributes weakness = (Attributes) ( ( (int)enemyAttribute -1 +3 ) % 3 );  //prevent negative
        Attributes neutral = (Attributes) ( ( (int)enemyAttribute + 1) % 3 );
        Attributes strength = enemyAttribute;

        Debug.Log("Bossfight: weakness: " + weakness + " neutral: " + neutral + " strength: " + strength);


        _initialDamage = Potion[Attributes.B] * 2 + Potion[neutral] - Potion[strength];
        _currentDamage = _initialDamage * 0.8f;
        _currentEnemy.GetComponent<EnemyHealth>().SetEnemyHealth(_currentDamage);

        Debug.Log("Bossfight: initial damage: " + _currentDamage);


    }



    void EnterRateScore()
    {
        //set the fightCanvas to be invisible
        CanvasGroup FightCanvasGroup =  GameObject.Find("FightCanvas").GetComponent<CanvasGroup>();
        FightCanvasGroup.alpha = 0;
        FightCanvasGroup.interactable = false;
        FightCanvasGroup.blocksRaycasts = false;


        CanvasGroup RateScoreCanvasGroup = GameObject.Find("RateScoreCanvas").GetComponent<CanvasGroup>();
        RateScoreCanvasGroup.alpha = 1;
        RateScoreCanvasGroup.interactable = true;
        RateScoreCanvasGroup.blocksRaycasts = true;



        //calculate score based on current damage and the health of the enemy
        //the rating is SS, S, ABC, F , total 0-5,  5 for the best, 0 for the worst
        //for every 100 above the enemy health, add 1 to the rating

        Rating rating = Rating.F;

         float enemyHealth = _currentEnemy.GetComponent<EnemyHealth>().Health;
         
        //if success

        if (enemyHealth <= 0)
        {
            // Calculate the rating based on excess damage, clamp it within the valid range.
            int excessDamage = Mathf.Clamp((int)(_currentDamage - enemyHealth) / 300, 0, 3);
            rating = (Rating)excessDamage;

            //set the score image
        }

        ScoreImage.sprite = ScoreSprites[(int)rating];

        StartCoroutine(RateScoreAfter());

        //======play the audio.
        _audioSources[0].Stop();

        //Switch case: play 1 if rating is F, 1 if rating is E, D, 2 if A,B,C, 3 if rating is S or SS
        switch (rating)
        {
            case Rating.F: 
                _audioSources[1].Play();
                break; 
            case Rating.A:
                _audioSources[2].Play();
                break;
            case Rating.S:
                _audioSources[3].Play();
                break;
            case Rating.SS:
                _audioSources[3].Play();
                break;
        }
         

    }
        

    private IEnumerator RateScoreAfter()
    {
        yield return new WaitForSeconds(5);

        GameOver();
    }
 


    void GameOver()
    {
        GlobalState.Reset();
        SceneManager.LoadScene("Title");  
    }


    private IEnumerator DisplayPotion()
    {
        yield return new WaitForSeconds(3f);



        CanvasGroup RateScoreCanvasGroup = GameObject.Find("PotionCanvas").GetComponent<CanvasGroup>();
        RateScoreCanvasGroup.alpha = 0;
        RateScoreCanvasGroup.interactable = false;
        RateScoreCanvasGroup.blocksRaycasts = false;


        // Code here will run after 3 seconds
        CanvasGroup FightCanvasGroup = GameObject.Find("FightCanvas").GetComponent<CanvasGroup>();
        FightCanvasGroup.alpha = 1;
        FightCanvasGroup.interactable = true;
        FightCanvasGroup.blocksRaycasts = true;


        CalculateDamage();
        StartCoroutine(EnterCountDown());

    }

}
