using System;
using UnityEngine;

namespace SlidingSphere
{
    public class MovingSphere : MonoBehaviour
    {
        [SerializeField,Range(0f,100f)]
        private float maxSpeed = 10f;

        private Vector3 velocity, desiredVelocity;
        [SerializeField, Range(0f, 100f)]
        float maxAcceleration = 10f, maxAirAcceleration = 1f;
        [Header("Jump")]
        private bool desiredJump;
        [SerializeField, Range(0f, 10f)]
        float jumpHeight = 2f;
        [SerializeField, Range(0, 5)]
        int maxAirJumps = 0;
        [SerializeField,Range(0,5)]
        private int jumpPhase;
        
        [Header("Physics")]
        private Rigidbody body;

        [Header("Status")] private bool onGround;

        
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }


        // Update is called once per frame
        void Update()
        {
            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput =Vector2.ClampMagnitude(playerInput,1f);
            desiredVelocity=new Vector3(playerInput.x,0,playerInput.y)*maxSpeed;
            desiredJump |= Input.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            UpdateState();
            float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
            float maxSpeedChange = acceleration*Time.deltaTime;
            velocity.x= Mathf.MoveTowards(velocity.x,desiredVelocity.x, maxSpeedChange);
            velocity.z= Mathf.MoveTowards(velocity.z,desiredVelocity.z, maxSpeedChange);
            if (desiredJump)
            {
                desiredJump = false;
                Jump();
            }
            
            
            onGround = false;
            body.velocity=velocity;
        }

        void UpdateState()
        {
            velocity = body.velocity;
            if (onGround)
            {
                jumpPhase = 0;
            }
        }

        private void Jump()
        {
            // based on Newton's law
            if (onGround || jumpPhase < maxAirJumps)
            {
                jumpPhase++;
                float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
                if (velocity.y > 0f) {
                    jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
                }
                velocity.y += jumpSpeed;
            }
        }

         void OnCollisionEnter(Collision other)
         {
             if(onGround)
                 onGround = true;
             
             EvaluateCollision(other);
         }
         void OnCollisionStay (Collision other) {
             onGround = true;
         }

         void EvaluateCollision(Collision collision)
         {
             for (int i = 0; i < collision.contactCount; i++) {
                 Vector3 normal = collision.GetContact(i).normal;
                 onGround |= normal.y>=0.9f;
             }
         }
         
         
    }
}
