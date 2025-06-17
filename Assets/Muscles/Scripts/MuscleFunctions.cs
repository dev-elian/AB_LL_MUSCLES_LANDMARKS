using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscleFunctions : MonoBehaviour
{
    public static MuscleFunctions Instance { get; private set; }

    public bool active = true;
    public bool transitioning = false;
    [SerializeField] Material _transitionToTransparentMaterial;
    [SerializeField] Material _transitionToMusclesMaterial;
    [SerializeField] Material _muscleMaterial;
    [SerializeField] Material _transparentMaterial;
    //GameObjects are assigned in editor DATA FROM WEBSITE
    [SerializeField] List<LegMuscle> _legMuscles = new()  {
        new LegMuscle {
            Name = "Adductor Brevis",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Initiate1 = LegMovement.Adduction,
            Control1 = LegMovement.Abduction,
            JointMovement1 = LegMovement.Adduction,
            Origin = "Inferior pubic ramus",
            Insertion = "Linea aspera of the femur"
        },
        new LegMuscle {
            Name = "Adductor Longus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Initiate1 = LegMovement.Adduction,
            Control1 = LegMovement.Abduction,
            JointMovement1 = LegMovement.Adduction,
            Origin = "Medial portion of the superior pubic ramus",
            Insertion = "Linea aspera of the femur"
        },
        new LegMuscle {
            Name = "Adductor Magnus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Initiate1 = LegMovement.Adduction,
            Control1 = LegMovement.Abduction,
            JointMovement1 = LegMovement.Adduction,
            Origin = "Ischiopubic ramus and ischial tuberosity",
            Insertion = "Linea aspera of the femur and adductor tubercle"
        },
        new LegMuscle {
            Name = "Biceps Femoris Long Head",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Flexion,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.Extension,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Ischial tuberosity",
            Insertion = "Head of the fibula"
        },
        new LegMuscle {
            Name = "Biceps Femoris Short Head",
            Joint1 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Flexion,
            Control1 = LegMovement.Extension,
            JointMovement1 = LegMovement.Flexion,
            Origin = "Linea aspera of the femur",
            Insertion = "Head of the the fibula"
        },
        new LegMuscle {
            Name = "Extensor Digitorum Longus",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.Dorsiflexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.PlantarFlexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.Dorsiflexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Lateral condyle of the tibia, proximal fibula and interosseous membrane",
            Insertion = "Middle and distal phalanges of the lateral four digits"
        },
        new LegMuscle {
            Name = "Extensor Hallucis Longus",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.Dorsiflexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.PlantarFlexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.Dorsiflexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Middle third of the fibula and interosseous membrane",
            Insertion = "Distal phalanx of the great toe"
        },
        new LegMuscle {
            Name = "Fibularis Brevis",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Eversion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Inversion,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Eversion,
            Origin = "Distal two-thirds of the lateral surface of the fibula",
            Insertion = "Tuberosity of the the fifth metatarsal bone"
        },
        new LegMuscle {
            Name = "Fibularis Longus",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Eversion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Inversion,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Eversion,
            Origin = "Head and upper two-thirds of the fibula",
            Insertion = "Base of the first metatarsal and medial cuneiform bones"
        },
        new LegMuscle {
            Name = "Fibularis Tertius",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.Dorsiflexion,
            Initiate2 = LegMovement.Eversion,
            Control1 = LegMovement.PlantarFlexion,
            Control2 = LegMovement.Inversion,
            JointMovement1 = LegMovement.Dorsiflexion,
            JointMovement2 = LegMovement.Eversion,
            Origin = "Distal part of the anterior surface of the fibula",
            Insertion = "Base of the fifth metatarsal bone"
        },
        new LegMuscle {
            Name = "Flexor Digitorum Longus",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Posterior surface of the tibia",
            Insertion = "Plantar surfaces of the distal phalanges of the lateral four toes"
        },
        new LegMuscle {
            Name = "Flexor Hallucis Longus",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Posterior surface of the fibula",
            Insertion = "Plantar surface of the distal phalanx of the great toe"
        },
        new LegMuscle {
            Name = "Gastrocnemius",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Medial and lateral condyles of the femur",
            Insertion = "Calcaneus via calcaneal tendon"
        },
        new LegMuscle {
            Name = "Gluteus Maximus",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Hip,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Transverse,
            Initiate1 = LegMovement.Extension,
            Initiate2 = LegMovement.ExternalRotation,
            Control1 = LegMovement.Flexion,
            Control2 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.Extension,
            JointMovement2 = LegMovement.ExternalRotation,
            Origin = "Posterior iliac crest, sacrum, and coccyx",
            Insertion = "Gluteal tuberosity"
        },
        new LegMuscle {
            Name = "Gluteus Medius",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Initiate1 = LegMovement.Abduction,
            Control1 = LegMovement.Adduction,
            JointMovement1 = LegMovement.Abduction,
            Origin = "Lateral surface of the ilium",
            Insertion = "Greater trochanter"
        },
        new LegMuscle {
            Name = "Gluteus Minimus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Initiate1 = LegMovement.Abduction,
            Control1 = LegMovement.Adduction,
            JointMovement1 = LegMovement.Abduction,
            Origin = "Lateral surface of the ilium",
            Insertion = "Greater trochanter"
        },
        new LegMuscle {
            Name = "Gracilis",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Frontal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Adduction,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Abduction,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.Adduction,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Inferior ramus of the pubis",
            Insertion = "Medial surface of the tibia"
        },
        new LegMuscle {
            Name = "Iliacus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Flexion,
            Control1 = LegMovement.Extension,
            JointMovement1 = LegMovement.Flexion,
            Origin = "Iliac fossa and ala of the sacrum",
            Insertion = "Lesser trochanter"
        },
        new LegMuscle {
            Name = "Inferior Gemellus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Transverse,
            Initiate1 = LegMovement.ExternalRotation,
            Control1 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.ExternalRotation,
            Origin = "Ischial tuberosity",
            Insertion = "Medial surface of the greater trochanter"
        },
        new LegMuscle {
            Name = "Obturator Externus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Transverse,
            Initiate1 = LegMovement.ExternalRotation,
            Control1 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.ExternalRotation,
            Origin = "Outer surface of obturator membrane and bony margin",
            Insertion = "Trochanteric fossa"
        },
        new LegMuscle {
            Name = "Obturator Internus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Transverse,
            Initiate1 = LegMovement.ExternalRotation,
            Control1 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.ExternalRotation,
            Origin = "Inner surface of obturator membrane and bony margin",
            Insertion = "Trochanteric fossa"
        },
        new LegMuscle {
            Name = "Pectineus",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Plane2 = LegPlane.Transverse,
            Initiate1 = LegMovement.Adduction,
            Initiate2 = LegMovement.ExternalRotation,
            Control1 = LegMovement.Abduction,
            Control2 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.Adduction,
            JointMovement2 = LegMovement.ExternalRotation,
            Origin = "Pectineal line of the pubis and femoral shaft",
            Insertion = "Pectineal line of the femur"
        },
        new LegMuscle {
            Name = "Piriformis",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Transverse,
            Initiate1 = LegMovement.ExternalRotation,
            Control1 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.ExternalRotation,
            Origin = "Anterior surface of the sacrum",
            Insertion = "Superior border of the greater trochanter"
        },
        new LegMuscle {
            Name = "Plantaris",
            Joint1 = LegJoint.Knee,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Flexion,
            Initiate2 = LegMovement.PlantarFlexion,
            Control1 = LegMovement.Extension,
            Control2 = LegMovement.Dorsiflexion,
            JointMovement1 = LegMovement.Flexion,
            JointMovement2 = LegMovement.PlantarFlexion,
            Origin = "Lateral supracondylar line of the femur",
            Insertion = "Calcaneus via calcaneal tendon"
        },
        new LegMuscle {
            Name = "Popliteus",
            Joint1 = LegJoint.Knee,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Transverse,
            Initiate1 = LegMovement.Flexion,
            Initiate2 = LegMovement.InternalRotation,
            Control1 = LegMovement.Extension,
            Control2 = LegMovement.ExternalRotation,
            JointMovement1 = LegMovement.Flexion,
            JointMovement2 = LegMovement.InternalRotation,
            Origin = "Lateral condyle of the femur and posterior horn of the lateral meniscus",
            Insertion = "Posterior proximal surface of the tibia"
        },
        new LegMuscle {
            Name = "Rectus Femoris",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Flexion,
            Initiate2 = LegMovement.Extension,
            Control1 = LegMovement.Extension,
            Control2 = LegMovement.Flexion,
            JointMovement1 = LegMovement.Flexion,
            JointMovement2 = LegMovement.Extension,
            Origin = "Anterior inferior iliac spine (AIIS) and acetabular rim",
            Insertion = "Tibial tuberosity via patellar ligament"
        },
        new LegMuscle {
            Name = "Sartorius",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Flexion,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Extension,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.Flexion,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Anterior superior iliac spine (ASIS)",
            Insertion = "Proximal tibia via pes anserinus"
        },
        new LegMuscle {
            Name = "Semimembranosus",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Flexion,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.Extension,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Ischial tuberosity",
            Insertion = "Posterior medial condyle of the tibia"
        },
        new LegMuscle {
            Name = "Semitendinosus",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Initiate2 = LegMovement.Flexion,
            Control1 = LegMovement.Flexion,
            Control2 = LegMovement.Extension,
            JointMovement1 = LegMovement.Extension,
            JointMovement2 = LegMovement.Flexion,
            Origin = "Ischial tuberosity",
            Insertion = "Proximal medial surface of the tibia"
        },
        new LegMuscle {
            Name = "Soleus",
            Joint1 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.PlantarFlexion,
            Control1 = LegMovement.Dorsiflexion,
            JointMovement1 = LegMovement.PlantarFlexion,
            Origin = "Head and proximal shaft of the fibula",
            Insertion = "Calcaneus via calcaneal tendon"
        },
        new LegMuscle {
            Name = "Superior Gemellus",
            Joint1 = LegJoint.Hip,
            Plane1 = LegPlane.Transverse,
            Initiate1 = LegMovement.ExternalRotation,
            Control1 = LegMovement.InternalRotation,
            JointMovement1 = LegMovement.ExternalRotation,
            Origin = "Ischial tuberosity",
            Insertion = "Medial surface of the greater trochanter"
        },
        new LegMuscle {
            Name = "Tensor Fasciae Latae",
            Joint1 = LegJoint.Hip,
            Joint2 = LegJoint.Hip,
            Plane1 = LegPlane.Frontal,
            Plane2 = LegPlane.Transverse,
            Initiate1 = LegMovement.Abduction,
            Initiate2 = LegMovement.InternalRotation,
            Control1 = LegMovement.Adduction,
            Control2 = LegMovement.ExternalRotation,
            JointMovement1 = LegMovement.Abduction,
            JointMovement2 = LegMovement.InternalRotation,
            Origin = "Anterior iliac crest and anterior superior iliac spine (ASIS)",
            Insertion = "Iliotibial tract"
        },
        new LegMuscle {
            Name = "Tibialis Anterior",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.Dorsiflexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.PlantarFlexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.Dorsiflexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Lateral condyle and proximal shaft of the tibia",
            Insertion = "Medial cuneiform and first metatarsal bones"
        },
        new LegMuscle {
            Name = "Tibialis Posterior",
            Joint1 = LegJoint.Ankle,
            Joint2 = LegJoint.Ankle,
            Plane1 = LegPlane.Sagittal,
            Plane2 = LegPlane.Frontal,
            Initiate1 = LegMovement.PlantarFlexion,
            Initiate2 = LegMovement.Inversion,
            Control1 = LegMovement.Dorsiflexion,
            Control2 = LegMovement.Eversion,
            JointMovement1 = LegMovement.PlantarFlexion,
            JointMovement2 = LegMovement.Inversion,
            Origin = "Interosseous membrane and posterior surface of the tibia",
            Insertion = "Tuberosity of the navicular, cuneiforms, and bases of the second to fourth metatarsals"
        },
        new LegMuscle {
            Name = "Vastus Intermedius",
            Joint1 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Control1 = LegMovement.Flexion,
            JointMovement1 = LegMovement.Extension,
            Origin = "Anterior and lateral shaft of the femur",
            Insertion = "Tibial tuberosity via patellar ligament"
        },
        new LegMuscle {
            Name = "Vastus Lateralis",
            Joint1 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Control1 = LegMovement.Flexion,
            JointMovement1 = LegMovement.Extension,
            Origin = "Greater trochanter, intertrochanteric line, linea aspera and lateral supracondylar line",
            Insertion = "Tibial tuberosity via patellar ligament"
        },
        new LegMuscle {
            Name = "Vastus Medialis",
            Joint1 = LegJoint.Knee,
            Plane1 = LegPlane.Sagittal,
            Initiate1 = LegMovement.Extension,
            Control1 = LegMovement.Flexion,
            JointMovement1 = LegMovement.Extension,
            Origin = "Linea aspera and medial supracondylar line",
            Insertion = "Tibial tuberosity via patellar ligament"
        }
        };

    List<Renderer> _muscleRenderers;
    List<Renderer> _transitioningRendersToMuscle;
    List<Renderer> _transitioningRendersTotransparent;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        _muscleRenderers = new List<Renderer>();
        foreach (var muscle in _legMuscles) {
            if (muscle.MuscleObject == null) continue;
            Renderer[] renderers = muscle.MuscleObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) _muscleRenderers.Add(r);
        }
        ShowAllMuscles();
    }

    void ShowAllMuscles() {
        if (active) return;
        foreach (var muscle in _legMuscles) {
            if (muscle.MuscleObject == null) continue;
            muscle.MuscleObject.SetActive(true);
            Renderer[] renderers = muscle.MuscleObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) {
                if (r.sharedMaterial != _muscleMaterial) {
                    r.sharedMaterial = _muscleMaterial;
                }
            }
        }
    }

    public void SetMusclesByPlane(LegJoint legJoint, LegPlane legPlane, bool showAnimation) {
        ShowAllMuscles();
        if (!active) return;
        transitioning = true;
        _transitioningRendersToMuscle = new List<Renderer>();
        _transitioningRendersTotransparent = new List<Renderer>();
        foreach (var muscle in _legMuscles) {
            if (muscle.MuscleObject == null) continue;
            muscle.MuscleObject.SetActive(false);

            bool matches = (muscle.Joint1 == legJoint && muscle.Plane1 == legPlane) ||
                           (muscle.Joint2 == legJoint && muscle.Plane2 == legPlane);

            Renderer[] renderers = muscle.MuscleObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) {
                if (matches) {
                    muscle.MuscleObject.SetActive(true);
                    if (r.sharedMaterial != _muscleMaterial) {
                        _transitioningRendersToMuscle.Add(r);
                        r.sharedMaterial = _transitionToMusclesMaterial;
                    }
                } else {
                    if (r.sharedMaterial != _transparentMaterial) {
                        _transitioningRendersTotransparent.Add(r);
                        r.sharedMaterial = _transitionToTransparentMaterial;
                    }
                }
            }
        }
        StartCoroutine(FadeAlphas());
    }

    public void SetMusclesByMovement(LegJoint legJoint, LegMovement legMovement, bool showAnimation) {
        ShowAllMuscles();
        if (!active) return;
        transitioning = true;
        _transitioningRendersToMuscle = new List<Renderer>();
        _transitioningRendersTotransparent = new List<Renderer>();

        foreach (var muscle in _legMuscles) {
            if (muscle.MuscleObject == null) continue;
            muscle.MuscleObject.SetActive(false);

            bool matches = (muscle.Joint1 == legJoint && muscle.JointMovement1 == legMovement) ||
                           (muscle.Joint2 == legJoint && muscle.JointMovement2 == legMovement);

            Renderer[] renderers = muscle.MuscleObject.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) {
                if (matches) {
                    muscle.MuscleObject.SetActive(true);
                    if (r.sharedMaterial != _muscleMaterial) {
                        _transitioningRendersToMuscle.Add(r);
                        r.sharedMaterial = _transitionToMusclesMaterial;
                    }
                } else {
                    if (r.sharedMaterial != _transparentMaterial) {
                        _transitioningRendersTotransparent.Add(r);
                        r.sharedMaterial = _transitionToTransparentMaterial;
                    }
                }
            }
        }

        StartCoroutine(FadeAlphas());
    }

    IEnumerator FadeAlphas() {
        float t = 0f;
        float duration = .6f;
        while (t < duration) {
            float p = t / duration;
            p = p * p * (3f - 2f * p); // ease in-out

            float a = Mathf.Lerp(1, 0f, p);
            _transitionToTransparentMaterial.SetFloat("_Alpha", a);

            float b = Mathf.Lerp(0f, 1f, p);
            _transitionToMusclesMaterial.SetFloat("_Alpha", b);

            t += Time.deltaTime;
            yield return null;
        }

        foreach (var muscle in _transitioningRendersTotransparent) {
            muscle.gameObject.SetActive(false);
        }

        foreach (var item in _transitioningRendersToMuscle) {
            item.sharedMaterial = _muscleMaterial;
        }
        transitioning = false;
    }
}