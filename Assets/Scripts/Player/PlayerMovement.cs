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

 
//set _targetPosition to the current position at start; used for test movable tile.



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
    public Vector2 MovingDirection { get { return _movingDirection; } set { _movingDirection = value; } }


    //--dependencies
    //the Tilemapmangerto get the map prefab.
    private TilemapManager _tilemapManager;


    private Animator _animator; 



    private void Awake()
    { 
        _tilemapManager = GameObject.Find("GameManager").GetComponent<TilemapManager>();

        _animator = this.GetComponent<Animator>(); 

        _targetPosition = transform.position;

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
            //ProcessInput();
            _movingDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            if (_movingDirection.x != 0) // Prevents diagonal movement
                _movingDirection.y = 0;


            if (_movingDirection != Vector2.zero && isWalkableTile(_movingDirection))
            {

                _isMoving = true;
                _movingTiles = 1;
                _targetPosition = (Vector2)transform.position + _movingDirection;
                _elapsedTime = 0;

                Debug.Log("start moving");

            }

        }

        else
        if (_isMoving)
        {

            //in each frame update, move a little bit towards the destination.  return end for the current update.
            if ((_elapsedTime + Time.deltaTime) < (_movingTiles / _movingSpeed))
            {
                _elapsedTime += Time.deltaTime;



                Vector3 moveDistance = (Vector3)_movingDirection * _movingSpeed * Time.deltaTime;
      

                Debug.Log("move distance: " + moveDistance);
                Debug.Log("elapsed time: " + _elapsedTime);
                Debug.Log("timestep: " + Time.deltaTime);
                Debug.Log("MovingDirection" + _movingDirection);
                Debug.Log("MovingSpeed" + _movingSpeed);
                Debug.Log("OriginalPos" + transform.position);

                transform.position += moveDistance;

                Debug.Log("NewPos" + transform.position);

            }
            //if still holding the button,  keep moving.
            else if (isWalkableTile(_movingDirection) &&
                (_movingDirection == new Vector2(Input.GetAxisRaw("Horizontal"), 0) ||
                 _movingDirection == new Vector2(0,Input.GetAxisRaw("Vertical")))   )
            {
                //update and keep moving
                _movingTiles++;
                _targetPosition += _movingDirection;

                Debug.Log("trigger keep moving to next tile");

            }
            else
            {
                transform.position = _targetPosition;
                _isMoving = false;
                _elapsedTime = 0;
                _movingTiles = 0;
                _movingDirection = Vector2.zero; 
                Debug.Log("stop moving");
            }

        }


        Debug.Log("playerPosition" + transform.position);

        Debug.Log("PlayerMovement: movingTiles" + _movingTiles);

        SetAnimatorParameters();
         

    }
 
 

    private void ProcessInput()
    {

        //======process input
        //priority : the last input direction.  implement by time stamp.
         
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


    void SetAnimatorParameters()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        if(_isMoving)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }


    }

}
