using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Este Script lo poseeen todas las Tiles del juego
    public string teamName = " ";
    SpriteRenderer spriteRenderer;
    public bool wall = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Cambia el color del Tile y le asigna el nombre del jugador que la tomo
    public void ChangeColor(string newOwner, Color teamColor)
    {
        teamName = newOwner;
        spriteRenderer.color = teamColor;
    }

}
