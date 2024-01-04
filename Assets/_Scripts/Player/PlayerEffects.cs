using System.Collections;
using UnityEngine;
public class PlayerEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem transformationEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private ParticleSystem landEffect;
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] private ParticleSystem deadEffect;
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

    public void PlayTransformationEffect()
    {
        StartEffect(transformationEffect);
    }

    public void PlayJumpEffect()
    {
        StartEffect(jumpEffect);
    }

    public void PlayLandEffect()
    {
        StartEffect(landEffect);
    }

    public void PlayDashEffect()
    {
        StartEffect(dashEffect);
    }
    public void PlayDeadEffect()
    {
        StartEffect(deadEffect);
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
