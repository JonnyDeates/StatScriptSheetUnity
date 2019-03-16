using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody rigidBody;
		Animator animator;
		
		float origGroundCheckDistance;
		const float half = 0.5f;
		float turnAround;
		float forwardAmount;
        Vector3 groundNormal;


        float capsuleHeight;
		Vector3 capsuleCenter;
		CapsuleCollider capsule;

        bool isGrounded;
        bool crouching;

        private float valueX;
        public GameObject playerModel;

        void Start()
		{
            animator = GetComponent<Animator>();
			rigidBody = GetComponent<Rigidbody>();
			capsule = GetComponent<CapsuleCollider>();
			capsuleHeight = capsule.height;
			capsuleCenter = capsule.center;

			rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			origGroundCheckDistance = m_GroundCheckDistance;
		}

         void Update()
        {
        }
 
        public void Move(Vector3 move, bool crouch, bool jump)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, groundNormal);
			turnAround = Mathf.Atan2(move.x, move.z);
			forwardAmount = move.z;

			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if (isGrounded)
			{
				HandleGroundedMovement(crouch, jump);
			}
			else
			{
				HandleAirborneMovement();
			}

			ScaleCapsuleForCrouching(crouch);
			PreventStandingInLowHeadroom();

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}


		void ScaleCapsuleForCrouching(bool crouch)
		{
			if (isGrounded && crouch)
			{
				if (crouching) return;
				capsule.height = capsule.height / 2f;
				capsule.center = capsule.center / 2f;
				crouching = true;
			}
			else
			{
				Ray crouchRay = new Ray(rigidBody.position + Vector3.up * capsule.radius * half, Vector3.up);
				float crouchRayLength = capsuleHeight - capsule.radius * half;
				if (Physics.SphereCast(crouchRay, capsule.radius * half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					crouching = true;
					return;
				}
				capsule.height = capsuleHeight;
				capsule.center = capsuleCenter;
				crouching = false;
			}
		}

		void PreventStandingInLowHeadroom()
		{
			// prevent standing up in crouch-only zones
			if (!crouching)
			{
				Ray crouchRay = new Ray(rigidBody.position + Vector3.up * capsule.radius * half, Vector3.up);
				float crouchRayLength = capsuleHeight - capsule.radius * half;
				if (Physics.SphereCast(crouchRay, capsule.radius * half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
				{
					crouching = true;
				}
			}
		}


		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
			animator.SetFloat("Turn", turnAround, 0.1f, Time.deltaTime);
			animator.SetBool("Crouch", crouching);
			animator.SetBool("OnGround", isGrounded);
			if (!isGrounded)
			{
				animator.SetFloat("Jump", rigidBody.velocity.y);
			}

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < half ? 1 : -1) * forwardAmount;
			if (isGrounded)
			{
				animator.SetFloat("JumpLeg", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (isGrounded && move.magnitude > 0)
			{
				animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				// don't use that while airborne
				animator.speed = 1;
			}
		}


		void HandleAirborneMovement()
		{
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			rigidBody.AddForce(extraGravityForce);

			m_GroundCheckDistance = rigidBody.velocity.y < 0 ? origGroundCheckDistance : 0.01f;
		}


		void HandleGroundedMovement(bool crouch, bool jump)
		{
			// check whether conditions are right to allow a jump:
			if (jump && !crouch && animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				// jump!
				rigidBody.velocity = new Vector3(rigidBody.velocity.x, m_JumpPower, rigidBody.velocity.z);
				isGrounded = false;
				animator.applyRootMotion = false;
				m_GroundCheckDistance = 0.1f;
			}
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, forwardAmount);
			transform.Rotate(0, turnAround * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (isGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = rigidBody.velocity.y;
				rigidBody.velocity = v;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				groundNormal = hitInfo.normal;
				isGrounded = true;
				animator.applyRootMotion = true;
			}
			else
			{
				isGrounded = false;
				groundNormal = Vector3.up;
				animator.applyRootMotion = false;
			}
		}
	}
}
