using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class WaitingRoomManager : MonoBehaviourPun
{

    public TMP_Text playerCount;

    public Button B_Start;

    private void Awake()
    {
        
    }

    private void Start()
    {
        
        Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
        playerCount.text = (PhotonNetwork.CurrentRoom.PlayerCount+(int)cp["ai"]).ToString();

        B_Start.interactable = false;
    }

    public void GoStart(){
        PhotonNetwork.LoadLevel("MainGame");
    }
    
    public void PlusAI(){
        if(PhotonNetwork.CurrentRoom.MaxPlayers > 1){
            PhotonNetwork.CurrentRoom.MaxPlayers -= 1;
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            cp["ai"] = 1+(int)cp["ai"];
            playerCount.text = (PhotonNetwork.CurrentRoom.PlayerCount+(int)cp["ai"]).ToString();
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers){
            B_Start.interactable = true;
        }
        else{
            B_Start.interactable = false;
        }
    }
}
