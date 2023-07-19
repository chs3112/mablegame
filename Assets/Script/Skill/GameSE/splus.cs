using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splus : GameSE
{
    public void GameEnd(Player player, UIManager ui, TurnManager tm)
    {
        
    }

    public void GameStart(Player player, UIManager ui, TurnManager tm)
    {
        player.AddPoint(5);
    }
}
public class aaa : GameSE
{
    public void GameEnd(Player player, UIManager ui, TurnManager tm)
    {
        
    }

    public void GameStart(Player player, UIManager ui, TurnManager tm)
    {
        int i = Random.Range(0, 3);
        if(i >= player.sequence){
            i++;
        }
        tm.GetPlayer(i).AddPoint(-7);
    }
}
public class bbb : GameSE
{
    public void GameEnd(Player player, UIManager ui, TurnManager tm)
    {
        
    }

    public void GameStart(Player player, UIManager ui, TurnManager tm)
    {
        player.AddPoint(5);
    }
}
