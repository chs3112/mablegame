using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_Text loading;
    public GameObject loadingBackground;
    public TMP_InputField nickName;
    public string gameVersion = "1.0";
    public List<RoomList> rooms = new List<RoomList>();
    public RoomList roomUi;
    public RectTransform roomTransform;

    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        loadingBackground.SetActive(true);
        StartCoroutine(waitloading());
    }

    IEnumerator waitloading(){
        while(true){
            loading.text = "로딩중.";
            yield return new WaitForSeconds(0.2f);
            loading.text = "로딩중..";
            yield return new WaitForSeconds(0.2f);
            loading.text = "로딩중...";
            yield return new WaitForSeconds(0.2f);
        }


    }


    public void Connect(){
        if (PhotonNetwork.IsConnected){
            PhotonNetwork.JoinRandomRoom();
        }
        else{

        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        
        
    }

    public override void OnJoinedLobby()
    {
        StopCoroutine(waitloading());
        loadingBackground.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("WaitingRoom");
    }

    public void JoinRandomRoom(){
        PhotonNetwork.NickName = nickName.text;
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = nickName.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.CustomRoomProperties = new Hashtable(){{"ai", 0}};
        roomOptions.CustomRoomPropertiesForLobby = new string[] {"ai"};

        PhotonNetwork.CreateRoom("새로운 방", roomOptions, null);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RoomUpdate(roomList);
    }

    public void JoinRoom(string rName){
        PhotonNetwork.NickName = nickName.text;
        PhotonNetwork.JoinRoom(rName);
    }

    public void RoomUpdate(List<RoomInfo> roomList){
        for (int i = 0; i < rooms.Count; i++){
            Destroy(rooms[i].gameObject);
        }
        rooms.Clear();
        roomTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5+35*roomList.Count);

        for (int i = 0; i < roomList.Count; i++){
            if (roomList[i].PlayerCount == 0){ continue; }

            RoomList newRoomUi = Instantiate(roomUi);
            newRoomUi.netM = this;
            newRoomUi.SetName(roomList[i].Name, roomList[i].MaxPlayers, roomList[i].PlayerCount+(int)roomList[i].CustomProperties["ai"]);
            newRoomUi.transform.SetParent(roomTransform);

            rooms.Add(newRoomUi);
        }
    }
}
