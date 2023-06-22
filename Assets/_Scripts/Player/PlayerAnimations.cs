using UnityEngine;
public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations instance;
    [SerializeField] private Animator animatorHuman;
    [SerializeField] private Animator animatorCat;

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
        Animator animator = animatorHuman.gameObject.activeInHierarchy ? animatorHuman : animatorCat;
        animator.SetTrigger("Convert");
    }

    public void ResetAnimations()
    {
        Animator animator = animatorHuman.gameObject.activeInHierarchy ? animatorHuman : animatorCat;
        animator.SetFloat("Speed", 0);
        animator.SetBool("IsJump",false);
        animator.SetBool("IsFalling",false);
        animator.SetBool("IsDash",false);
        animator.SetBool("IsMoving",false);       
    }
}
