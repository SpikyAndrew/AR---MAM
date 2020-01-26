using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using SimpleFileBrowser;

public class GameStateManager : MonoBehaviour
{
    public delegate void BoardUpdatedDelegate();
    public event BoardUpdatedDelegate BoardUpdatedEvent;
    public delegate void GameOverDelegate();
    public event GameOverDelegate GameOverEvent;

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
            result = "Nic nie znalazłaś/eś. :c\n";
        }

        return result;
    }

    public void SaveGame()
    {
        SimpleFileBrowser.FileBrowser.ShowSaveDialog( SaveGameToPath, null, false, Application.persistentDataPath, "Save", "Save" );
    }
    
    public void LoadGame()
    {
        SimpleFileBrowser.FileBrowser.ShowLoadDialog( LoadGameFromPath, null, false, Application.persistentDataPath, "Load", "Load" );
    }

    private void SaveGameToPath(string path)
    {
            var savedGame = new SavedGame(BoardSpaceStates);
            var json = JsonUtility.ToJson(savedGame);
            File.WriteAllText(path, json);
            BoardUpdatedEvent?.Invoke();
            return;
    }

    private void LoadGameFromPath(string path)
    {
        if (!File.Exists(path))
        {
            return;
        }
        else
        {
            var savedJson = File.ReadAllText(path);
            var savedGame = JsonUtility.FromJson<SavedGame>(savedJson);
            
            for (int i = 0; i < savedGame.numberofPenguins.Count; i++)
            {
                BoardSpaceStates[i].numberOfPenguins = savedGame.numberofPenguins[i];
                if (savedGame.antarcticBasePosition == i)
                    BoardSpaceStates[i].IsAntarcticBase = true;
                else BoardSpaceStates[i].IsAntarcticBase = false;
            }
            BoardUpdatedEvent?.Invoke();
        }
    }
    
    void GameOver()
    {
        GameOverEvent?.Invoke();
    }
    
    
}
