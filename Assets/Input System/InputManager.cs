using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (playerInputActions != null)
            playerInputActions.Dispose();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }
}
