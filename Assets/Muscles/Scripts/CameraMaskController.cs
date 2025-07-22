using UnityEngine;

public class CameraMaskController : MonoBehaviour
{
	public static CameraMaskController Instance { get; private set; }

	[SerializeField] MeshRenderer _meshRenderer;

	public bool IsMaskVisible {
		get => _meshRenderer.enabled;
		set => _meshRenderer.enabled = value;
	}

	void Awake() {
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}

	void Update() {
		UpdatePosition();
	}

	void UpdatePosition() {
		if (Camera.main != null)
		{
			transform.position = Camera.main.transform.position;
			transform.rotation = Camera.main.transform.rotation;
		}
	}
}
