using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    Player player;
    UIManager ui;
    TurnManager tm;
    GameSE gameSE;
    TurnSE turnSE;
    PASpecial pASpecial;
    PAEnemy pAEnemy;
    Dice dice;
    Always always;

    public void SetVariable(Player p, UIManager u, TurnManager t){
        player = p;
        ui = u;
        tm = t;
    }
    
    public void GameStart(){
        // gameSE.GameStart(player, ui, tm);
    }
    public void GameEnd(){
        gameSE.GameEnd(player, ui, tm);
    }
    public void TurnStart(){
        turnSE.TurnStart(player, ui, tm);
    }
    public void TrunEnd(){
        turnSE.TurnEnd(player, ui, tm);
    }
    public void PassSpecial(){
        pASpecial.PassSpecial(player, ui, tm);
    }
    public void ArriveSpecial(){
        pASpecial.ArriveSpecial(player, ui, tm);
    }
    public void PassEnemy(){
        pAEnemy.PassEnemy(player, ui, tm);
    }
    public void ArriveEnemy(){
        pAEnemy.ArriveEnemy(player, ui, tm);
    }
    public void Always(){
        always.Always(player, ui, tm);
    }
    public void Dice(){
        dice.Dice(player, ui, tm);
    }
}
public interface GameSE{
    public void GameStart(Player player, UIManager ui, TurnManager tm);
    public void GameEnd(Player player, UIManager ui, TurnManager tm);
}
public interface TurnSE{
    public void TurnStart(Player player, UIManager ui, TurnManager tm);
    public void TurnEnd(Player player, UIManager ui, TurnManager tm);
}
public interface PASpecial{
    public void PassSpecial(Player player, UIManager ui, TurnManager tm);
    public void ArriveSpecial(Player player, UIManager ui, TurnManager tm);
}
public interface PAEnemy{
    public void PassEnemy(Player player, UIManager ui, TurnManager tm);
    public void ArriveEnemy(Player player, UIManager ui, TurnManager tm);
}
public interface Always{
    public void Always(Player player, UIManager ui, TurnManager tm);
}
public interface Dice{
    public void Dice(Player player, UIManager ui, TurnManager tm);
}
