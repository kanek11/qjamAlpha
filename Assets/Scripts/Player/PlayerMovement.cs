using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



//readme:
//this component is to be attached under Player
//to control the player movement

//specifications:
//the player move one tile at a time. and only 4 directions, not diagonal.
//assume the tilemap is a 1x1 unit grid . 

//the last input direction is the moving direction.
//when start moving, the player won't respond until it reaches the destination.
 

 
//bug history:
//don't compare with destination directly,  because of numerical precision, compare is not reliable. use < and snap to destination instead.
//within a single frame,  the snap is not so noticeable but might need to be improved in the future.

 



public class PlayerMovement : MonoBehaviour

{

    //====properties
    [SerializeField]
    private float _movingSpeed = 4f;
    public float MovingSpeed { get { return _movingSpeed; } set { _movingSpeed = value; } }


    //control variables might needed for player movement;
    private float _elapsedTime = 0f; 
    private bool _isMoving = false;  

    Vector2 _targetPosition = Vector2.zero;
    private int _movingTiles = 0;
    private Vector2 _movingDirection = Vector2.zero;


    //--dependencies
    //the Tilemapmangerto get the map prefab.
    private TilemapManager _tilemapManager;



    private void Awake()
    { 
        _tilemapManager = GameObject.Find("GameManager").GetComponent<TilemapManager>();

    }

     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMoving)
        {
            ProcessInput();

            if (_movingDirection != Vector2.zero && isWalkableTile(_movingDirection))
            {

                _isMoving = true;  
                _movingTiles = 1; 
                _targetPosition = (Vector2)transform.position + _movingDirection; 
                _elapsedTime = 0;
 
            }
             
        }

        if(_isMoving)
        {

            //in each frame update, move a little bit towards the destination.  return end for the current update.
            if ( (_elapsedTime + Time.deltaTime) < (_movingTiles / _movingSpeed) )
            {
                _elapsedTime += Time.deltaTime;

                transform.position += (Vector3)_movingDirection * _movingSpeed * Time.deltaTime;

            }
            //if still holding the button,  keep moving.
            else if ( isWalkableTile(_movingDirection)  && (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) )
            {
                //update and keep moving
                _movingTiles++;
                _targetPosition += _movingDirection;

            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;
                _elapsedTime = 0;
                _movingTiles = 0;
                _movingDirection = Vector2.zero; 
            }

        }

 


    }
 
 

    private void ProcessInput()
    {

        //======process input
        //priority : the last input direction.  implement by time stamp.

        float lastHorizontalPressTime = 0;
        float lastVerticalPressTime = 0;

        if (Input.GetButtonDown("Horizontal"))
        {
            lastHorizontalPressTime = Time.time;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            lastVerticalPressTime = Time.time;
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
       
            //get the last input direction
            //this should return integer,  for example (1,0) if press right;
            if (lastHorizontalPressTime > lastVerticalPressTime)
            {
                
                _movingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            }
            else
            { 
                _movingDirection = new Vector2(0, Input.GetAxisRaw("Vertical"));
            }


        }

    }

  

    bool isWalkableTile(Vector2 Direction)

    { 
        //======test if the destination is valid

        Vector2 targetPosition = _targetPosition + Direction;
        if (targetPosition.x < 0 || targetPosition.x >= _tilemapManager.CurrentMapWidth || targetPosition.y < 0 || targetPosition.y >= _tilemapManager.CurrentMapHeight)
        {
            Debug.Log("destination out of bound");
            _movingDirection = Vector2.zero;
            return false;
        }

        else
        {
            TilemapPrefab targetTile = _tilemapManager.GetTilemapPrefabByPosition((int)targetPosition.x, (int)targetPosition.y);

            if (!targetTile.GetComponent<TilemapPrefab>().IsWalkable)
            {
                Debug.Log("destination is not walkable");
                _movingDirection = Vector2.zero;
                return false;
            }

        }

        return true;
         
    }

 
}
