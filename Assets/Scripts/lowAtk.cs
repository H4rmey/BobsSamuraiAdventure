using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lowAtk : MonoBehaviour {
    public GameObject hitBox;
    public GameObject fireBreath;
    public Transform fireBreathPos;


    void LowAtk() {
        Instantiate(fireBreath, fireBreathPos.position, fireBreath.transform.rotation);
        Instantiate(hitBox, hitBox.transform.position, hitBox.transform.rotation);
    }

    void DestroyFireBreath() {
        Destroy(fireBreath);
        Destroy(hitBox);
    }
}
