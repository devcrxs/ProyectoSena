using UnityEngine;
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem rayEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private ParticleSystem landEffect;
    public static PlayerEffects instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlayRayEffect()
    {
        StartEffect(rayEffect);
    }

    public void PlayJumpEffect()
    {
        StartEffect(jumpEffect);
    }

    public void PlayLandEffect()
    {
        StartEffect(landEffect);
    }
    private void StartEffect(ParticleSystem particleSystem)
    {
        particleSystem.Play();
    }
}
