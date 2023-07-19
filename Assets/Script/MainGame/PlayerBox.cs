using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBox : MonoBehaviour
{
    Image playerColor;
    Image turnimg;
    TMP_Text playerName;
    TMP_Text pointTxt;
    bool myturn;
    // Start is called before the first frame update
    void Awake()
    {
        myturn = false;
        playerColor = GetComponent<Image>();
        playerName = transform.GetChild(0).GetComponent<TMP_Text>();
        turnimg = transform.GetChild(2).GetComponent<Image>();
        pointTxt = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        
    }

    public void SetPlayerName(string name){
        playerName.text = name;
    }
    public void SetPoint(string point){
        pointTxt.text = point;
    }

    public void SetColor(Color color){
        playerColor.color = color;
    }

    public void MyTurn(){
        myturn = true;
        turnimg.gameObject.SetActive(true);
        StartCoroutine(twinkle(true));
    }

    IEnumerator twinkle(bool i){
        if(myturn){
            yield return new WaitForSeconds(0.5f);
            if(i){
                turnimg.color = Color.black;
            }
            else{
                turnimg.color = Color.white;
            }
            StartCoroutine(twinkle(!i));
        }
    }

    public void EndTurn(){
        myturn = false;
        turnimg.gameObject.SetActive(false);
    }

}
