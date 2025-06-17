using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour {
    public float laserDistance = 10f;
    public Transform laserOrigin;

    public string interactableTag = "Muscles"; // Tag for objects you want to interact with

    public Transform musclesContainer;
    public Transform togglesContainer;
    public Transform descriptionsContainer;
    public TextMeshProUGUI infoText; // UI Text element to display object information

    public GameObject[] objectsToHit; // Array of interactable objects
    public TextMeshProUGUI[] textOfToggle;
    public GameObject[] descriptions;


    void Awake() {
        // Initialize the arrays based on the number of children in the containers
        int muscleCount = musclesContainer.childCount;
        objectsToHit = new GameObject[muscleCount];
        textOfToggle = new TextMeshProUGUI[muscleCount];
        descriptions = new GameObject[muscleCount];
        for (int i = 0; i < muscleCount; i++) {
            //objectsToHit[i] = musclesContainer.GetChild(i).gameObject;
            //textOfToggle[i] = togglesContainer.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
            //descriptions[i] = descriptionsContainer.GetChild(i).gameObject;
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

    int oldIndex = -1; // To keep track of the previously highlighted object

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
