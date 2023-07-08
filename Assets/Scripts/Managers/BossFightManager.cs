using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{

    public GameObject Player;
    public GameObject Enemy;

    
    private float initialDamage = 0;
    private float currentDamage = 0;

    private int _buttonPressCount = 0;
    public int ButtonPressCount { get { return _buttonPressCount; } set { _buttonPressCount = value; } }

    [SerializeField]
    private float _timeLeft;
    public float TimeLimit { get { return _timeLeft; } set { _timeLeft = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Enemy = GameObject.Find("Enemy");

        
        CalculateDamage();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if time is up, game over
        _timeLeft -= Time.deltaTime; 
        if (_timeLeft <= 0 || Enemy.GetComponent<EnemyHealth>().Health <= 0)
        {
            GameOver();
        }
         
        //if input enter, increment buttonPressCount
        if (Input.GetKeyDown(KeyCode.Return))
        { 
            _buttonPressCount++;
            float Damage = initialDamage * (0.8f + _buttonPressCount * 0.02f);
            Enemy.GetComponent<EnemyHealth>().SetEnemyHealth(Damage);


            Debug.Log("BossFight: button pressed, count:" + _buttonPressCount);
        }
        


    }


    void CalculateDamage()
    {
        Attributes enemyAttribute = Enemy.GetComponent<Enemy>().EnemyAttribute;

        //get from the players - inventory - potion.potion. Attributes.RGB
        Dictionary<Attributes, int> potion = GameObject.Find("PotionData").GetComponent<PotionData>().PotionAttributes;


        Attributes weakness = (Attributes) ( ( (int)enemyAttribute -1 +3 ) % 3 );  //prevent negative
        Attributes neutral = (Attributes) ( ( (int)enemyAttribute + 1) % 3 );
        Attributes strength = enemyAttribute;

        Debug.Log("Bossfight: weakness: " + weakness + " neutral: " + neutral + " strength: " + strength);


        initialDamage = potion[Attributes.B] * 2 + potion[neutral] - potion[strength];
        currentDamage = initialDamage * 0.8f;
        Enemy.GetComponent<EnemyHealth>().SetEnemyHealth(currentDamage);

        Debug.Log("Bossfight: initial damage: " + currentDamage);


    }


    void GameOver()
    {

        UnityEditor.EditorApplication.isPlaying = false;


    }


}
