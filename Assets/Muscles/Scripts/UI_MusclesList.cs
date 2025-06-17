using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MusclesList : MonoBehaviour
{
    [SerializeField] GameObject _togglePrefab;
    [SerializeField] Toggle _globalToggle;
    [SerializeField] Transform _togglesContainer;
    [SerializeField] List<ToggleObject> _muscles = new();

    void Awake() {
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize() {
        yield return new WaitUntil(() => MuscleFunctions.Instance != null);
        for (int i = 0; i < MuscleFunctions.Instance._legMuscles.Count; i++) {
            GameObject newToggle = Instantiate(_togglePrefab, _togglesContainer);
            _muscles.Add(new ToggleObject {
                muscleGameObject = MuscleFunctions.Instance._legMuscles[i].MuscleObject,
                toggle = newToggle.transform.GetChild(1).GetChild(0).GetComponent<Toggle>(),
            });
            newToggle.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = MuscleFunctions.Instance._legMuscles[i].Name;
        }
        _globalToggle.isOn = true;
        ToggleAllObjects(true);
        MuscleFunctions.Instance.initialized = true;
        for (int i = 0; i < _muscles.Count; i++) {
            int index = i;
            _muscles[i].toggle.onValueChanged.AddListener((value) => ToggleObject(index, value));
        }
    }

    void OnEnable() {
        _globalToggle.onValueChanged.AddListener(ToggleAllObjects);
        ToggleAllObjects(true);
        _globalToggle.interactable = !MuscleFunctions.Instance.active;
        _globalToggle.isOn = !MuscleFunctions.Instance.active;
    }

    void OnDisable() {
        _globalToggle.onValueChanged.RemoveListener(ToggleAllObjects);
    }

    void ToggleObject(int index, bool value) {
        if (index >= 0 && index < _muscles.Count) {
            _muscles[index].muscleGameObject.SetActive(value);
        }
    }

    void ToggleAllObjects(bool value) {
        foreach (ToggleObject obj in _muscles) {
            if (obj != null) {
                if (MuscleFunctions.Instance.active) {
                    bool active = obj.muscleGameObject.activeInHierarchy;
                    obj.muscleGameObject.SetActive(active);
                    obj.toggle.isOn = active;
                    obj.toggle.interactable = active;

                } else {
                    obj.muscleGameObject.SetActive(value);
                    obj.toggle.isOn = value;
                    obj.toggle.interactable = true;
                }
            }
        }
    }
}

[Serializable]
public class ToggleObject {
    public GameObject muscleGameObject;
    public Toggle toggle;
}