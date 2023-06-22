using System.Collections;
using UnityEngine;

public class ShockWaveManager : MonoBehaviour
{
    public static ShockWaveManager instance;
    [SerializeField] private float shockWaveTime = 0.75f;
    [SerializeField] private float sizeDefault;

    private Material _material;
    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCenter");
    private static int _SIze = Shader.PropertyToID("_SIze");

    private void Awake()
    {
        if (instance == null) instance = this;
        _material = GetComponent<SpriteRenderer>().material;
    }

    public void CallShockWave()
    {
        StartCoroutine(ShockWaveAction(0.093f, 1f));
    }

    private IEnumerator ShockWaveAction(float startPos, float endPos)
    {
        _material.SetFloat(_SIze,sizeDefault);
        _material.SetFloat(_waveDistanceFromCenter,startPos);
        float lerpedAmount;
        float elapsedTime = 0;

        while (elapsedTime < shockWaveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / shockWaveTime));
            _material.SetFloat(_waveDistanceFromCenter,lerpedAmount);
            yield return null;
        }
        _material.SetFloat(_SIze,0);
    }
}
