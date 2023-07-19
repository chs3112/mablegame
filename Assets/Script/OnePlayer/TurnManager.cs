using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnManager : MonoBehaviour
{

    public GameObject instantPlayer;
    UIManager uiManager;
    TileManager tileManager;
    public Player[] players {get; private set; } = new Player[4];

    int turn;


    void Start(){
        uiManager = GetComponent<UIManager>();
        tileManager = GetComponent<TileManager>();
        turn = 1;
        DeterminePlayerSequence(4);
        GameStart();
    }


    public void GameStart(){
        foreach(Player player in players){
            player.GameStart();
        }
        Next(0);
    }

    public void GameEnd(){
        
        foreach(Player player in players){
            player.GameStart();
        }

    }

    public void Next(int n){
        if(n == 4){
            if(turn <= 10){
                players[0].turnStart();
                turn++;
            }
            else{
                GameEnd();
            }
        }
        else{
            players[n].turnStart();
        }

    }

    
    void DeterminePlayerSequence(int maxPlayer){
        int mySequence = UnityEngine.Random.Range(0, maxPlayer);
        for(int i = 0; i < maxPlayer; i++){
        
            GameObject g = Instantiate(instantPlayer, Vector3.zero, Quaternion.identity);
            Player player;
            if (i == mySequence) {player= g.AddComponent<Human>();}
            else {player= g.AddComponent<Ai>();}
            players[i] = player;
            player.SetVariable(uiManager, this, tileManager, i, new Skill());
        }
    }

    public void Rank(){
        foreach(Player p in players){
            int i = 1;
            foreach(Player e in players){
                if(p.point < e.point){
                    i++;
                }
            }
            p.myranking = i;
        }
    }

    public Player GetPlayer(int i){
        return players[i];
    }


}

