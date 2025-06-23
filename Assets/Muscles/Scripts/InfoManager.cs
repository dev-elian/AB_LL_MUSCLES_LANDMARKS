using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class InfoManager : MonoBehaviour {
    public float laserDistance = 10f;
    public Transform laserOrigin;

    public string interactableTag = "Muscles";

    public Transform musclesContainer;
    public Transform togglesContainer;
    public Transform descriptionsContainer;
    public TextMeshProUGUI infoText;

    public GameObject[] objectsToHit;
    public GameObject[] descriptions;
    public CanvasGroup[] backgrounds;
    public TextMeshProUGUI[] textOfToggle;

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
        descriptions = new GameObject[muscleCount];
        backgrounds = new CanvasGroup[muscleCount];
        textOfToggle = new TextMeshProUGUI[muscleCount];
        for (int i = 0; i < muscleCount; i++) {
            objectsToHit[i] = MuscleFunctions.Instance._legMuscles[i].MuscleObject;
            descriptions[i] = descriptionsContainer.GetChild(i).gameObject;
            backgrounds[i] = togglesContainer.GetChild(i).GetChild(1).GetComponent<CanvasGroup>();
            textOfToggle[i] = togglesContainer.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>();
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
            backgrounds[oldIndex].alpha = 0f;
            backgrounds[oldIndex].gameObject.SetActive(false);
            textOfToggle[oldIndex].color = Color.white;
            textOfToggle[oldIndex].fontWeight = FontWeight.Bold;
            textOfToggle[oldIndex].fontSize = 10;
            descriptions[oldIndex].SetActive(false);
            oldIndex = -1;
        }
    }

    void ShowObjectInfo(GameObject obj) {
        infoText.text = "";
        DisableOld();
        int index = Array.IndexOf(objectsToHit, obj);
        if (index != -1 && index < descriptions.Length && infoText != null) {
            descriptions[index].SetActive(true);
            textOfToggle[index].color = Color.yellow;
            textOfToggle[index].fontWeight = FontWeight.Regular;
            textOfToggle[index].fontSize = 12;
            StartCoroutine(FadeIn(index));
            oldIndex = index;
        }
    }

    IEnumerator FadeIn(int index) {
        float duration = 0.3f;
        float time = 0f;
        backgrounds[index].gameObject.SetActive(true);

        while (time < duration) {
            float t = time / duration;
            backgrounds[index].alpha = Mathf.Lerp(0f, 1f, t);
            time += Time.deltaTime;
            yield return null;
        }
        backgrounds[index].alpha = 1f;
    }
}