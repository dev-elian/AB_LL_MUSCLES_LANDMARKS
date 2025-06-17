using UnityEngine;

public class MuscleRaycast : MonoBehaviour {
    public bool isLeft = true;
    public float laserDistance = 10f;
    public Transform laserOrigin;

    public string interactableTag = "Muscles";
    public Material highlightMaterial;

    GameObject _lastObject;

    void Update() {
        if (isLeft) {
            if (InputController.Instance.leftGrabbing) {
                ClearHighlight();
                return;
            }
        } else {
            if (InputController.Instance.rightGrabbing) {
                ClearHighlight();
                return;
            }
        }

        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out RaycastHit hit, laserDistance)) {
            if (hit.collider.CompareTag(interactableTag)) {
                if (hit.collider.gameObject != _lastObject) {
                    ClearHighlight();
                    _lastObject = hit.collider.gameObject;
                    MuscleHighlightManager.Highlight(_lastObject, highlightMaterial);
                }
                return;
            }
        }

        ClearHighlight();
    }

    void ClearHighlight() {
        if (_lastObject != null) {
            MuscleHighlightManager.Unhighlight(_lastObject);
            _lastObject = null;
        }
    }
}
