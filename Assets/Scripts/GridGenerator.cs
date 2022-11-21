using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //Variables iniciales para crear el tablero de juego
    [SerializeField] Tile tilesPrefab;
    public Tile[,] spawnedPrefab;

    //Ajustes de posicion del jugador y la camara
    [SerializeField] Transform cameraPosition;
    [SerializeField] PlayerController playerController;

    int xPos, yPos;

    public void GenerateGrid(int sides)
    {
        spawnedPrefab = new Tile[sides, sides];
        //Generamos el terreno
        for (int i = 0; i < sides; i++)
        {
            for (int j = 0; j < sides; j++)
            {
                spawnedPrefab[i, j] = Instantiate(tilesPrefab, new Vector3(i,j), Quaternion.identity);
                spawnedPrefab[i, j].name = $"Tile {i} - {j}";
            }
        }

        //Ajustamos la posicion del jugador a una posicion inicial aleatoria dentro del tablero y lo Inicializamos
        xPos = Random.Range(1, sides);
        yPos = Random.Range(1, sides);
        cameraPosition.position = new Vector3(xPos, yPos, 0);
        playerController.StartPlayer();
    }
}
