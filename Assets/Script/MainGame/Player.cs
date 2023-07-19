using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class Player : MonoBehaviour
{

    protected UIManager uiManager;
    TurnManager turnManager;
    TileManager tileManager;
    int location = 0;
    public int sequence {get; private set;}
    public int point {get; private set;}
    public bool isRestart;
    public bool isdie;
    TileData nowTile;
    public PlayerBox playerBox;
    SpriteRenderer turnimg;
    SpriteRenderer myimg;
    bool myturn;
    public int myranking;
    Stack<int> pointlist = new Stack<int>();
    Skill skill;
    DiePlace diePlace;
    #region Skills
    TurnStartEnd turnse;
    GameStartEnd gamese;

        
    #endregion




    public void SetVariable(UIManager uiManager, TurnManager turnManager, TileManager tileManager, int sequence, Skill skill){
        myturn = false;
        #region managers
        this.uiManager = uiManager;
        this.sequence = sequence;
        this.turnManager = turnManager;
        this.tileManager = tileManager;
        #endregion
        this.skill = skill;
        skill.SetVariable(this, uiManager, turnManager);
        Color playerColor = new Color(UnityEngine.Random.Range(0,1f),UnityEngine.Random.Range(0, 1f),UnityEngine.Random.Range(0,1f));
        GetComponent<SpriteRenderer>().color = playerColor;
        turnimg = transform.GetChild(0).GetComponent<SpriteRenderer>();
        myimg = GetComponent<SpriteRenderer>();
        #region playerBox
        playerBox = uiManager.playerBoxes[sequence];
        playerBox.SetColor(playerColor);
        playerBox.SetPlayerName("플레이어"+(sequence+1).ToString());
        playerBox.SetPoint("0");
        #endregion
        this.nowTile = tileManager.GetTile(location);
        isRestart = false;
        this.diePlace = tileManager.diePlace;
        Teleporte(sequence*10);
    }

    private void AddLocation(int i){
        location += i;
        if(location >= 40){
            location-=40;
        }

    }

    private void SetLocation(int i){
        location = i;
    }

    public void GameStart(){
        
    }

    public void UpSort(){
        myimg.sortingOrder = 2;
    }
    public void DownSort(){
        myimg.sortingOrder = 1;
    }

    //턴시작
    public void turnStart(){
        turnse.TurnStart();
    }

    
    public void MyTurn(){
        playerBox.MyTurn();
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
        playerBox.EndTurn();
        myturn = false;
        turnimg.gameObject.SetActive(false);
    }


    public abstract void ButtonUp();

    //2
    protected void Roll_Dice(){
        uiManager.SetActive_Roll_Button(false);
        int ndice = RandomDice();
        if(ndice == 6){
            isRestart = true;
        }
        uiManager.Roll_Dice(0, ndice);
        StartCoroutine(sleep(1f, ()=>Moves(ndice)));
    }

    int RandomDice(){
        return UnityEngine.Random.Range(1,7);
    }

    //3
    void Moves(int n_iteration){
        nowTile.SetPlayer(null);
        nowTile = null;
        StartCoroutine(sleep(0.3f, ()=>forMove(n_iteration, 1)));
    }

    void forMove(int n, int i){
        PrePass();
        Move();
        PostPass();
        if (i < n){
            StartCoroutine(sleep(0.3f, ()=>forMove(n, i+1)));
        }
        else{
            uiManager.Dice_SetActive(false);
            Arrival();
        }
        
    }

    //3-1
    void PrePass(){

    }

    //3-2
    public virtual void Move(){
        Dictionary<int, Vector3> direction = new Dictionary<int, Vector3>(){
            {0, new Vector3(-0.7f, 0,0)},
            {1, new Vector3(0, 0.7f,0)},
            {2, new Vector3(0.7f, 0,0)},
            {3, new Vector3(0, -0.7f,0)},
        };
        int line = (int)System.Math.Truncate((double)location/10);
        PositionUpdate(direction[line]);
        AddLocation(1);
        
    }

    void PositionUpdate(Vector3 add){
        transform.position+= add;

    }

    //3-3
    void PostPass(){
        if(tileManager.GetTile(location).IsSpecial()){
            AddPoint(1);
        }
    }
    

    //4
    public void Arrival(){
        nowTile = tileManager.GetTile(location);
        if(nowTile.IsExistPlayer()){
            Attack(nowTile.GetPlayer());
        }
        nowTile.SetPlayer(this);
        if(nowTile.IsSpecial()){
            isRestart = true;
        }
        TrunEnd();
    }

    //5
    public void TrunEnd(){
        turnse.TurnEnd();
    }

    public void Next(){
        turnManager.Next(sequence+1);
    }


    //0
    public void Restart(){
        isRestart = false;
        uiManager.TurnStart_TextUP("한 번 더!");
        uiManager.SetActive_Roll_Button(true);
        uiManager.Action_Roll_Button(Roll_Dice);
    }


    //6
    public void GameEnd(){

    }

    public void Attack(Player diePlayer){
        AddPoint(1);
        isRestart = true;
        diePlayer.Die();
    }


    public void Die(){
        AddPoint(-1);
        diePlace.AddDie(this);
        isdie = true;
        nowTile.SetPlayer(null);
    }

    public void Revive(){
        uiManager.TurnStart_TextUP("부활");
        isRestart = false;
        isdie = false;
        Teleporte(sequence*10);
        StartCoroutine(sleep(1, ()=>TrunEnd()));
    }

    public void Teleporte(int toLocation){
        nowTile.SetPlayer(null);
        SetLocation(toLocation);
        nowTile = tileManager.GetTile(location);
        transform.position = nowTile.GetPosition();
        if(nowTile.IsExistPlayer()){
            Attack(nowTile.GetPlayer());
        }
        nowTile.SetPlayer(this);
    }

    

    public void AddPoint(int point){
        this.point += point;
        pointlist.Push(point);
        turnManager.Rank();
        playerBox.SetPoint(this.point.ToString());
    }

    public void CancelPoint(){
        this.point -= pointlist.Pop();
        turnManager.Rank();
        playerBox.SetPoint(this.point.ToString());
    }

    protected IEnumerator sleep(float n, Action action){
        yield return new WaitForSeconds(n);
        action();
    }



}

