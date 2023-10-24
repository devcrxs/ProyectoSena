using System;
using System.Collections;
using UnityEngine;
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem rayEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private ParticleSystem landEffect;
    [SerializeField] private GameObject trailHuman;
    [SerializeField] private GameObject trailCat;
    private GameObject human;
    private GameObject cat;
    public static PlayerEffects instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        human = GameObject.FindWithTag("Human");
        cat = GameObject.FindWithTag("Cat");
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

    public void StartTrailGhost()
    {
        StartCoroutine(InstanceTrail());
    }

    private IEnumerator InstanceTrail()
    {
        while (PlayerDash.instance.DashAnimation)
        {
            GameObject effectInstance = human.activeInHierarchy ? trailHuman : trailCat;
            Instantiate(effectInstance, transform.position, transform.localRotation);
            yield return new WaitForSeconds(0.07f);
        }
        
    }
}
