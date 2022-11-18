using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string teamName = " ";
    SpriteRenderer spriteRenderer;
    public bool wall = false;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(string newOwner, Color teamColor)
    {
        teamName = newOwner;
        spriteRenderer.color = teamColor;
    }

}
