using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MuscleFunctions : MonoBehaviour
{
    [SerializeField] GameObject _panelPlane;
    [SerializeField] GameObject _panelMovement;

    [SerializeField] Toggle _showSelected;
    [SerializeField] TMPro.TMP_Dropdown _joint;
    [SerializeField] TMPro.TMP_Dropdown _type;
    [SerializeField] TMPro.TMP_Dropdown _plane;
    [SerializeField] TMPro.TMP_Dropdown _movement;
    [SerializeField] Slider _sliderOpacity;

    IEnumerator Start() {
        while (!MuscleFunctions.Instance.initialized) {
            yield return new WaitForSeconds(.1f);
        }
        _joint.value = 0;
        _type.value = 0; // Default to "Muscle" type
        _panelPlane.SetActive(true); // Show plane panel for "Muscle" type
        _panelMovement.SetActive(false);
        SetMovementOptions(0);
        _movement.value = 0; // Default to first movement option
        if (MuscleFunctions.Instance != null && MuscleFunctions.Instance.initialized)
            _showSelected.isOn = MuscleFunctions.Instance.active;
    }

    void OnEnable() {
        _showSelected.onValueChanged.AddListener(OnShowSelectedChanged);
        _joint.onValueChanged.AddListener(OnJointChanged);
        _type.onValueChanged.AddListener(OnTypeChanged);
        _plane.onValueChanged.AddListener(OnPlaneChanged);
        _movement.onValueChanged.AddListener(OnMovementChanged);
        if (MuscleFunctions.Instance != null && MuscleFunctions.Instance.initialized) {
            _showSelected.isOn = MuscleFunctions.Instance.active;
            Select();
        }
        _sliderOpacity.onValueChanged.AddListener(OnSliderOpacityChanged);
    }

    void OnDisable() {
        _showSelected.onValueChanged.RemoveListener(OnShowSelectedChanged);
        _joint.onValueChanged.RemoveListener(OnJointChanged);
        _type.onValueChanged.RemoveListener(OnTypeChanged);
        _plane.onValueChanged.RemoveListener(OnPlaneChanged);
        _movement.onValueChanged.RemoveListener(OnMovementChanged);
        _sliderOpacity.onValueChanged.RemoveListener(OnSliderOpacityChanged);
    }

    void OnSliderOpacityChanged(float value) {
        MuscleFunctions.Instance.Setopacity(value);
    }

    void OnShowSelectedChanged(bool active) {
        MuscleFunctions.Instance.active = active;
        _joint.interactable = active;
        _type.interactable = active;
        _plane.interactable = active;
        _movement.interactable = active;
        if (active)
            Select();
        else
            MuscleFunctions.Instance.ShowAllMuscles();
    }

    void OnJointChanged(int index) {
        Select();
        SetMovementOptions(index);
    }
    void OnTypeChanged(int index) {
        _panelPlane.SetActive(index == 0); // Show plane panel for "Muscle" type
        _panelMovement.SetActive(index == 1); // Show movement panel for "Movement" type
        Select();
    }
    void OnPlaneChanged(int index) {
        Select();
    }
    void OnMovementChanged(int index) {
        Select();
    }
    void Select() {
        if (_type.value == 0)
            MuscleFunctions.Instance.SetMusclesByPlane((LegJoint)_joint.value, (LegPlane)_plane.value);
        else {
            var x = GetMovementFromDropdown(_movement.options[_movement.value].text);
            MuscleFunctions.Instance.SetMusclesByMovement((LegJoint)_joint.value, x);
        }
    }

    void SetMovementOptions(int index) {
        _movement.ClearOptions();
        switch ((LegJoint)index) {
            case LegJoint.None:
                break;
            case LegJoint.Hip:
                _movement.AddOptions(new List<string> {
                    "Flexion", "Extension", "Abduction", "Adduction", "Internal Rotation", "External Rotation"
                });
                break;
            case LegJoint.Knee:
                _movement.AddOptions(new List<string> {
                    "Flexion", "Extension", "Internal Rotation", "External Rotation"
                });
                break;
            case LegJoint.Ankle:
                _movement.AddOptions(new List<string> {
                    "Plantar Flexion", "Dorsiflexion", "Inversion", "Eversion"
                });
                break;
            default:
                break;
        }
    }

    LegMovement GetMovementFromDropdown(string value) {
        return value switch {
            "Flexion" => LegMovement.Flexion,
            "Extension" => LegMovement.Extension,
            "Abduction" => LegMovement.Abduction,
            "Adduction" => LegMovement.Adduction,
            "Internal Rotation" => LegMovement.InternalRotation,
            "External Rotation" => LegMovement.ExternalRotation,
            "Plantar Flexion" => LegMovement.PlantarFlexion,
            "Dorsiflexion" => LegMovement.Dorsiflexion,
            "Inversion" => LegMovement.Inversion,
            "Eversion" => LegMovement.Eversion,
            _ => LegMovement.None,
        };
    }
}
