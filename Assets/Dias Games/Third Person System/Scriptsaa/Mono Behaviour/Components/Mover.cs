using System;
using UnityEngine;
using DiasGames.Components;

namespace DiasGames.Components
{
    public class Mover : MonoBehaviour, IMover, ICapsule
    {
		[Header("Player")]
		[Tooltip("How fast the character turns to face movement direction")]
		[Range(0.0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;
		public bool _useCameraOrientation = true;


		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Gravity")]
		[Tooltip("Should apply gravity?")]
		[SerializeField] private bool UseGravity = true;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		[SerializeField] private float Gravity = -15.0f;

		[Header("Run")]
		public bool isRun =false;

		[Header("Swim")]
		float submergence;
		[SerializeField] private bool Inwater => submergence > 0f;
		[SerializeField] private bool Swimming => submergence >= swimThreshold;
		[SerializeField] private float Gravityinwater = -5f;
		[SerializeField] private Transform chestT;
		[SerializeField]
		float submergenceOffset = 0.5f;
		[SerializeField, Min(0f)]
		float buoyancy = 1f;

		[SerializeField, Min(0.1f)]
		float submergenceRange = 1f;
		[SerializeField, Range(0f, 10f)]
		float waterDrag = 1f;
		[SerializeField, Range(0.01f, 1f)]
		float swimThreshold = 0.5f;
		RaycastHit hitinfo;
		public LayerMask waterMask;
		[SerializeField] private float speedSwim = 1.2f;
		[SerializeField, Range(0f, 100f)]
		float
		maxAcceleration = 10f,
		maxAirAcceleration = 1f,maxSwimAcceleration = 5f;
		[SerializeField, Range(0f, 100f)]
		float maxSpeed = 10f, maxClimbSpeed = 4f, maxSwimSpeed = 5f;

		// player
		private float _speed;
		private float _animationBlend;
		private float _targetRotation = 0.0f;
		private float _rotationVelocity;
		private float _terminalVelocity = 53.0f;
		private float _initialCapsuleHeight = 2f;
		private float _initialCapsuleRadius = 0.28f;
		private float walkspeed = 2.65f;
		private float sprintspeed = 5.26f;


		// variables for root motion
		private bool _useRootMotion = false;
		private Vector3 _rootMotionMultiplier = Vector3.one;
		private bool _useRotationRootMotion = true;

		// animation IDs
		private int _animIDSpeed;
		private int _animIDMotionSpeed;

		private Animator _animator;
		private CharacterController _controller;
		private GameObject _mainCamera;
		

		private bool _hasAnimator;

		// controls character velocity
		private Vector3 _velocity;
		private float _timeoutToResetVars = 0;
		Vector3 upAxis;
		LayerMask probeMask = -1;
		int stepsSinceLastGrounded, stepsSinceLastJump;
		Vector3 connectionVelocity;
		Vector3 UpDown;
		float acceleration, speed;

		//add Stamina
		[HideInInspector] public StaminaController _staminaController;

		private void Awake()
        {
			_staminaController = GetComponent<StaminaController>();
			_mainCamera = Camera.main.gameObject;
			_controller = GetComponent<CharacterController>();
			_initialCapsuleHeight = _controller.height;
			_initialCapsuleRadius = _controller.radius;
		}

        private void Start()
        {
			_hasAnimator = TryGetComponent(out _animator);
			AssignAnimationIDs();
        }

		private void Update()
		{
			GravityControl();
			GroundedCheck();

            


			if (Input.GetButtonDown("Sprint"))
			{
				isRun = true;
            }
			else if (Input.GetButtonUp("Sprint"))
            {
				isRun = false;
			}
			Debug.Log(isRun);
			if (Inwater)
            {
				UpDown = new Vector3();
				UpDown.y = Swimming ? Input.GetAxis("updown") : 0f;
				//Debug.Log(UpDown.y);
				//_controller.Move(new Vector3(0.0f, UpDown.y, 0.0f));
            }

			if (_timeoutToResetVars <= 0)
			{
				_speed = 0;
				_animationBlend = 0;
				_animator.SetFloat(_animIDSpeed, 0);
				_timeoutToResetVars = 0;
			}
			else
				_timeoutToResetVars -= Time.deltaTime;

			if (_useRootMotion)
				return;

			if (!_controller.enabled) return;

			if (UpDown.y != 0)
			{
				Debug.Log(UpDown.y);
				_controller.Move(new Vector3(0.0f, UpDown.y * speedSwim, 0.0f) * Time.deltaTime);
			}

			_controller.Move(_velocity * Time.deltaTime);

            if (!isRun)
            {
				_staminaController.weAreSprinting = false;
            }
		}

		private void FixedUpdate()
		{
			
			//UpDown = new Vector3();
			//UpDown.y = Swimming ? Input.GetAxis("updown") : 0f;
			if (Inwater)
			{
				_velocity.y *= 1f - waterDrag * submergence * Time.deltaTime;
			}

			if (Inwater)
			{
				_velocity.y +=
					Gravityinwater * ((1f - buoyancy * submergence) * Time.deltaTime);
			}
			
			ClearState();
		}

		/*void AdjustVelocity()
		{
			
			Vector3 xAxis, zAxis;
			if (Inwater)
			{
				float swimFactor = Mathf.Min(1f, submergence / swimThreshold);
				acceleration = Mathf.LerpUnclamped(
					Grounded ? maxAcceleration : maxAirAcceleration,
					maxSwimAcceleration, swimFactor
				);
				speed = Mathf.LerpUnclamped(maxSpeed, maxSwimSpeed, swimFactor);
				//xAxis = rightAxis;
				//zAxis = forwardAxis;
			}
			Vector3 relativeVelocity = _velocity - connectionVelocity;
			//float currentX = Vector3.Dot(relativeVelocity, xAxis);
			//float currentZ = Vector3.Dot(relativeVelocity, zAxis);

			float maxSpeedChange = acceleration * Time.deltaTime;

			//_velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);

			if (Swimming)
			{
				float currentY = Vector3.Dot(relativeVelocity, upAxis);
				float newY = Mathf.MoveTowards(
					currentY, UpDown.y * speed, maxSpeedChange
				);
				_velocity += upAxis * (newY - currentY);
			}
		}*/
		void UpdateState()
		{
			stepsSinceLastGrounded += 1;
			stepsSinceLastJump += 1;
			//velocity = body.velocity;
			if ( CheckSwimming() || SnapToGround())
            {

            }		
		}

		bool SnapToGround()
		{
			if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
			{
				return false;
			}
			if (!Physics.Raycast(this.transform.position, -upAxis, out RaycastHit hit, 0.5f, probeMask, QueryTriggerInteraction.Ignore))
			{
				return false;
			}
			return true;
		}

		bool CheckSwimming()
		{
			if (Swimming)
			{
				//groundContactCount = 0;
				//contactNormal = upAxis;
				return true;
			}
			return false;
		}

		void ClearState()
		{
			connectionVelocity = Vector3.zero;
			submergence = 0f;
		}

		void EvaluateSubmergence()
		{
			if (Physics.Raycast(this.transform.position + upAxis * submergenceOffset,	-upAxis, out RaycastHit hit, submergenceRange + 1f,
			waterMask, QueryTriggerInteraction.Collide))
			{
				submergence = 1f - hit.distance / submergenceRange;
			}
			else
			{
				submergence = 1f;
			}
		}
		void OnTriggerEnter(Collider other)
		{
			if ((waterMask & (1 << other.gameObject.layer)) != 0)
			{
				Grounded = false;
				UseGravity = false;
				EvaluateSubmergence();
				_animator.CrossFadeInFixedTime("swim.swim idle", 0.1f);
				Debug.Log(Inwater);
			}
		}

		void OnTriggerStay(Collider other)
		{
			if ((waterMask & (1 << other.gameObject.layer)) != 0 && Inwater == true)
			{
				UseGravity = false;
				Grounded = false;
				//Inwater = true;
				EvaluateSubmergence();
				Debug.Log("stay");
			}
		}
		void OnTriggerExit(Collider other)
		{
			if ((waterMask & (1 << other.gameObject.layer)) != 0 && Inwater == false)
			{
				UseGravity = true;
				
			}
		}
		private void OnAnimatorMove()
        {
			if (!_useRootMotion) return;

			if (_controller.enabled)
				_controller.Move(_animator.deltaPosition);
			else
				_animator.ApplyBuiltinRootMotion();

			transform.rotation *= _animator.deltaRotation;
        }

        private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDMotionSpeed = Animator.StringToHash("Motion Speed");
		}

		private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, _controller.radius, GroundLayers, QueryTriggerInteraction.Ignore);

