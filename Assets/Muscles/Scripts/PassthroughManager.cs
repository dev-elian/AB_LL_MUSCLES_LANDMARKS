using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PassthroughManager : MonoBehaviour
{
	public static PassthroughManager Instance { get; private set; }

	[SerializeField] ARCameraBackground _arCameraBackground;

	[SerializeField] GameObject[] _gameObjectsToEnableWhenEnabled = new GameObject[0];
	[SerializeField] GameObject[] _gameObjectsToDisableWhenEnabled = new GameObject[0];

	public bool IsPassthroughEnabled {
		get {
			return _arCameraBackground.enabled;
		}
		set {
			_arCameraBackground.enabled = value;
			ProcessGameObjects(value);
		}
	}

	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(this);
			return;
		}
	}

	void ProcessGameObjects(bool isPassthroughEnabled) {
		foreach (GameObject go in _gameObjectsToEnableWhenEnabled)
		{
			if (go != null)
			{
				go.SetActive(isPassthroughEnabled);
			}
		}

		foreach (GameObject go in _gameObjectsToDisableWhenEnabled)
		{
			if (go != null)
			{
				go.SetActive(!isPassthroughEnabled);
			}
		}
	}
}
