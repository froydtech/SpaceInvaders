using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    //graffiti
    public Text uiText;

    //total score
    public int enemiesShot = 0;

    int enemies = 0;
        enum State { NotStarted, Playing, GameOverGround, GameOverPlayer }


    State currState;
    EnemyManager enemyManager;

    void Start()
    {
        currState = State.NotStarted;

        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        RefreshUI();
    }


    void RefreshUI ()
    {
        //act according to the state
        switch(currState)
        {
            case State.NotStarted:
                uiText.text = "Shoot here to begin";
                break;
            case State.Playing:
                EnemyManager gm2 = GameObject.FindObjectOfType<EnemyManager>();
                enemies = gm2.ReturnEnemies();
                uiText.text = "Enemies Left in Wave: " + enemies;
                break;            
        }
    }

    public void Initgame()
    {
        if (currState == State.Playing) return;

        //set state to playing on startgame.
        currState = State.Playing;

        //creates the enemy wave
        enemyManager.StartNewWave();

        enemiesShot = 0;

        //refreshes the text board on the building
        RefreshUI();
    }

    public void GameOverGround()
    {
        if ((currState == State.GameOverGround) || (currState == State.GameOverPlayer))
            return;
        currState = State.GameOverGround;
        uiText.text = "Game over, they've landed!  Shoot to play again. Score: " + enemiesShot;
    }

    public void GameOverPlayer()
    {
        if ((currState == State.GameOverGround) || (currState == State.GameOverPlayer))
            return;
        currState = State.GameOverPlayer;
        uiText.text = "Game over, they've hit you!  Shoot to play again. Score: " + enemiesShot;
    }
  
    public void Updatescore()
    {
        if ((currState == State.GameOverGround) || (currState == State.GameOverPlayer))
            return;
        enemiesShot++;
        RefreshUI();
    }
}
