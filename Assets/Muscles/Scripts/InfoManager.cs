using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour {
    public float laserDistance = 10f;
    public Transform laserOrigin;

    public string interactableTag = "Muscles";

    public Transform musclesContainer;
    public Transform togglesContainer;
    public Transform descriptionsContainer;
    public TextMeshProUGUI infoText;

    public GameObject[] objectsToHit; 
    public TextMeshProUGUI[] textOfToggle;
    public GameObject[] descriptions;

    void Awake() {
        StartCoroutine(Initializeinfo());
    }

    IEnumerator Initializeinfo() {
        while (!MuscleFunctions.Instance.initialized) {
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.1f);
        int muscleCount = MuscleFunctions.Instance._legMuscles.Count;
        objectsToHit = new GameObject[muscleCount];
        textOfToggle = new TextMeshProUGUI[muscleCount];
        descriptions = new GameObject[muscleCount];
        for (int i = 0; i < muscleCount; i++) {
            objectsToHit[i] = MuscleFunctions.Instance._legMuscles[i].MuscleObject;
            textOfToggle[i] = togglesContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            descriptions[i] = descriptionsContainer.GetChild(i).gameObject;
        }
    }

    void Update() {
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out RaycastHit hit, laserDistance)) {
            if (hit.collider.CompareTag(interactableTag)) {
                ShowObjectInfo(hit.collider.gameObject);
            } else
                DisableOld();
        } else
            DisableOld();
    }

    int oldIndex = -1; 

    void DisableOld() {
        if (oldIndex != -1) {
            descriptions[oldIndex].SetActive(false);
            textOfToggle[oldIndex].color = Color.white;
            oldIndex = -1; // Reset old index after disabling
        }
    }
    void ShowObjectInfo(GameObject obj) {
        infoText.text = "";
        DisableOld();
        // Find the index of the object in the array
        int index = System.Array.IndexOf(objectsToHit, obj);
        if (index != -1 && index < descriptions.Length && infoText != null) {
            textOfToggle[index].color = Color.yellow;
            descriptions[index].SetActive(true);
        }
        oldIndex = index;
    }


}
