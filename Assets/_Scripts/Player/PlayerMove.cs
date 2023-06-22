using System;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
	[SerializeField] private new Rigidbody2D rigidbody2D;
	[SerializeField] private float runMaxSpeed;
	[SerializeField] private float runAccelAmount;
	[SerializeField] private float runDeccelAmount;
	private Vector2 _moveInput;
	
	private void Update()
	{
		GetInputsMove();

		if (_moveInput.x != 0 && PlayerProperties.instance.IsDynamicBody())
			Turn(_moveInput.x);
	}

	private void GetInputsMove()
	{
		if (GameManager.instance.DesactiveInputs) return;
		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");
	}

	private void FixedUpdate()
	{
		if (!PlayerDash.instance.IsDash) Run(1);
	}
	
    private void Run(float lerpAmount)
	{
		float targetSpeed = _moveInput.x * runMaxSpeed;
		
		targetSpeed = Mathf.Lerp(rigidbody2D.velocity.x, targetSpeed, lerpAmount);

		var accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;

		if(IsStop(targetSpeed))
		{
			accelRate = 0; 
		}

		float speedDif = targetSpeed - rigidbody2D.velocity.x;

		float movement = speedDif * accelRate;

		rigidbody2D.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

    private bool IsStop(float targetSpeed)
    {
	    return (Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Abs(targetSpeed) &&
	            Math.Abs(Mathf.Sign(rigidbody2D.velocity.x) - Mathf.Sign(targetSpeed)) < 0.1f &&
	            Mathf.Abs(targetSpeed) > 0.01f);
    }

	public void Turn(float inputX)
	{
		Quaternion rotation = transform.localRotation; 
		rotation.y = inputX > 0 ? 0 : 180;
		transform.localRotation = rotation;
	}
}
