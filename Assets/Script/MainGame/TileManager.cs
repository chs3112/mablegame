using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public TileData[] tiles = new TileData[40];
    public DiePlace diePlace;

    public TileData GetTile(int index){
        return tiles[index];
    }

    void Awake(){
        for(int i=0; i<40; i++){
            tiles[i].SetVariable(i);
        }
    }
}
