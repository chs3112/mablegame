using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePlace : MonoBehaviour
{
    List<Player> players = new List<Player>();
    
    public void AddDie(Player player){
        players.Add(player);
        DieMove(player, players.Count);
    }

    public void RemoveDie(Player player){
        players.Remove(player);
        for(int i = 0; i < players.Count; i++){
            DieMove(players[i], i+1);
        }
    }

    void DieMove(Player player, int num){
        switch(num){
            case 1:
                player.transform.position = new Vector3(-7f, 0.5f, 0);
                break;
            case 2:
                player.transform.position = new Vector3(-6f, 0.5f, 0);
                break;
            case 3:
                player.transform.position = new Vector3(-7f, -0.5f, 0);
                break;
            case 4:
                player.transform.position = new Vector3(-6f, -0.5f, 0);
                break;
        }
    }
}
