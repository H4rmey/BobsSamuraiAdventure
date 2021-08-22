using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public RectTransform healthBar;
    public RectTransform bar;

    private float maxSize;
    private float filledPercentage;

    public float maxHealth, currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxSize = healthBar.sizeDelta.x;
    }

    public void Run()
    {
        bar.sizeDelta = new Vector2(currentHealth / maxHealth * maxSize, bar.sizeDelta.y);

        TextMeshProUGUI t = GetComponentInChildren<TextMeshProUGUI>(); 
        t.text = currentHealth.ToString();
    }
}

