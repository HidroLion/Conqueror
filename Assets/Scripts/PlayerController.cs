using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool explorer;

    [Header("Player Stats")]
    [SerializeField] float speed;
    [SerializeField] Color teamColor;
    [SerializeField] Color wallTeamColor;
    [SerializeField] public string playerName;
    Vector2 movePoint;

    [Header("Player Movement")]
    [SerializeField] Vector2 offsetMovePoint;
    [SerializeField] LayerMask obstacles;
    [SerializeField] float detectRadius;
    bool moving = false;

    [Header("Other Settings")]
    [SerializeField] CycleManager cycleManager;
    [SerializeField] GridGenerator grids;
    Vector2 inputs;
    PlayerConquest playerConquest;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("[HDD] - Exploration Phase Started");
        explorer = true;      
        playerConquest = GetComponent<PlayerConquest>();
    }

    public void StartPlayer()
    {
        movePoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (explorer)
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
                explorer = false;
                Debug.Log("[HDD] - Exploration Phase Ended");
            }
        }

        if (!explorer)
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
                cycleManager.phase = 1;

                int x = (int)transform.position.x;
                int y = (int)transform.position.y;

                grids.spawnedPrefab[(int)transform.position.x, (int)transform.position.y].ChangeColor(playerName, teamColor);
                Debug.Log($"[HDD] - You Take the Terrotory {x} , {y}");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint + offsetMovePoint, detectRadius);
    }
}
