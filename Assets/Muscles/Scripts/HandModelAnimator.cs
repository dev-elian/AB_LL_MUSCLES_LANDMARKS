using UnityEngine;
using UnityEngine.InputSystem;

public class HandModelAnimator : MonoBehaviour {
    [SerializeField] private InputActionProperty triggerAction;
    [SerializeField] private InputActionProperty gripAction;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
    }
}
