using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Player
{
    public override void ButtonUp(){
        uiManager.TurnStart_TextUP("내 턴");
        uiManager.Action_Roll_Button(Roll_Dice);
        uiManager.SetActive_Roll_Button(true);
    }
}
