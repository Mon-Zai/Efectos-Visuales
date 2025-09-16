using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerStates
{
    DEFAULT,
    SPRINTING,
    CROUCHING,
    SLIDING,
    WALLRUNNING
}

public class PlayerStateHandler : MonoBehaviour
{
    PlayerStates currentState;

    void Awake()
    {
        currentState = PlayerStates.DEFAULT;
    }


    public void SetState(PlayerStates playerStates)
    {
        currentState = playerStates;
    }
    public PlayerStates GetState()
    {
        return currentState;
    }
}