//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InputManager : MonoBehaviour
//{

//    public Animation anim;
//    public GameObject mainCanvas, infoCanvas, listCanvas;
//    // Start is called before the first frame update
//    void Start()
//    {
//        anim.Play("idle");

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (OVRInput.Get(OVRInput.Button.One))
//        {
//            anim.Play("expand");
//        }
//        else if (OVRInput.Get(OVRInput.Button.Two)) 
//        {
//            anim.Play("join");

//        }else if (OVRInput.GetDown(OVRInput.Button.Four))
//        {

//            if (mainCanvas.activeSelf)
//            {
//                mainCanvas.SetActive(false);
//            }
//            else
//            {
//                infoCanvas.SetActive(false);
//                listCanvas.SetActive(false);
//                mainCanvas.SetActive(true);
//            }
//        }


        

//    }
//}
