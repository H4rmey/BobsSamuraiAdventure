using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatDrop : MonoBehaviour

{
    private PlatformEffector2D effector;
    public float waitTime;
    void Start() {
        effector = GetComponent<PlatformEffector2D>(); 
    }

    void Update() {

        if (Input.GetKey(KeyCode.S)) {
            effector.rotationalOffset = 180f;
        }
    
        if(Input.GetButtonDown("Jump")) {

            effector.rotationalOffset = 0f;
        }
    }
}
