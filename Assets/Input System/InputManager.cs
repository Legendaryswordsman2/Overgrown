using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //public static InputManager instance { get; private set; }

    public static PlayerInputActions playerInputActions;

    private void Awake()
    {
        //instance = this;

        if (playerInputActions != null)
            playerInputActions.Player.Disable();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
}
