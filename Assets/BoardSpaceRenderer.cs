using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardSpaceRenderer : MonoBehaviour
{
    private int _boardSpaceNumber;
    private List<GameObject> _penguins;
//    private MeshRenderer antarcticBaseRenderer;
        
    void Start()
    {
        _penguins = GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToList();
        GameStateManager.Singleton.NewGameEvent += UpdateState;
        GameStateManager.Singleton.NewTurnEvent += UpdateState;
        UpdateState();
    }

    private void UpdateState()
    {
        BoardSpaceState boardSpaceState = GameStateManager.Singleton.BoardSpaceStates[_boardSpaceNumber];
        foreach (GameObject penguin in _penguins)
        {
            penguin.SetActive(false);
        }
        for (int i = 0; i < boardSpaceState.numberOfPenguins; i++)
        {
            _penguins[i].SetActive(true);
            _penguins[i].transform.localPosition = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f));
            _penguins[i].transform.localRotation = Quaternion.Euler(0,Random.Range(0,360),0);
        }
    }  
    
}
