using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardSpaceRenderer : MonoBehaviour
{
    [SerializeField] private int boardSpaceNumber;
    [SerializeField] private GameObject antarcticBase;
    private Penguin[] _penguins;
        
    void Start()
    {
        _penguins = GetComponentsInChildren<Penguin>();
        GameStateManager.Singleton.BoardUpdatedEvent += UpdateState;
        UpdateState();
    }

    private void UpdateState()
    {
        BoardSpaceState boardSpaceState = GameStateManager.Singleton.BoardSpaceStates[boardSpaceNumber];
        foreach (Penguin penguin in _penguins)
        {
            penguin.gameObject.SetActive(false);
        }
        for (int i = 0; i < boardSpaceState.numberOfPenguins; i++)
        {
            _penguins[i].gameObject.SetActive(true);
            _penguins[i].transform.localPosition = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
            _penguins[i].transform.localRotation = Quaternion.Euler(0,Random.Range(0,360),0);
        }
        antarcticBase.SetActive(boardSpaceState.IsAntarcticBase);
    }  
    
}
