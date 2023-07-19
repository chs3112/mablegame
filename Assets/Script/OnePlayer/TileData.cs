using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{

    int tile_sequence;
    string state;
    Player player;   
    public bool isSpecialTile;
    Vector2 position;

    
    public void SetVariable(int tile_sequence){
        this.tile_sequence = tile_sequence;
        isSpecialTile = tile_sequence%5 == 0 ? true : false;
        player = null;
        position = new Vector2(transform.position.x, transform.position.y);
    }


    public Vector2 GetPosition(){
        return position;
    }

    public void SetPlayer(Player player){
        this.player = player;
    }

    
    public Player GetPlayer(){
        return player;
    }

    public bool IsExistPlayer(){
        return player == null ? false : true;
    }
    
    public bool IsSpecial(){
        return isSpecialTile;
    }
}
