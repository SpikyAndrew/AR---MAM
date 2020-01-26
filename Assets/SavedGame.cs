using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class SavedGame
{
    public List<int> numberofPenguins;
    public int antarcticBasePosition;

    public SavedGame(BoardSpaceState[] spaces)
    {
        numberofPenguins = new List<int>();
        numberofPenguins.AddRange(spaces.Select(s=>s.numberOfPenguins));
        for(int i=0;i<spaces.Length; i++)
        {
            if (spaces[i].IsAntarcticBase)
            {
                antarcticBasePosition = i;
                break;
            }
        }
    }

    public void Save()
    {
        
    }
}
