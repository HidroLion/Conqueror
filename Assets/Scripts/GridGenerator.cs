using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] public int sides;
    [SerializeField] Tile tilesPrefab;
    public Tile[,] spawnedPrefab;

    [SerializeField] Transform cameraPosition;
    [SerializeField] PlayerController playerController;

    int xPos, yPos;

    private void Awake()
    {
        spawnedPrefab = new Tile[sides, sides];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < sides; i++)
        {
            for (int j = 0; j < sides; j++)
            {
                spawnedPrefab[i, j] = Instantiate(tilesPrefab, new Vector3(i,j), Quaternion.identity);
                spawnedPrefab[i, j].name = $"Tile {i} - {j}";
            }
        }

        xPos = Random.Range(1, sides);
        yPos = Random.Range(1, sides);
        cameraPosition.position = new Vector3(xPos, yPos, 0);
        playerController.StartPlayer();
    }
}
