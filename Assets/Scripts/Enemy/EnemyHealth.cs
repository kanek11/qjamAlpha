using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] 
    private float _maxHealth;
    public  float MaxHealth { get { return _maxHealth; } }  

    private float _health; 
    public float Health { get { return _health; } }


    public UnityEvent OnHealthChanged;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void SetEnemyHealth(float health)
    {
        _health = _maxHealth - health;
        OnHealthChanged?.Invoke(); 

        Debug.Log("Enemy health: health changed, current: " + _health);
    }
     

}
