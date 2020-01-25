using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpaceState : MonoBehaviour
{
    public BoardSpaceState[] neighbours;
    [HideInInspector] public int numberOfPenguins = 0;
    [HideInInspector] public bool IsAntarcticBase = false;
}
