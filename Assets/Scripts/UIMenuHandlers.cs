using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class UIMenuHandlers : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject arMenu;
    [SerializeField] private GameObject selectBoardSpaceMenu;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Text infoText;

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
        infoText.text = "";
    }
    
    public void OnScoutButton()
    {
        mainMenu.SetActive(false);
        selectBoardSpaceMenu.SetActive(true);
    }

    public void OnNewGameButton()
    {
        GameStateManager.Singleton.NewGame();
        infoText.text = "Rozpocznijmy grę!";
    }

    public void OnNewTurnButton()
    {
        GameStateManager.Singleton.NewTurn();
        infoText.text = "Nowa tura";
    }

    public void OnBoardSpaceButton(int boardSpaceNumber)
    {
        mainMenu.SetActive(true);
        selectBoardSpaceMenu.SetActive(false);
        infoText.text = GameStateManager.Singleton.ScoutBoardSpace(boardSpaceNumber);
    }    
    
}
