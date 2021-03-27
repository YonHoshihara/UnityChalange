﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public bool inbattle;
    public float battleMinDistance;
    [SerializeField] private GameController gameController;
    [SerializeField] private RoundController RoundController;
    private List<int> player1Dices, player2Dices;
    private int currentBattleVictory;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        inbattle = false;
        List<int> test_list = new List<int>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        distance = (gameController.player1.transform.position - gameController.player2.transform.position).magnitude;
        if (distance <= battleMinDistance)
        {
            inbattle = true;
        }
        else
        {
            inbattle = false;
        }
    }

    public void Battle()
    {
        StartCoroutine(StartBattle());
    }
    IEnumerator StartBattle()
    {
        player1Dices = new List<int>();
        player2Dices = new List<int>();
        player1Dices = GeneratePlayerRolls(gameController.player1.GetComponent<PlayerController>());
        player2Dices = GeneratePlayerRolls(gameController.player2.GetComponent<PlayerController>());
        currentBattleVictory = CompareDices(player1Dices, player2Dices);
        Debug.Log(currentBattleVictory);
        yield return new WaitForSeconds(.2f);
    }

    private int CompareDices(List<int> player1Dices, List<int> player2Dices)
    {
  
        int player1VictoryCounter = 0; 
        int player2VictoryCounter = 0;
        int minDicesValue = Mathf.Min(player1Dices.Count,player2Dices.Count);
        
        for (int i = 0; i < minDicesValue; i++)
        {
            if (player1Dices[i] == player2Dices[i])
            {
                if (RoundController.GetPlayerOnTurn() == 1)
                {
                    player1VictoryCounter++;
                }
                else
                {
                    player2VictoryCounter++;
                }
            }

            if (player1Dices[i] > player2Dices[i])
            {
                player1VictoryCounter++;
            }
            else
            {
                player2VictoryCounter++;
            }
        }

        if (player1VictoryCounter > player2VictoryCounter)
        {
            return 1;
        }
        else
        {
            return 2;
        }
        
    }
    private List<int> GeneratePlayerRolls(PlayerController player)
    {
        List<int> dicesRolls = new List<int>();
        for (int i = 0; i<player.dicesNumber;i++)
        {
            dicesRolls.Add(RollDice());
        }
        dicesRolls.Sort();
        dicesRolls.Reverse();
        return dicesRolls;
    }
    private int RollDice()
    {
        int dice_number = Random.Range(1,7);
        return dice_number;
    }

}
