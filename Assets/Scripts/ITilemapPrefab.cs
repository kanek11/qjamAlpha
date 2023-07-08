using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
interface ITilemapPrefab   
{
    public int Id { get; set; }
    public string Description { get; set; }

    public bool IsWalkable { get; set; } 


}
