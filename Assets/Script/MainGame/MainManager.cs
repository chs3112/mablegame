using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainManager : MonoBehaviourPun
{
    Player[] players = new Player[4];
    int[,] pl = new int[24,4] { {1,2,3,0}, {1,2,0,3}, {1,3,2,0}, {1,3,2,0}, 
                                {1,0,2,3}, {1,0,3,2}, {2,1,3,0}, {2,1,0,3},
                                {2,3,1,0}, {2,3,0,1}, {2,0,1,3}, {2,0,3,1},
                                {3,1,2,0}, {3,1,0,2}, {3,2,1,0}, {3,2,0,1},
                                {3,0,1,2}, {3,0,2,1}, {0,1,2,3}, {0,1,3,2},
                                {0,2,1,3}, {0,2,3,1}, {0,3,1,2}, {0,3,2,1}
                                };


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    
    private void Awake()
    {
        if(PhotonNetwork.LocalPlayer.IsMasterClient){
            int a = PhotonNetwork.LocalPlayer.ActorNumber;
            photonView.RPC("SetNum", RpcTarget.MasterClient, players);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [PunRPC]
    void SetNum(){

    }

    [PunRPC]
    void instate(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
