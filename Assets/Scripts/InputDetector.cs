using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// detects player's click, touch input
/// </summary>
public class InputDetector : MonoBehaviour
{
    private InputAction clickAction;

    private bool isup;
    public bool IsUp { get => isup; }

    private bool isDown;
    public bool IsDown { get => isDown; }

    private void Awake()
    {
        GameManager.Instance.ResisterInputDetector(this);
        Init();
    }

    private void Init()
    {
        clickAction = new InputAction(type: InputActionType.Button);

        clickAction.AddBinding("<Mouse>/leftButton");
        clickAction.AddBinding("<Touchscreen>/press");

        clickAction.started += ctx => Down();
        clickAction.canceled += ctx => Up();

        clickAction.Enable();
    }

    private void Up()
    {
        isup = true;
        isDown = false;
    }

    private void Down()
    {
        isup = false;
        isDown = true;
    }
}
