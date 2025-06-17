using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetThePositions : MonoBehaviour
{
    public static ResetThePositions Instance { get; private set; }

    public GameObject[] objectsToReset;
    Dictionary<Transform, Vector3> originalPositions = new();
    Dictionary<Transform, Vector3> originalRotations = new();

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SavePositions();
    }

    public void ResetPositions()
    {
        foreach (var entry in originalPositions)
        {
            entry.Key.localPosition = entry.Value;
        }

        foreach (var entryRot in originalRotations)
        {
            entryRot.Key.localEulerAngles = entryRot.Value;
        }

    }

    public void SavePositions()
    {
        originalPositions.Clear();
        originalRotations.Clear();

        foreach (GameObject obj in objectsToReset)
        {
            Transform objTransform = obj.transform;
            originalPositions[objTransform] = objTransform.localPosition;
            originalRotations[objTransform] = objTransform.localEulerAngles;
        }
    }

}
