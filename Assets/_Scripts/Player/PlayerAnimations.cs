using UnityEngine;
public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations instance;
    private Animator currentAnimator;
    [SerializeField] private Animator animatorHuman;
    [SerializeField] private Animator animatorCat;
    public Animator CurrentAnimator => currentAnimator;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        SetAnimations(animatorHuman);
        SetAnimations(animatorCat);
    }

    private void SetAnimations(Animator animator)
    {
        if (!animator.gameObject.activeInHierarchy) return;
        currentAnimator = animator;
        float speed = PlayerProperties.instance.IsDynamicBody() && !GameManager.instance.DesactiveInputs? Input.GetAxisRaw("Horizontal") : 0;
        animator.SetFloat("Speed", speed);
        if(!PlayerProperties.instance.IsDynamicBody()) return;
        animator.SetBool("IsJump",PlayerJump.instance.IsJumping);
        animator.SetBool("IsFalling",PlayerJump.instance.IsFalling);
        animator.SetBool("IsDash",PlayerDash.instance.DashAnimation);
        animator.SetBool("IsMoving", speed != 0);       
    }

    public void CallConvertAnimation()
    {
        currentAnimator = animatorHuman.gameObject.activeInHierarchy ? animatorHuman : animatorCat;
        currentAnimator.SetTrigger("Convert");
    }

    public void ResetAnimations()
    {
        currentAnimator = animatorHuman.gameObject.activeInHierarchy ? animatorHuman : animatorCat;
        currentAnimator.SetFloat("Speed", 0);
        currentAnimator.SetBool("IsJump",false);
        currentAnimator.SetBool("IsFalling",false);
        currentAnimator.SetBool("IsDash",false);
        currentAnimator.SetBool("IsMoving",false);       
    }

    public void AnimationDead()
    {
        currentAnimator = animatorHuman.gameObject.activeInHierarchy ? animatorHuman : animatorCat;
        currentAnimator.SetTrigger("IsDead");
        GameManager.instance.ResetPlayerDead();
    }
}
