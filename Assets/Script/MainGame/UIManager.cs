using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text dict;
    public TMP_Text start_Text;
    public Button roll_Button;
    public PlayerBox[] playerBoxes = new PlayerBox[4];


    void Start()
    {
        
    }

    public void TurnStart_TextUP(string msg){
        start_Text.text = msg;
        start_Text.gameObject.SetActive(true);
        StartCoroutine(sleep(() => start_Text.gameObject.SetActive(false), 1));
    }

    public void SetActive_Roll_Button(bool i){
        roll_Button.gameObject.SetActive(i);
    }

    public void Action_Roll_Button(UnityAction button_Action){
        roll_Button.onClick.RemoveAllListeners();
        roll_Button.onClick.AddListener(button_Action);
    }

    public void Dice_SetActive(bool isSet){
        dict.gameObject.SetActive(isSet);
    }

    public void Roll_Dice(int i, int ndice){
        Dice_SetActive(true);
        int n = UnityEngine.Random.Range(1, 7);
        dict.text = n.ToString();
        if (i < 20){
            StartCoroutine(sleep(()=>Roll_Dice(i+1, ndice), 0.05f));
        }
        else{
            dict.text = ndice.ToString();
        }
    }

    
    protected IEnumerator sleep(Action action, float n){
        yield return new WaitForSeconds(n);
        action();
    }







    // Update is called once per frame
    void Update()
    {
        
    }
}
