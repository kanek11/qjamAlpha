using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    //properties of enemy
    private Attributes _enemyAttribute;
    public Attributes EnemyAttribute { get { return _enemyAttribute; } set { _enemyAttribute = value; } }

    // Start is called before the first frame update
  
}