            if (!Grounded && !_controller.isGrounded ) return;

            Depenetrate();
        }

        private void Depenetrate()
        {
			if (!_controller.enabled) return;

			// first check if there is a possible ground in all grounds
			RaycastHit[] hits = Physics.SphereCastAll(transform.position + Vector3.up, _controller.radius, Vector3.down,
				1 - GroundedOffset, Physics.AllLayers, QueryTriggerInteraction.Ignore);

			foreach (RaycastHit h in hits)
			{
				if (h.distance != 0 && Vector3.Dot(h.normal, Vector3.up) > 0.7f)
					return;
			}

			// if not depenetrate char
			RaycastHit hit;
            if (Physics.SphereCast(transform.position + Vector3.up, _controller.radius, Vector3.down,
                out hit, 1 - GroundedOffset, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Vector3.Dot(hit.normal, Vector3.up) < 0.5f)
                {
                    Grounded = false;
                    Vector3 direction = hit.normal;
                    direction.y = -1;
                    _controller.Move(direction.normalized * _controller.skinWidth*3);
                }
            }
        }

        public Collider GetGroundCollider()
        {
			if (!Grounded) return null;

			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Collider[] grounds = Physics.OverlapSphere(spherePosition, _controller.radius, GroundLayers, QueryTriggerInteraction.Ignore);
			
			if (grounds.Length > 0)
				return grounds[0];

			return null;
        }

