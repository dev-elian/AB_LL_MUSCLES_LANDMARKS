using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    public Action OnMenuButtonPressed;

    public InputActions actions;

    public bool leftGrabbing = false;
    public bool rightGrabbing = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            actions = new InputActions();
            actions.Enable();
        } else {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        actions.Generals.LeftGrip.started += OnLeftGrabStarted;
        actions.Generals.LeftGrip.canceled += OnLeftGrabFinished; 
        actions.Generals.RightGrip.started += OnRightGrabStarted;
        actions.Generals.RightGrip.canceled += OnRightGrabFinished;
        actions.Generals.SecondaryButtonLeft.started += OnPressSecondaryButton;
    }

    void Start() {
        actions.Enable();
    }

    void OnDisable() {
        actions.Generals.LeftGrip.started -= OnLeftGrabStarted;
        actions.Generals.LeftGrip.canceled -= OnLeftGrabFinished;
        actions.Generals.RightGrip.started -= OnRightGrabStarted;
        actions.Generals.RightGrip.canceled -= OnRightGrabFinished;
        actions.Generals.SecondaryButtonLeft.started -= OnPressSecondaryButton;
        actions.Disable();
    }

    void OnPressSecondaryButton(InputAction.CallbackContext context) {
        OnMenuButtonPressed?.Invoke();
    }

    void OnLeftGrabStarted(InputAction.CallbackContext context) {
        leftGrabbing = true;
    }

    void OnLeftGrabFinished(InputAction.CallbackContext context) {
        leftGrabbing = false; 
    }

    void OnRightGrabStarted(InputAction.CallbackContext context) {
        rightGrabbing = true;
    }

    void OnRightGrabFinished(InputAction.CallbackContext context) {
        rightGrabbing = false;
    }
}
