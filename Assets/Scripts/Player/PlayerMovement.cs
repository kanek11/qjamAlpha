using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//readme:
//this component is to be attached under Player
//to control the player movement

//specifications:
//the player move one tile at a time. and only 4 directions, not diagonal.
//assume the tilemap is a 1x1 unit grid . 

//the last input direction is the moving direction.
//when start moving, the player won't respond to input until it reaches the destination.

//always keep the player position as integer. if increment to a float near the destination, round it to integer.


//the player has a kinetic rigidbody . 
//the kinetic information can be queried by the rigidbody component. such as velocity.




public class PlayerMovement : MonoBehaviour

{

    [SerializeField]
    //properties
    private float _movingSpeed = 4f;
    public float MovingSpeed { get { return _movingSpeed; } set { _movingSpeed = value; } }

    private Vector2 _movingDirection = Vector2.zero;


    //====states
    private bool _isMoving = false; 


    //-components
    private Rigidbody2D _rigidbody2D;

    //--dependencies
    //the Tilemapmangerto get the map prefab.
    private TilemapManager _tilemapManager;



    private void Awake()
    {
       

        _rigidbody2D = GetComponent<Rigidbody2D>();

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

            if (_movingDirection != Vector2.zero)
            {
                //Debug.Log("start moving in direction " + _movingDirection + "with speed " + _movingSpeed);

                //start moving, the coroutine will update the rigidbody in each frame until it reaches the destination.
                StartCoroutine(MoveToDestination());

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


    IEnumerator MoveToDestination()
    {
        //======test if the destination is valid

        Vector2 targetPosition = (Vector2)transform.position + _movingDirection;
        if (targetPosition.x < 0 || targetPosition.x >= _tilemapManager.CurrentMapWidth || targetPosition.y < 0 || targetPosition.y >= _tilemapManager.CurrentMapHeight)
        {
            Debug.Log("destination out of bound");
            _movingDirection = Vector2.zero;
            yield break;
        }

        else
        {
            TilemapPrefab targetTile = _tilemapManager.GetTilemapPrefabByPosition((int)targetPosition.x, (int)targetPosition.y);

            if (!targetTile.GetComponent<TilemapPrefab>().IsWalkable)
            {
                Debug.Log("destination is not walkable");
                _movingDirection = Vector2.zero;
                yield break;
            }

        }

        //========ok to move now


        _isMoving = true;

        float elapsedTime = 0; 

        //in each frame update, move a little bit towards the destination.  return end for the current update.
        while(elapsedTime < 1/ _movingSpeed)
        {
            elapsedTime += Time.deltaTime;
 
            Move();

            yield return null;
        }

        //===end the movement 
        //set the position to the destination by transform.
        transform.position = targetPosition;
        _isMoving = false;
        _movingDirection = Vector2.zero;
        //Debug.Log("end moving");

    }



    private void Move()
    {
        
        Vector2 movement =  _movingSpeed  * _movingDirection * Time.deltaTime;

        // Apply this movement to the position
        transform.position += (Vector3)movement;


    }


}
