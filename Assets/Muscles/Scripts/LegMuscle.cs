using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LegMuscle {
    public GameObject MuscleObject;
    public string Name;
    public LegJoint Joint1 = LegJoint.None;
    public LegJoint Joint2 = LegJoint.None;
    public LegPlane Plane1 = LegPlane.None;
    public LegPlane Plane2 = LegPlane.None;
    public LegMovement Initiate1 = LegMovement.None;
    public LegMovement Initiate2 = LegMovement.None;
    public LegMovement Control1 = LegMovement.None;
    public LegMovement Control2 = LegMovement.None;
    public LegMovement JointMovement1 = LegMovement.None;
    public LegMovement JointMovement2 = LegMovement.None;
    public string Origin;
    public string Insertion;
}

public enum LegJoint {
    None = -1,
    Hip = 0,
    Knee = 1,
    Ankle = 2,
}

public enum LegPlane {
    None = -1,
    Sagittal = 0,
    Frontal = 1,
    Transverse = 2,
}
public enum LegMovement {
    None = -1,
    Flexion = 0,
    Extension = 1,
    Abduction = 2,
    Adduction = 3,
    InternalRotation = 4,
    ExternalRotation = 5,
    PlantarFlexion = 6,
    Dorsiflexion = 7,
    Inversion = 8,
    Eversion = 9
}

[CustomPropertyDrawer(typeof(LegMuscle))]
public class LegMuscleDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var nameProp = property.FindPropertyRelative("Name");
        if (!string.IsNullOrEmpty(nameProp.stringValue)) {
            label.text = nameProp.stringValue;
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
