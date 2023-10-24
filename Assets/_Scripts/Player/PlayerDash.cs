using System.Collections;
using UnityEngine;
public class PlayerDash : MonoBehaviour
{
   [SerializeField] private new Rigidbody2D rigidbody2D;
   [SerializeField] private KeyCode keyDash;
   [SerializeField] private float dashTime;
   [SerializeField] private float forceDash;
   [SerializeField] private float dashEndSpeed;
   private bool _isDash;
   private bool _canDash;
   private bool _dashAnimation;
   private float forceDashDefault;
   private Vector2 _directionDash;
   public static PlayerDash instance;
   public bool DashAnimation => _dashAnimation;
   public bool IsDash => _isDash;
   public bool CanDash
   {
      set => _canDash = value;
   }
   private void Awake()
   {
      if (instance == null) instance = this;
   }

   private void Start()
   {
      _canDash = true;
      forceDashDefault = forceDash;
   }

   private void Update()
   {
      if (Input.GetKeyDown(keyDash) && _canDash && !GameManager.instance.DesactiveInputs)
      {
         _isDash = true;
         _canDash = false;
         _dashAnimation = true;
         _directionDash.x = Input.GetAxisRaw("Horizontal");
         _directionDash.y = Input.GetAxisRaw("Vertical");
         ShockWaveManager.instance.CallShockWave();
         PlayerEffects.instance.StartTrailGhost();
         StartCoroutine(CameraManager.instance.CameraShakeRecursive(2.5f, 0.2f));
         if (_directionDash == Vector2.zero)
         {
            float directionDash = transform.localRotation.y == 0 ? 1 : -1;
            _directionDash = new Vector2(directionDash, 0);
         }

         if (_directionDash.y == 0)
         {
            forceDash = 40;
         }
         else
         {
            forceDash = forceDashDefault;
         }
         StartCoroutine(StopDash());
      }
      Dash();
   }

   private void Dash()
   {
      if (!_isDash || !PlayerProperties.instance.IsDynamicBody()) return;
      PlayerProperties.instance.SetGravityScale(0);
      rigidbody2D.velocity = _directionDash.normalized * forceDash;
   }

   private IEnumerator StopDash()
   {
      yield return new WaitForSeconds(dashTime);
      _isDash = false;
      PlayerProperties.instance.SetGravityScale(PlayerProperties.instance.Gravity);
      rigidbody2D.velocity = dashEndSpeed * _directionDash.normalized;
      while (rigidbody2D.velocity.y > 0)
      {
         yield return null;
      }
      _dashAnimation = false;
   }
}
