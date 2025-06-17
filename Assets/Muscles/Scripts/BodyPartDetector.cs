using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BodyPartDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("BodyPart")) {
            other.GetComponent<BodyPart>().SetGrabberState(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("BodyPart")) {
            other.GetComponent<BodyPart>().SetGrabberState(false);
        }
    }
}
