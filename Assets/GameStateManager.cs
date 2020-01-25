using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public delegate void NewTurnDelegate();
    public event NewTurnDelegate NewTurnEvent;
    
    public delegate void NewGameDelegate();
    public event NewGameDelegate NewGameEvent;

    public BoardSpaceState[] BoardSpaceStates;
    
    public static GameStateManager Singleton;
    
    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
    }

    public void NewGame()
    {
        for (int i = 0; i < 10; i++)
        {
            BoardSpaceStates[Random.Range(0, BoardSpaceStates.Length)].numberOfPenguins++;
        }
        BoardSpaceStates[Random.Range(0, BoardSpaceStates.Length)].IsAntarcticBase = true;
        NewGameEvent?.Invoke();
    }

    public void NewTurn()
    {
           
        NewTurnEvent?.Invoke();
    }
}
