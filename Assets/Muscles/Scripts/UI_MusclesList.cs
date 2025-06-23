using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MusclesList : MonoBehaviour
{
    [SerializeField] GameObject _togglePrefab;
    [SerializeField] Toggle _globalToggle;
    [SerializeField] Toggle _muscleFunctionsToggle;
    [SerializeField] Transform _togglesContainer;
    [SerializeField] List<ToggleObject> _muscles = new();

    UIManager _uiManager;

    void Awake() {
        _uiManager = GetComponentInParent<UIManager>();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize() {
        yield return new WaitUntil(() => MuscleFunctions.Instance != null);
        for (int i = 0; i < MuscleFunctions.Instance._legMuscles.Count; i++) {
            GameObject newToggle = Instantiate(_togglePrefab, _togglesContainer);
            _muscles.Add(new ToggleObject {
                muscleGameObject = MuscleFunctions.Instance._legMuscles[i].MuscleObject,
                toggle = newToggle.transform.GetChild(0).GetChild(0).GetComponent<Toggle>(),
            });
            newToggle.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = MuscleFunctions.Instance._legMuscles[i].Name;
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
        _muscleFunctionsToggle.onValueChanged.AddListener(SetMuscleFunctionsEnable);
        _uiManager.OnMenuChanged += SetVisibles;
        _globalToggle.onValueChanged.AddListener(ToggleAllObjects);
        _globalToggle.interactable = !MuscleFunctions.Instance.active;
        _globalToggle.isOn = !MuscleFunctions.Instance.active;
        _muscleFunctionsToggle.isOn = MuscleFunctions.Instance.active;
    }

    bool _setting = false;
    void SetMuscleFunctionsEnable(bool active) {
        if (_setting) return; 
        _setting = true;
        MuscleFunctions.Instance.active = active;
        SetVisibles();
        _setting = false;
    }

    void OnDisable() {
        _muscleFunctionsToggle.onValueChanged.RemoveListener(SetMuscleFunctionsEnable);
        _uiManager.OnMenuChanged -= SetVisibles;
        _globalToggle.onValueChanged.RemoveListener(ToggleAllObjects);
    }

    void ToggleObject(int index, bool value) {
        if (index >= 0 && index < _muscles.Count) {
            _muscles[index].muscleGameObject.SetActive(value);
        }
    }

    void SetVisibles() {
        if (MuscleFunctions.Instance.active) {
            MuscleFunctions.Instance.ShowLastSelected();
        } else {
            MuscleFunctions.Instance.ShowAllMuscles();
        }
        ToggleAllObjects(true);
        _muscleFunctionsToggle.isOn = MuscleFunctions.Instance.active;
        _globalToggle.interactable = !MuscleFunctions.Instance.active;
        _globalToggle.isOn = !MuscleFunctions.Instance.active;
    }

    void ToggleAllObjects(bool value) {
        foreach (ToggleObject obj in _muscles) {
            if (obj != null) {
                if (MuscleFunctions.Instance.active) {
                    bool active = MuscleFunctions.Instance.MuscleVisible(obj.muscleGameObject);
                    obj.muscleGameObject.SetActive(true);
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