using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager manager;

    private void Awake()
    {
        manager = this;
        cam = Camera.main;
    }

    public static GameManager getInstance()
    {
        return manager;
    }
    
    public static Camera cam;
    public GameObject playerPivot;
    public float runSpeed = 30f;
    public MovableCharacter player;

    public enum GameState
    {
        Prepare,
        MainGame,
        FinishGame,
    }

    private GameState currentGameState;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:

                    break;
                case GameState.FinishGame:

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            currentGameState = value;
        }

    }

    

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                CurrentGameState = GameState.MainGame;
                break;
            case GameState.MainGame:
                playerPivot.transform.localPosition += Vector3.forward * runSpeed * Time.deltaTime;
                break;
            case GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }


}
