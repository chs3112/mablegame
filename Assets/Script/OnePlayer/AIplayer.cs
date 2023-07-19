using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIplayer : MonoBehaviour
{

    public Player player;

    public IEnumerator AiRollDice(){
        yield return new WaitForSeconds(2);
    }

}
