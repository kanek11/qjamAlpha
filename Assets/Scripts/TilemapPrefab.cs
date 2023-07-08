using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapPrefab : MonoBehaviour, ITilemapPrefab
{

    [SerializeField]
    private int _id;
    public int Id { get => _id; set => _id = value; }


    [SerializeField]
    private string _description;
    public string Description { get => _description; set => _description = value; }

    //this is a tilemap game so we set
    //walkable tile by hand instead of a collider.
    //this also potentially allows for advanced pathfinding .
    [SerializeField]
    private bool _isWalkable;
    public bool IsWalkable { get => _isWalkable; set => _isWalkable = value; }



}
