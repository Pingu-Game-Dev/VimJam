using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	//These fields are for air control by momentum implementation
	public float airMoveSpeed = 0.8f;
	private float airSpeed;
	public float maxVelX = 30f;

	// These fields are for wall jumping
	public Transform m_wallCheck;
	public Transform m_wallCheck_Back;
	private bool wall = false;
	private bool wallBack = false;
	private float timer = 0f;
	private bool timerOn = false;

	// Animations
	public Animator animator;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		airSpeed = airMoveSpeed;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	void Update()
	{
		if (timerOn){
			timer += Time.deltaTime;
		}

		if (timer > .1f){
			timer = 0f;
			timerOn = false;
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		wall = false;
		wallBack = false;
		animator.SetFloat("Speed", m_Rigidbody2D.velocity.x * m_Rigidbody2D.velocity.x);
		animator.SetBool("Grounded", m_Grounded);
		// If crouching, check to  see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		// If the input is moving the player right and the player is facing left...
		if (move > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (move < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}

		// Air control by momentum
		if (!m_Grounded && !timerOn)
		{
			//MOVE LEFT
			if (move < 0f && m_Rigidbody2D.velocity.x > -maxVelX){
				m_Rigidbody2D.AddForce(new Vector2((move*airSpeed)/Time.fixedDeltaTime,0f));
			}
			//MOVE RIGHT
			else if (move > 0f && m_Rigidbody2D.velocity.x < maxVelX){
				m_Rigidbody2D.AddForce(new Vector2((move*airSpeed)/Time.fixedDeltaTime,0f));
			}

			//Checking for wall collision
			if (Physics2D.OverlapCircle(m_wallCheck.position, .2f, m_WhatIsGround))
			{
				wall = true;
			}
			else if (Physics2D.OverlapCircle(m_wallCheck_Back.position, .6f, m_WhatIsGround)){
				wallBack = true;
			}
		}


		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			airSpeed = airMoveSpeed;

			// If crouching
			if (crouch)
			{
				
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

		//wall slide
		if ((wall || wallBack) && !jump && !(wall &&wallBack)){
			if (m_Rigidbody2D.velocity.y < -60f){
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x , -60f);
			}
		}

		//wall Jump
		if ((wall || wallBack) && jump && !(wall &&wallBack)){
			
			airSpeed = airMoveSpeed * 0.5f;
			m_Rigidbody2D.velocity = new Vector2(0f,0f);
			
			if (m_FacingRight && wall){
				m_Rigidbody2D.AddForce(new Vector2((1f/2f)*-m_JumpForce,  m_JumpForce));
				Flip();
				//Debug.Log("FRONT - RIGHT");
			}
			else if (m_FacingRight &&wallBack){
				m_Rigidbody2D.AddForce(new Vector2((1f/2f)*m_JumpForce,  m_JumpForce));
				//Debug.Log("BACK - LEFT");
			}
			else if (!m_FacingRight && wallBack){
				m_Rigidbody2D.AddForce(new Vector2((1f/2f)*-m_JumpForce,  m_JumpForce));
				//Debug.Log("BACK - RIGHT");
			}
			else if (!m_FacingRight && wall){
				m_Rigidbody2D.AddForce(new Vector2((1f/2f)*m_JumpForce,  m_JumpForce));
				Flip();
				//Debug.Log("FRONT - LEFT");
			}
			
			wall = false;
			wallBack = false;
			timerOn = true;
		}

	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