		public void Move(Vector2 moveInput, float targetSpeed, bool rotateCharacter = true)
		{
            if (Inwater)
            {
				Move(moveInput, speedSwim, _mainCamera.transform.rotation, rotateCharacter);
			}
			else if (isRun)
            {
				Move(moveInput, sprintspeed, _mainCamera.transform.rotation, rotateCharacter);
				_staminaController.Sprinting();
			}
            else 
            {
				Move(moveInput, walkspeed, _mainCamera.transform.rotation, rotateCharacter);
			}
		}

		public void Move(Vector2 moveInput, float targetSpeed, Quaternion cameraRotation, bool rotateCharacter = true)
        {
			targetSpeed = Inwater ? speedSwim : targetSpeed;

			Debug.Log(targetSpeed);
			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (moveInput == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = moveInput.magnitude; // _input.analogMovement ? _input.move.magnitude : 1f;

			if (inputMagnitude > 1)
				inputMagnitude = 1f;
			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed * inputMagnitude;
			}
			
			_animationBlend = Mathf.Lerp(_animationBlend, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

			// normalise input direction
			Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (moveInput != Vector2.zero)
			{
				_targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + (_useCameraOrientation ? cameraRotation.eulerAngles.y : 0);
				float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

				// rotate to face input direction relative to camera position
				if (rotateCharacter)
					transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
			}

			// update animator if using character
			if (_hasAnimator)
			{
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
			}

			Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
			_velocity = targetDirection.normalized * _speed + new Vector3(0.0f, _velocity.y, 0.0f);
			_timeoutToResetVars = 0.5f;

			if (Inwater==true)
			{
				Debug.Log("MOveMOvemoeve");
				/*_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
							 new Vector3(_velocity.x, 0.0f, _velocity.z) * Time.deltaTime);*/
                if (Input.GetKeyDown(KeyCode.Space))
                {
					_velocity *= -1f;
					_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
							 new Vector3(0.0f, _velocity.y, 0.0f) * Time.deltaTime);
					Debug.Log(_controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
							 new Vector3(0.0f, _velocity.y, 0.0f) * Time.deltaTime));
				}
			}

			
		}


		public void Move(Vector3 velocity)
        {
			Vector3 newVelocity = velocity;
			if (UseGravity == true)
				newVelocity.y = _velocity.y;

			/*if (Inwater)
			{
				_velocity.y +=
					Gravityinwater * ((1f - buoyancy * submergence) * Time.deltaTime);
				newVelocity.y = _velocity.y;
			}*/

			_velocity = newVelocity;
        }

		private void GravityControl()
		{
			Gravity =  Inwater? Gravityinwater : -15f;
			//Debug.Log(Gravity);
			
            /*if (InWater)
			{
				velocity *= 1f - waterDrag * submergence * Time.deltaTime;
			}else if(Inwater)
			{
				_velocity.y +=
					Gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
			}*/
            if (UseGravity == true)
            {
				if (_controller.isGrounded)
				{
					//Debug.Log("con.gro");
					// stop our velocity dropping infinitely when grounded
					if (_velocity.y < 2.0f)
					{
						_velocity.y = -5f;
					}
				}
			}
			

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (UseGravity == true && _velocity.y < _terminalVelocity )
			{
				//Debug.Log("usegra");
				//Debug.Log(UseGravity);
				_velocity.y += Gravity * Time.deltaTime;
			}

		}

		/// <summary>
		/// Get rotation to face desired direction
		/// </summary>
		/// <returns></returns>
		public Quaternion GetRotationFromDirection(Vector3 direction)
		{
			float yaw = Mathf.Atan2(direction.x, direction.z);
			return Quaternion.Euler(0, yaw * Mathf.Rad2Deg, 0);
		}

		/// <summary>
		/// Sets character new position
		/// </summary>
		/// <param name="newPosition"></param>
		public void SetPosition(Vector3 newPosition)
        {
			bool currentEnable = _controller.enabled;

			_controller.enabled = false;
			transform.position = newPosition;
			_controller.enabled = currentEnable;
        }

		public void DisableCollision()
		{
			_controller.enabled = false;
		}

		public void EnableCollision()
		{
			_controller.enabled = true;
		}

		public void SetCapsuleSize(float newHeight, float newRadius)
		{
			if (newRadius > newHeight * 0.5f)
				newRadius = newHeight * 0.5f;

			_controller.radius = newRadius;
			_controller.height = newHeight;
			_controller.center = new Vector3(0, newHeight * 0.5f, 0);
		}

		public void ResetCapsuleSize()
        {
			SetCapsuleSize(_initialCapsuleHeight, _initialCapsuleRadius);
        }

        public void SetVelocity(Vector3 velocity)
        {
            this._velocity = velocity;
        }

        public Vector3 GetVelocity()
        {
			return _velocity;
        }

        public float GetGravity()
        {
            return Gravity;
        }

        public void ApplyRootMotion(Vector3 multiplier, bool applyRotation = false)
        {
			_useRootMotion = true;
			_rootMotionMultiplier = multiplier;
			_useRotationRootMotion = applyRotation;
        }

        public void StopRootMotion()
        {
			_useRootMotion = false;
			_useRotationRootMotion = false;
		}

        public float GetCapsuleHeight()
        {
			return _controller.height;
        }

        public float GetCapsuleRadius()
        {
			return _controller.radius;
        }

        public void EnableGravity()
        {
			UseGravity = true;
        }

        public void DisableGravity()
        {
			UseGravity = false;
		}

        bool IMover.IsGrounded()
        {
            return Grounded;
        }

        public void StopMovement()
        {
			_velocity = Vector3.zero;
			_speed = 0; 
			
			_animator.SetFloat(_animIDSpeed, 0);
			_animator.SetFloat(_animIDMotionSpeed, 0);
		}

		public Vector3 GetRelativeInput(Vector2 input)
        {
			Vector3 relative = _mainCamera.transform.right * input.x + 
				Vector3.Scale(_mainCamera.transform.forward, new Vector3(1, 0, 1)) * input.y;

			return relative;
        }

        bool IMover.isRun()
        {
			return isRun;
        }

        /*bool IMover.Inwater()
        {
			return Inwater;
        }*/
    }
}