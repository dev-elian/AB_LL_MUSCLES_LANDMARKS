using UnityEngine;
using UnityEngine.UI;

public class UI_MusclesList : MonoBehaviour
{
    public Toggle globalToggle;
    public Transform musclesContainer;
    public Transform togglesContainer;
    public ToggleObject[] _muscles;

    void Awake() {
        _muscles = new ToggleObject[togglesContainer.childCount];
        for (int i = 0; i < togglesContainer.childCount; i++) {
            Transform child = togglesContainer.GetChild(i).GetChild(1).GetChild(0);
            _muscles[i] = new ToggleObject {
                gameObject = musclesContainer.GetChild(i).gameObject,
                toggle = child.GetComponent<Toggle>()
            };
        }
        globalToggle.isOn = true;
        ToggleAllObjects(true);
    }

    void OnEnable() {
        globalToggle.onValueChanged.AddListener(ToggleAllObjects);
        ToggleAllObjects(true);
    }

    void OnDisable() {
        globalToggle.onValueChanged.RemoveListener(ToggleAllObjects);
    }

    void Start() {
        for (int i = 0; i < _muscles.Length; i++) {
            int index = i;
            _muscles[i].toggle.onValueChanged.AddListener((value) => ToggleObject(index, value));
        }
    }

    void ToggleObject(int index, bool value) {
        if (index >= 0 && index < _muscles.Length) {
            _muscles[index].gameObject.SetActive(value);
        }
    }

    void ToggleAllObjects(bool value) {
        foreach (ToggleObject obj in _muscles) {
            if (obj != null) {
                if (MuscleFunctions.Instance.active) {
                    bool active = obj.gameObject.activeInHierarchy;
                    obj.gameObject.SetActive(active);
                    obj.toggle.isOn = active;
                    obj.toggle.interactable = value;

                } else {
                    obj.gameObject.SetActive(value);
                    obj.toggle.isOn = value;
                    obj.toggle.interactable = true;
                }
            }
        }
    }
}
