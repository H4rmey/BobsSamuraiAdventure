using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAttributes : MonoBehaviour
{
    public HealthBar hp;
    public float currentHealth = 750;
    public float maxHealth = 1000;

    public SpriteRenderer[] spriteRenderers;
    public float restoreFactor= 4f;

    public void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();    
    }

    public void Update()
    {
        hp.currentHealth    = this.currentHealth;
        hp.maxHealth        = this.maxHealth;
        hp.Run();

        if (this.currentHealth < 0)
            SceneManager.LoadScene("Score");
    }
}