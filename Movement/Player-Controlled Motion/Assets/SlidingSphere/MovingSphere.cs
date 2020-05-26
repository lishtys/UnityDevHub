using UnityEngine;

namespace SlidingSphere
{
    public class MovingSphere : MonoBehaviour
    {
        [SerializeField,Range(0f,100f)]
        private float maxSpeed = 10f;
        private Vector3 velocity;
        [SerializeField, Range(0f, 100f)]
        float maxAcceleration = 10f;
        [SerializeField, Range(0f, 1f)]
        float bounciness = 0.5f;
        
        [SerializeField]
        Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
        
        
        // Update is called once per frame
        void Update()
        {
            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");
            playerInput =Vector2.ClampMagnitude(playerInput,1f);
            var desiredVelocity=new Vector3(playerInput.x,0,playerInput.y)*maxSpeed;
            float maxSpeedChange = maxAcceleration*Time.deltaTime;
            velocity.x= Mathf.MoveTowards(velocity.x,desiredVelocity.x, maxSpeedChange);
            velocity.z= Mathf.MoveTowards(velocity.z,desiredVelocity.z, maxSpeedChange);
            var displacement = velocity * Time.deltaTime;
            Vector3 newPosition = transform.localPosition + displacement;
          
            // Clamp
            if (newPosition.x < allowedArea.xMin) {
                newPosition.x = allowedArea.xMin;
                velocity.x = -velocity.x * bounciness;
            }
            else if (newPosition.x > allowedArea.xMax) {
                newPosition.x = allowedArea.xMax;
                velocity.x = -velocity.x * bounciness;
            }
            if (newPosition.z < allowedArea.yMin) {
                newPosition.z = allowedArea.yMin;
                velocity.z = -velocity.z * bounciness;
            }
            else if (newPosition.z > allowedArea.yMax) {
                newPosition.z = allowedArea.yMax;
                velocity.z = -velocity.z * bounciness;
            }
            transform.localPosition = newPosition;

        }
    }
}
