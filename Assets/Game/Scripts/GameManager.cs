using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private GameManager(){}

    private static GameManager manager;

    public static GameManager Instance
    {
        get => manager;
    }
    
    // For adding another state just add here
    public enum GameState
    {
        PREP,MID,END
    }

    // UnityEvents for GameStates, automatically filled
    public Dictionary<GameState,UnityEvent> events = new Dictionary<GameState, UnityEvent>();


    private void Awake()
    {
        manager = this;
        cam = Camera.main;
        foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
        {
            events.Add(gameState, new UnityEvent());
        }
    }

    // Register class for state listening, After registration create function with GameSate's name Ex: void PREP()
    public void AddGameStateListener(UnityEngine.Object obj)
    {
        foreach(MethodInfo inf in obj.GetType().GetMethods())
        {
            foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
            {
                if (inf.Name.Equals(gameState.ToString()))
                {
                    events[gameState].AddListener((UnityAction)Delegate.CreateDelegate(typeof(UnityAction), obj, inf.Name));
                }
            }
        }
    }

    public void RemoveGameStateListener(UnityEngine.Object obj)
    {
        foreach (MethodInfo inf in obj.GetType().GetMethods())
        {
            foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
            {
                if (inf.Name.Equals(gameState.ToString()))
                {
                    events[gameState].RemoveListener((UnityAction)Delegate.CreateDelegate(typeof(UnityAction), obj, inf.Name));
                }
            }
        }
    }

    public static Camera cam;

    private GameState currentGameState = GameState.PREP;

    public GameState CurrentGameState
    {
        get => currentGameState;
        set => currentGameState = value;
    }

    private void Update()
    {
        Debug.Log(currentGameState);
        events[currentGameState].Invoke();
    }


}
