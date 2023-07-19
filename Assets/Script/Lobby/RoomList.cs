using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomList : MonoBehaviour
{
    public NetworkManager netM;
    public TMP_Text roomName;
    
    public void SetName(string rName, int maxP, int currentP){
        roomName.text = rName+"("+currentP.ToString()+"/"+maxP.ToString()+")";
    }

    public void Onjoin(){
        
        netM.JoinRoom(roomName.text);
    }
}
