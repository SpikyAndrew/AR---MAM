using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class UIMenuHandlers : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject arMenu;
    [SerializeField] private GameObject selectBoardSpaceMenu;
    [SerializeField] private GameStateManager gameStateManager;

    public void OnShowPenguinsButton()
    {
        arCamera.enabled = true;
        mainMenu.SetActive(false);
        arMenu.SetActive(true);
    }
    
    public void OnReturnButton()
    {
        arCamera.enabled = false;
        mainMenu.SetActive(true);
        arMenu.SetActive(false);
        selectBoardSpaceMenu.SetActive(false);
    }
    
    public void OnScoutButton()
    {
        mainMenu.SetActive(false);
        selectBoardSpaceMenu.SetActive(true);
    }

    public void OnNewGameButton()
    {
        GameStateManager.Singleton.NewGame();
    }

    public void OnNewTurnButton()
    {
        GameStateManager.Singleton.NewTurn();
    }

    public void OnBoardSpaceButton(int boardSpaceNumber)
    {
        GameStateManager.Singleton.ScoutBoardSpace(boardSpaceNumber);
    }
    
}
