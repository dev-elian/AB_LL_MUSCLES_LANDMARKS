using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ToggleObject {
    public GameObject gameObject;
    public Toggle toggle;
}
public class UIManager : MonoBehaviour {
    [Header("Canvas")]
    public GameObject infoCanvas;
    public GameObject musclesCanvas;
    public GameObject muscleFunctionsCanvas;

    [Header("Buttons")]
    public Button _btnMuscleList;
    public Button _btnMuscleFunctions;
    public Button _btnMuscleInfo;
    public Button _btnResetMuscle;

    Vector3 _scaleHidden = Vector3.zero;
    Vector3 _scaleShown = Vector3.one;
    float _animationDuration = 0.35f;
    Coroutine _scaleRoutine;
    bool _visible = false;

    void Awake() {
        DisableAll();
    }
    void OnEnable() {
        InputController.Instance.OnMenuButtonPressed += SetPanelVisibility;
        _btnResetMuscle.onClick.AddListener(ResetMusclePositions);
        _btnMuscleList.onClick.AddListener(UI_EnableMuscles);
        _btnMuscleInfo.onClick.AddListener(UI_EnableInfo);
        _btnMuscleFunctions.onClick.AddListener(UI_EnableMuscleFunctions);
        DisableAll();
        transform.localScale = Vector3.zero;
    }
    void OnDisable() {
        InputController.Instance.OnMenuButtonPressed -= SetPanelVisibility;
        _btnResetMuscle.onClick.RemoveListener(ResetMusclePositions);
        _btnMuscleList.onClick.RemoveListener(UI_EnableMuscles);
        _btnMuscleInfo.onClick.RemoveListener(UI_EnableInfo);
        _btnMuscleFunctions.onClick.RemoveListener(UI_EnableMuscleFunctions);
    }

    void DisableAll() {
        infoCanvas.SetActive(false);
        musclesCanvas.SetActive(false);
        muscleFunctionsCanvas.SetActive(false);

        //Deselect Buttons
        _btnMuscleList.transform.GetChild(2).gameObject.SetActive(false);
        _btnMuscleFunctions.transform.GetChild(2).gameObject.SetActive(false);
        _btnMuscleInfo.transform.GetChild(2).gameObject.SetActive(false);
    }
    void SetPanelVisibility() {
        if (_visible) {
            if (_scaleRoutine != null) StopCoroutine(_scaleRoutine);
            _scaleRoutine = StartCoroutine(ScaleAndDisable());
        } else {
            transform.localScale = _scaleHidden;
            _visible = true;
            if (_scaleRoutine != null) StopCoroutine(_scaleRoutine);
            _scaleRoutine = StartCoroutine(ScaleRoutine(_scaleHidden, _scaleShown, _animationDuration));
        }
    }
    void UI_EnableInfo() {
        bool toggled = !infoCanvas.activeInHierarchy;
        DisableAll();
        infoCanvas.SetActive(toggled);
        _btnMuscleInfo.transform.GetChild(2).gameObject.SetActive(toggled);
    }
    void UI_EnableMuscles() {
        bool toggled = !musclesCanvas.activeInHierarchy;
        DisableAll();
        musclesCanvas.SetActive(toggled);
        _btnMuscleList.transform.GetChild(2).gameObject.SetActive(toggled);
    }
    void UI_EnableMuscleFunctions() {
        bool toggled = !muscleFunctionsCanvas.activeInHierarchy;
        DisableAll();
        muscleFunctionsCanvas.SetActive(toggled);
        _btnMuscleFunctions.transform.GetChild(2).gameObject.SetActive(toggled);
    }
    void ResetMusclePositions() {
        ResetThePositions.Instance.ResetPositions();
    }

    IEnumerator ScaleRoutine(Vector3 from, Vector3 to, float duration) {
        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / duration;
            float eased = Mathf.SmoothStep(0, 1, t);
            transform.localScale = Vector3.LerpUnclamped(from, to, eased);
            yield return null;
        }
        transform.localScale = to;
    }
    IEnumerator ScaleAndDisable() {
        yield return ScaleRoutine(_scaleShown, _scaleHidden, _animationDuration);
        _visible = false;
    }
}
