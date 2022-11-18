using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    [SerializeField] float explorationTime;
    [SerializeField] float coquistTime;
    [SerializeField] PlayerController playerController;
    float timer;

    public int phase;

    private void Awake()
    {
        timer = 0;
        phase = 0;
    }

    private void Update()
    {
        if(phase == 0)
        {
            timer += Time.deltaTime;
            if(timer > explorationTime)
            {
                phase = 1;
                playerController.explorer = false;
            }
        }
        if(phase == 1)
        {
            timer = 0;
            phase = 2;
        }
        if(phase == 2)
        {
            timer += Time.deltaTime;
        }
    }
}
