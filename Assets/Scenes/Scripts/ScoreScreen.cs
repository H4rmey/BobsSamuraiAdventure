using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreen : MonoBehaviour
{
    float time = 0;

    TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        text.text = PlayerPrefs.GetFloat("score").ToString();

        time += Time.deltaTime;

        if (time > 7.5)
            SceneManager.LoadScene("Home");
    }
}
