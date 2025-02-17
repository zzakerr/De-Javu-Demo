using System.Collections;
using UnityEngine;

public class ScreenShaderControl: MonoBehaviour
{
    private static readonly int NoiseAmountHorizontal = Shader.PropertyToID("_NoiseAmountHorizontal");
    private static readonly int NoiseAmountVertical = Shader.PropertyToID("_NoiseAmountVertical");
    private static readonly int GlitchStrength = Shader.PropertyToID("_GlitchStrength");
    
    [SerializeField] private Material material;
    [SerializeField] private float interpolateSpeed = 50f;

    private Coroutine _currentCoroutine;
    public void EnableGlitch(float horizontalNoiseAmount,float verticalNoiseAmount , float glitchStrength)
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(InterpolateGlitch(horizontalNoiseAmount, verticalNoiseAmount, glitchStrength));
    }

    private IEnumerator InterpolateGlitch(float horizontalNoiseAmount,float verticalNoiseAmount , float glitchStrength)
    {
        Debug.Log("Starting Glitch");
        var currentGlitchStrength = material.GetFloat(GlitchStrength);
        var currentVerticalNoiseAmount = material.GetFloat(NoiseAmountVertical);
        var currentHorizontalNoiseAmount = material.GetFloat(NoiseAmountHorizontal);
        var speed = interpolateSpeed * Time.deltaTime;
        while (true)
        {
            currentGlitchStrength = Mathf.MoveTowards(currentGlitchStrength, glitchStrength,speed);
            currentVerticalNoiseAmount = Mathf.MoveTowards(currentVerticalNoiseAmount, verticalNoiseAmount, speed);
            currentHorizontalNoiseAmount = Mathf.MoveTowards(currentHorizontalNoiseAmount, horizontalNoiseAmount, speed);
            
            material.SetFloat(GlitchStrength, currentGlitchStrength);
            material.SetFloat(NoiseAmountVertical, currentVerticalNoiseAmount);
            material.SetFloat(NoiseAmountHorizontal, currentHorizontalNoiseAmount);

            if (Mathf.Approximately(currentGlitchStrength, glitchStrength) &&
                Mathf.Approximately(currentVerticalNoiseAmount, verticalNoiseAmount) &&
                Mathf.Approximately(currentHorizontalNoiseAmount, horizontalNoiseAmount)) break;
            
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Ending Glitch Switch");
    }
    
    public void Reset()
    {
        if (material.GetFloat(GlitchStrength) == 0 && material.GetFloat(NoiseAmountVertical) == 0 &&
            material.GetFloat(NoiseAmountHorizontal) == 0) return;
        material.SetFloat(NoiseAmountHorizontal, 0);
        material.SetFloat(NoiseAmountVertical, 0);
        material.SetFloat(GlitchStrength, 0);
        Debug.Log("Glitch OFF");
    }

    private void OnDisable()
    {
        Reset();
    }
    
}
