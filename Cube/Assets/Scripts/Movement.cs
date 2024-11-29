using Unity.Netcode;
using UnityEngine;

public class Movement : NetworkBehaviour
{
	[SerializeField] Camera _camera;
	[SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
	[SerializeField] bool cursorLock = true;
	[SerializeField] float mouseSensitivity = 3.5f;
	[SerializeField] float Speed = 6.0f;
	[SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
	[SerializeField] float gravity = -30f;
	[SerializeField] Transform groundCheck;
	[SerializeField] LayerMask ground;
	[SerializeField] Animator animator;

	public float jumpHeight = 6f;
	float velocityY;
	bool isGrounded;

	float cameraCap;
	Vector2 currentMouseDelta;
	Vector2 currentMouseDeltaVelocity;

	CharacterController controller;
	Vector2 currentDir;
	Vector2 currentDirVelocity;
	
	void Start()
	{
		controller = GetComponent<CharacterController>();

		if (cursorLock)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = true;
		}
	}

	void Update()
	{
		if (!IsOwner) return;
		UpdateMouse();
		UpdateMove();
		UpdateAnimation();
	}	

	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
		if (!IsOwner) return;  
		_camera.enabled = true; 
	}

	void UpdateMouse()
	{
		Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

		cameraCap -= currentMouseDelta.y * mouseSensitivity;

		cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

		_camera.transform.localEulerAngles = Vector3.right * cameraCap;

		transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
	}

	void UpdateMove()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

		Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		targetDir.Normalize();

		currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

		velocityY += gravity * 2f * Time.deltaTime;

		Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * Speed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);

		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

		if (isGrounded! && controller.velocity.y < -1f)
		{
			velocityY = -8f;
		}
	}

	void UpdateAnimation()
	{
		bool isWalking = currentDir.magnitude > 0.01f;

		animator.SetBool("isWalking", isWalking);
	}
}
