using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;

    [SerializeField, Range(0,30)] private float timeOfDay;

    [SerializeField] private float sunRotationSpeed;

    [Header("LightingPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;

    void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if (timeOfDay > 30)
            timeOfDay = 0;
        SunRotation();
        UpdateLighting();
    }
    private void OnValidate()
    {
        SunRotation();
        UpdateLighting();
    }
    private void SunRotation()
    {
        float sunRotation = Mathf.Lerp(-50, 230, timeOfDay / 30);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }
    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 30;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }
}
