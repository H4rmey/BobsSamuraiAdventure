using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRain : MonoBehaviour {

    public GameObject projectile;
    // Update is called once per frame
    void Update() {

        rainTest();
    }
    void rainTest() {

        if (Input.GetMouseButtonDown(1)) {

            for (int i = 20; i > 0; i--) {
                Vector3 randomPos = UnityEngine.Random.insideUnitCircle * 1f;
                Instantiate(projectile,  randomPos, this.transform.rotation);
            }
        }

    }
}