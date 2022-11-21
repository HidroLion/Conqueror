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
        phase = -1;
        // -1: First Exploration , 0: Explorer Mode, 1: Phase Transition, 2: Conquest Mode
    }

    private void Update()
    {
        if(phase == 0)
        {
            timer += Time.deltaTime;
            if(timer > explorationTime)
            {
                phase = 1;
                playerController.gamePhase = 1;
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

            if(timer >= coquistTime)
            {
                Debug.Log("[HDD] - Conquest Avaliable");
                playerController.gamePhase = 2;
                phase = 3;
                timer = 0;
            }
        }
    }
}
