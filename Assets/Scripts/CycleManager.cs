using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CycleManager : MonoBehaviour
{
    //Manejo de Ciclos del juego, en todas sus etapas
    [SerializeField] float explorationTime;
    [SerializeField] float conquistTime;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMPro.TMP_Text phaseText;
    [SerializeField] TMPro.TMP_Text avaiableText;

    [SerializeField] TMPro.TMP_Text tempText;

    float timer;

    public int phase;

    private void Awake()
    {
        timer = 0;
        phase = -1;
        UpdateText();
        avaiableText.text = "";
        // -1: First Exploration , 0: Explorer Mode, 1: Phase Transition, 2: Conquest Mode
    }

    private void Update()
    {
        //Fase de exploracion - Pendiente: Un metodo que permita entrar en esta fase
        if(phase == 0)
        {
            timer += Time.deltaTime;
            tempText.text = (explorationTime - timer).ToString("F2");

            if(timer > explorationTime)
            {
                phase = 1;
                playerController.gamePhase = 1;
            }
        }

        //Transicion entre una fase y otra
        if (phase == 1)
        {
            timer = 0;
            phase = 2;

            UpdateText();
        }

        //Fase de conquista, "conquistTime" el jugador puede volver a moverse con libertad por el mapa, y tomar un territorio
        if (phase == 2)
        {
            timer += Time.deltaTime;
            tempText.text = (conquistTime - timer).ToString("F2");
            avaiableText.text = "";

            if (timer >= conquistTime)
            {
                Debug.Log("[HDD] - Conquest Avaliable");
                avaiableText.text = "Conquista!";
                playerController.gamePhase = 2;
                phase = 3;
                timer = 0;
            }
        }
    }

    void UpdateText()
    {
        switch (phase)
        {
            case -1:
                phaseText.text = "Primera Exploracion";
                break;
            case 0:
                phaseText.text = "Exploracion";
                break;
            case 2:
                phaseText.text = "Toma de Territorios";
                break;
        }
    }
}
