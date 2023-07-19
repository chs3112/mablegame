using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartEnd : Skills
{
    
    public virtual void TurnStart(){
        player.UpSort();
        player.isRestart = false;
        player.MyTurn();
        if(player.isdie){
            player.Revive();
        }
        else{
            player.ButtonUp();
        }
    }

    public virtual void TurnEnd(){
        if(player.isRestart){
            player.Restart();
        }
        else{
            player.EndTurn();
            player.DownSort();
            player.Next();
        }
    }
}
