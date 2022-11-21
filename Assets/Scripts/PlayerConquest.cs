using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConquest : MonoBehaviour
{
    [SerializeField] Color teamColor;
    [SerializeField] public string playerName; 
    [SerializeField] public GridGenerator grids;

    public void TakeTerrain()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        if (grids.spawnedPrefab[x,y].teamName != playerName)
        {
            grids.spawnedPrefab[(int)transform.position.x, (int)transform.position.y].ChangeColor(playerName, teamColor);
            Debug.Log($"[HDD] - You Take the Terrotory {x} , {y}");
        }
    }
}
