using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public delegate void BoardUpdatedDelegate();
    public event BoardUpdatedDelegate BoardUpdatedEvent;

    public BoardSpaceState[] BoardSpaceStates;
    
    public static GameStateManager Singleton;
    private int _totalPenguins;

    // Start is called before the first frame update
    void Awake()
    {
        Singleton = this;
    }

    public void NewGame()
    {
        foreach (var space in BoardSpaceStates)
        {
            space.numberOfPenguins = 0;
            space.IsAntarcticBase = false;
        }
        for (int i = 0; i < 10; i++)
        {
            BoardSpaceStates[Random.Range(0, BoardSpaceStates.Length)].numberOfPenguins++;
        }
        _totalPenguins = 10;
        BoardSpaceStates[Random.Range(0, BoardSpaceStates.Length)].IsAntarcticBase = true;
        BoardUpdatedEvent?.Invoke();
    }

    public void NewTurn()
    {
        //Remove penguins that want to migrate
        foreach (var space in BoardSpaceStates)
        {
            for (int i = 0; i < space.numberOfPenguins; i++)
            {
                if (Random.Range(0f, 1f) <= 0.3f)
                {
                    space.numberOfPenguins--;
                    var neighbour = space.neighbours[Random.Range(0, space.neighbours.Length)];
                    neighbour.immigratingPenguins++;
                }
            }
        }

        //Add pending penguins that migrated from neighbouring spaces
        foreach (var space in BoardSpaceStates)
        {
            space.numberOfPenguins += space.immigratingPenguins;
            space.immigratingPenguins = 0;
        }

        BoardUpdatedEvent?.Invoke();
    }

    public string ScoutBoardSpace(int boardSpaceNumber)
    {
        string result = "";
        var boardSpace = BoardSpaceStates[boardSpaceNumber];
        if (boardSpace.numberOfPenguins > 0)
        {
            result += "Znalazłaś/eś pingwina! Weź żeton badań.\n";
            boardSpace.numberOfPenguins--;
            _totalPenguins--;
            BoardUpdatedEvent?.Invoke();
            if (_totalPenguins <= 0)
            {
                GameOver();
            }
        }
        if (boardSpace.IsAntarcticBase)
        {
            result += "Odwiedziłaś/eś stację badawczą. Odzyskaj żetony skanera.\n";
        }
        if (result.Length == 0)
        {
            result = "Nic nie znalazłaś/eś. :c";
        }

        return result;
    }

    void GameOver()
    {
        
    }
}
