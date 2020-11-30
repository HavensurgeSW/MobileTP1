using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnabler : MonoBehaviour{
    public GameObject controls;
   void Awake(){
    #if UNITY_ANDROID
            Debug.Log("This is Adnroid");
            controls.SetActive(true);
    #endif

    #if UNITY_STANDALONE
            Debug.Log("This is Windows");
            controls.SetActive(false);
    #endif
     
   }
}
