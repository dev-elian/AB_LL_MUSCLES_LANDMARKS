using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class XRPlayer : MonoBehaviour
{
	[SerializeField]
	private Transform _cameraOffset;

	[SerializeField]
	private Transform _camera;

	private XRInputSubsystem _xrInputSubsystem;

	private void OnEnable()
	{
		List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
		SubsystemManager.GetSubsystems(subsystems);

		if (subsystems.Count > 0)
		{
			_xrInputSubsystem = subsystems[0];
		}

		if (_xrInputSubsystem != null)
		{
			_xrInputSubsystem.trackingOriginUpdated += OnTrackingOriginUpdated;
		}
	}

	private void OnDisable()
	{
		if (_xrInputSubsystem != null)
		{
			_xrInputSubsystem.trackingOriginUpdated -= OnTrackingOriginUpdated;
		}
	}

	private void Start()
	{
		StartCoroutine(InitializeStartingTransform());
	}

	private IEnumerator InitializeStartingTransform()
	{
		yield return XRGeneralSettings.Instance.Manager.activeLoader != null; // Wait for the XR Loader to be initialized
		yield return new WaitForSeconds(0.5f); // Works without this delay, but this is only to ensure the XR system is ready

		// Position
		Vector3 revertPosition = _camera.localPosition;
		revertPosition.y = 0;
		_cameraOffset.localPosition -= revertPosition;

		// Rotation
		_cameraOffset.RotateAround(_camera.position, Vector3.up, -_camera.eulerAngles.y);
	}

	private void OnTrackingOriginUpdated(XRInputSubsystem subsystem)
	{
		// Reset the camera offset to ensure it aligns with the new tracking origin
		_cameraOffset.localPosition = Vector3.zero;
		_cameraOffset.localRotation = Quaternion.identity;
	}
}
