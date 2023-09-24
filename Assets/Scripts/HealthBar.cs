using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        
    }

    public void UpdateHealthBar(float currantValue, float maxValue)
    {
        slider.value = currantValue / maxValue;
    }
}
