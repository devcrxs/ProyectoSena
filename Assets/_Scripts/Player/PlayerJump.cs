using UnityEngine;
public class PlayerJump : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private KeyCode keyJump;
	[SerializeField] private float coyoteTime;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCutGravityMult;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float jumpHangTimeThreshold;
    [SerializeField] private float jumpHangGravityMult;
    [SerializeField] private float fallGravityMult;
    [SerializeField] private float jumpInputBufferTime;
    private bool _isJumping;
    private float _lastOnGroundTime;
    private bool _isJumpCut;
    private float _lastPressedJumpTime;
	private bool _isFalling;
	public bool IsJumping => _isJumping;
	public bool IsFalling => _isFalling;
	public static PlayerJump instance;

	private void Awake()
	{
		if (instance == null) instance = this;
	}

	private void Update()
    {
	    _lastOnGroundTime -= Time.deltaTime;
	    _lastPressedJumpTime -= Time.deltaTime;
	    _isFalling = rb.velocity.y < 0;

	    if (IsDownJumpInput())
	    {
		    OnJumpInput();
	    }
	    if (IsUpJumpInput())
	    {
		    OnJumpUpInput();
	    }
	    CheckGround();
	    
	    if (_isJumping && rb.velocity.y < 0)
	    {
		    _isJumping = false;
		    
	    }
	    if (_lastOnGroundTime > 0 && !_isJumping)
	    {
		    _isJumpCut = false;
	    }

	    if (!PlayerDash.instance.IsDash)
	    {
		    if ( CanJump() && _lastPressedJumpTime > 0.05f)
		    {
			    _isJumping = true;
			
			    _isJumpCut = false;
			    Jump();
		    }
		    else if (_lastPressedJumpTime > 0)
		    {
			    _isJumping = false;
			    _isJumpCut = false;
		    }
	    }
	    GravityJumps();
	}

	private void GravityJumps()
	{
		if (!PlayerProperties.instance.IsDynamicBody()) return;
		if (_isJumpCut)
		{
			PlayerProperties.instance.SetGravityScale(PlayerProperties.instance.Gravity * jumpCutGravityMult);
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
		}
		else if ((_isJumping || _isFalling) && Mathf.Abs(rb.velocity.y) < jumpHangTimeThreshold)
		{
			PlayerProperties.instance.SetGravityScale(PlayerProperties.instance.Gravity * jumpHangGravityMult);
		}
		else if (rb.velocity.y < 0)
		{
			PlayerProperties.instance.SetGravityScale(PlayerProperties.instance.Gravity * fallGravityMult);
			rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
		}
		else
		{
			PlayerProperties.instance.SetGravityScale(PlayerProperties.instance.Gravity);
		}
	}

	private void CheckGround()
	{
		if (_isJumping || PlayerDash.instance.IsDash) return;
		if (!PlayerProperties.instance.IsTouchGround() || _isJumping) return;
		_lastOnGroundTime = coyoteTime;
		PlayerDash.instance.CanDash = true;
	}

	private bool IsDownJumpInput()
	{
		return CanJump() && Input.GetKeyDown(keyJump) && PlayerProperties.instance.IsDynamicBody() && !GameManager.instance.DesactiveInputs;
	}

	private bool IsUpJumpInput()
	{
		return Input.GetKeyUp(keyJump) && PlayerProperties.instance.IsDynamicBody() && !GameManager.instance.DesactiveInputs;
	}

	private void OnJumpInput()
	{
		_lastPressedJumpTime = jumpInputBufferTime;
	}

    private void OnJumpUpInput()
	{
		if (CanJumpCut())
			_isJumpCut = true;
	}

    private void Jump()
	{
		PlayerEffects.instance.PlayJumpEffect();
		_lastPressedJumpTime = 0;
		_lastOnGroundTime = 0;
		
		float force = jumpForce;
		if (rb.velocity.y < 0)
			force -= rb.velocity.y;

		rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

	}

    private bool CanJump()
    {
		return _lastOnGroundTime > 0 && !_isJumping;
    }
    
	private bool CanJumpCut()
    {
		return _isJumping && rb.velocity.y > 0;
    }
}
