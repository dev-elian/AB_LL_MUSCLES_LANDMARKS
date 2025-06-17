using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BodyPart : MonoBehaviour
{
    XRGrabInteractable _grabbable;
    int objectsTouching = 0;

    void Awake() {
        _grabbable = GetComponent<XRGrabInteractable>();
    }

    public void SetGrabberState(bool active) {
        objectsTouching += (active ? 1 : -1);
        _grabbable.enabled = objectsTouching > 0;
    }
}
