using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int gamePhase;

    [Header("Player Stats")]
    [SerializeField] float speed;
    [SerializeField] Color wallTeamColor;
    Vector2 movePoint;

    [Header("Player Movement")]
    [SerializeField] Vector2 offsetMovePoint;
    [SerializeField] LayerMask obstacles;
    [SerializeField] float detectRadius;
    [SerializeField] float movementDelay = 0.3f;
    bool moving = false;

    [Header("Other Settings")]
    [SerializeField] CycleManager cycleManager;
    Vector2 inputs;
    PlayerConquest playerConquest;
    Tile checkTile;

    void Awake()
    {
        Debug.Log("[HDD] - Exploration Phase Started");
        gamePhase = 0;      
        playerConquest = GetComponent<PlayerConquest>();
        gameObject.SetActive(false);
    }

    //inicializamos el jugador, asignando sus valores de movimiento al valor de posicion donde Spawneo.
    public void StartPlayer()
    {
        movePoint = transform.position;
        gameObject.SetActive(true);
    }

    void Update()
    {
        //Primera estapa de exploracion. El jugador puede moverse libremente por el mapa y tomar el primer territorio que desee
        if (gamePhase == 0)
        {
            ChechInput();

            //Logica disponible para usar los muros 
            if ((inputs.x != 0 || inputs.y != 0) && !moving)
            {
                Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + inputs;
                
                if(!Physics2D.OverlapCircle(checkPoint, detectRadius, obstacles))
                {
                    moving = true;
                    movePoint += inputs;
                }
            }

            //Una vez el jugador escoge un punto inicial, se comienza el juego principal.
            if(Input.GetButtonDown("Jump") && !moving)
            {
                Debug.Log("[HDD] - Exploration Phase Ended");
                playerConquest.TakeTerrain();
                gamePhase = 1;
                cycleManager.phase = 1;
            }
        }

        //Segunda estapa de planeacion. El jugador puede moverse solo por su ciudad.
        if (gamePhase == 1)
        {
            ChechInput();

            //Verifica que el terreno al que se quiere mover, es un terreno del jugador.
            if ((inputs.x != 0 || inputs.y != 0) && !moving)
            {
                Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + inputs;

                if (playerConquest.grids.spawnedPrefab[(int)checkPoint.x, (int)checkPoint.y].teamName ==
                    playerConquest.playerName)
                {
                    moving = true;
                    movePoint += inputs;
                }
            }
        }

        //El jugador puede tomar un terrotorio cada cierto tiempo
        if(gamePhase == 2)
        {
            ChechInput();

            //Verifica que el terreno al que se quiere mover no ha sido tomado, si es asi, detiene el movimiento en ese punto
            if ((inputs.x != 0 || inputs.y != 0) && !moving)
            {
                Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + inputs;

                if (playerConquest.grids.spawnedPrefab[(int)checkPoint.x, (int)checkPoint.y].teamName ==
                    playerConquest.playerName)
                {
                    moving = true;
                    movePoint += inputs;
                }
                else if (playerConquest.grids.spawnedPrefab[(int)checkPoint.x, (int)checkPoint.y].teamName ==
                    " ")
                {
                    moving = true;
                    movePoint += inputs;
                    gamePhase = 3;
                }
            }
        }

        if (gamePhase == 3)
        {
            ChechInput();

            //Toma de territorios cada X tiempo
            if (Input.GetButtonDown("Jump") && !moving)
            {
                Debug.Log("[HDD] - Terrain Taked");
                playerConquest.TakeTerrain();
                gamePhase = 1;
                cycleManager.phase = 1;
            }
        }
    }

    void ChechInput()
    {
        inputs.x = Input.GetAxisRaw("Horizontal");
        inputs.y = Input.GetAxisRaw("Vertical");


        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, movePoint) == 0)
            {
                moving = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint + offsetMovePoint, detectRadius);
    }
}
