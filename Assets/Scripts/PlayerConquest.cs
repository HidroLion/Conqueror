using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConquest : MonoBehaviour
{
    //Este codigo se encarga de la toma de territorios

    [SerializeField] Color teamColor; //Color del equipo
    [SerializeField] public string playerName; //Nombre del jugador
    [SerializeField] public GridGenerator grids; //Referencia al arreglo de "Tiles" del juego

    public void TakeTerrain()
    {
        //Transformamos los valores de posicion del jugador en numero enteros...
        //El arreglo de Tiles esta hecho de tal forma que la posicion en el mundo Coincide con la posicion en el arreglo
        int x = (int)transform.position.x; 
        int y = (int)transform.position.y;

        if (grids.spawnedPrefab[x,y].teamName != playerName) //Verficamos que el Tile que el jugador quiere tomar no halla sido tomada aun por el.
        {
            //Accedemos al metodo ChangeColor del Tile correspondiente.
            grids.spawnedPrefab[(int)transform.position.x, (int)transform.position.y].ChangeColor(playerName, teamColor);
            Debug.Log($"[HDD] - You Take the Terrotory {x} , {y}");
        }
    }
}
