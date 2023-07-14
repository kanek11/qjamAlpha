using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//readme:
//Player should be ready at Start(),  as it is part of the level preparation.

//when the camera reach limit of the map, it stops.  we use Clamp() to limit the camera position.

//camera height by  Camera.main.orthographicSize,  
//camera width by Camera.main.orthographicSize * Camera.main.aspect  or Screen.width/Screen.height  

//x = width, y = height



public class CameraController : MonoBehaviour
{
    //fornow: just follow the player
    public GameObject player;  
    private Transform _target;

 
    public float Smoothing = 0.3f;


    //data
    private float _cameraHeight;
    private float _cameraWidth;

    private Vector2 minBounds;
    private Vector2 maxBounds;



    // Start is called before the first frame update
    void Start()
    { 

        player = GameObject.Find("Player");
        _target = player.transform;
        if (player == null)
        {
            Debug.LogError("CameraController: _playerTransform is not found.");
        }
        Vector3 targetPostion = new Vector3(_target.position.x, _target.position.y , this.transform.position.z);
                                            

        //get the camera height and width
        _cameraHeight = Camera.main.orthographicSize;
        _cameraWidth = _cameraHeight * Camera.main.aspect;

        Debug.Log("CameraController: camera height: " + _cameraHeight + "width: " + _cameraWidth);


        TilemapManager _tilemapManager = GameObject.Find("GameManager").GetComponent<TilemapManager>();
        //get the map bounds
        minBounds =  new Vector2(0,0); 
        maxBounds = new Vector2(_tilemapManager.CurrentMapWidth, _tilemapManager.CurrentMapHeight);

        //Debug.Log("CameraController: minBounds: " + minBounds + " maxBounds: " + maxBounds);

    }
 

    // Update is called once per frame
    void Update()
    {

        //follow the player, don't change the z position
        //Debug.Log("Updated, called , is target null?" + (_target == null));
        if(_target == null)
        {
            Debug.LogError("CameraController: camera target is null.");
            return;
        }
         

        float targetX = Mathf.Clamp(_target.position.x, minBounds.x + _cameraWidth - 0.5f, maxBounds.x - _cameraWidth + 0.5f);
        float targetY = Mathf.Clamp(_target.position.y, minBounds.y + _cameraHeight- 0.5f, maxBounds.y - _cameraHeight + 0.5f);

       // Debug.Log("original target: " + _target.position);
       // Debug.Log("clamped targetX: " + targetX + " targetY: " + targetY);


        Vector3 targetPostion= new Vector3(targetX, targetY, this.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPostion, Smoothing);


    }

   


}
