using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;


public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] GameObject host;
    // Start is called before the first frame update
    float health, maxHealth, healthRatio = 0;
    void Start()
    {
        maxHealth = host.GetComponent<Health>().healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        health = host.GetComponent<Health>().healthPoints;
        if (health <= 0 )
        {
            gameObject.SetActive(false);
            return;
        }
        healthRatio = health / maxHealth;
        slider.value = healthRatio;
        fill.color = gradient.Evaluate(healthRatio);
        
    }
}
