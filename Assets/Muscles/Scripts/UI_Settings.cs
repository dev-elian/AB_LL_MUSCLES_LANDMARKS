using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
	[SerializeField] Toggle _enablePassthrough;

	void Start() {
		_enablePassthrough.isOn = CameraMaskController.Instance.IsMaskVisible;
	}

	void OnEnable() {
		_enablePassthrough.onValueChanged.AddListener(OnEnablePassthroughChanged);
	}

	void OnDisable() {
		_enablePassthrough.onValueChanged.RemoveListener(OnEnablePassthroughChanged);
	}

	void OnEnablePassthroughChanged(bool active) {
		CameraMaskController.Instance.IsMaskVisible = active;
	}
}
