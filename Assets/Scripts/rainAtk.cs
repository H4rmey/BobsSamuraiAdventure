using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainAtk : MonoBehaviour
{
    public GameObject fireball;
    void rainTest() {
        for (int i = 20; i > 0; i--) {
            Vector3 randomPos = UnityEngine.Random.insideUnitCircle * 1f;
            Instantiate(fireball, randomPos, Quaternion.identity);
        }
    }
}
