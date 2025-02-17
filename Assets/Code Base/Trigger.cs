using UnityEngine;

public class Trigger : MonoBehaviour
{
    private ScreenShaderControl _shader;
    [SerializeField] private float horizontalNoise;
    [SerializeField] private float verticalNoise;
    [SerializeField] private float glitchStrength;

    private void Start()
    {
        _shader = FindAnyObjectByType<ScreenShaderControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _shader.EnableGlitch(horizontalNoise,verticalNoise,glitchStrength);
    }

    private void OnTriggerExit(Collider other)
    {
        _shader.Reset();
    }
}
