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
    bool moving = false;

    [Header("Other Settings")]
    [SerializeField] CycleManager cycleManager;
    Vector2 inputs;
    PlayerConquest playerConquest;
    Tile checkTile;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("[HDD] - Exploration Phase Started");
        gamePhase = 0;      
        playerConquest = GetComponent<PlayerConquest>();
    }

    public void StartPlayer()
    {
        movePoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Primera estapa de exploracion. El jugador puede moverse libremente por el mapa y tomar el primer territorio que desee
        if (gamePhase == 0)
        {
            inputs.x = (int)Input.GetAxis("Horizontal");
            inputs.y = (int)Input.GetAxis("Vertical");

            if (moving)
            {
                transform.position = Vector2.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);

                if(Vector2.Distance(transform.position, movePoint) == 0)
                {
                    moving = false;
                }
            }

            if((inputs.x != 0 || inputs.y != 0) && !moving)
            {
                Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + inputs;
                
                if(!Physics2D.OverlapCircle(checkPoint, detectRadius, obstacles))
                {
                    moving = true;
                    movePoint += inputs;
                }
            }

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
            inputs.x = (int)Input.GetAxis("Horizontal");
            inputs.y = (int)Input.GetAxis("Vertical");

            if (moving)
            {
                transform.position = Vector2.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, movePoint) == 0)
                {
                    moving = false;
                }
            }

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
            inputs.x = (int)Input.GetAxis("Horizontal");
            inputs.y = (int)Input.GetAxis("Vertical");

            if (moving)
            {
                transform.position = Vector2.MoveTowards(transform.position, movePoint, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, movePoint) == 0)
                {
                    moving = false;
                }
            }

            if ((inputs.x != 0 || inputs.y != 0) && !moving)
            {
                Vector2 checkPoint = new Vector2(transform.position.x, transform.position.y) + offsetMovePoint + inputs;

                if (!Physics2D.OverlapCircle(checkPoint, detectRadius, obstacles))
                {
                    moving = true;
                    movePoint += inputs;
                }
            }

            if (Input.GetButtonDown("Jump") && !moving)
            {
                Debug.Log("[HDD] - Terrain Taked");
                playerConquest.TakeTerrain();
                gamePhase = 1;
                cycleManager.phase = 1;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint + offsetMovePoint, detectRadius);
    }
}
