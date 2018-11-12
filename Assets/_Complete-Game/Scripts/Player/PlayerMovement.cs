using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    
    public class PlayerMovement : MonoBehaviour
    {
          public GameObject armaActual;
          GameObject m;

            GameObject y;


        public Transform[] hijos;
        
         /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>


        void OnTriggerEnter(Collider other)
        {
             if (other.gameObject.CompareTag ("arma"))
        {
        // GameObject.FindGameObjectWithTag ("miArma") = other.gameObject;
                print("nueva arma"+ movimientoArma.instance.danio);
        Destroy(m);
         
        m =Instantiate(other.gameObject);
        m.transform.tag = "miArma";
        m.transform.parent =transform;
        m.transform.position = transform.position;
             m.transform.rotation= transform.rotation;

         }
        }

        
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            hijos = GetComponentsInChildren<Transform> ();
            
        }

        public float speed = 6f;            // The speed that the player will move at.


        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
#if !MOBILE_INPUT
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
#endif

        void Awake ()
        {
		m =Instantiate(armaActual);

        m.transform.parent =transform;
#if !MOBILE_INPUT
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask ("Floor");
#endif

            // Set up references.
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
        }


        void FixedUpdate ()
        {

            // Store the input axes.
            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
      
            if(movimientoArma.instance.tipo == "m60"){
                   m.transform.position =new Vector3(transform.position.x+ .0f,transform.position.y,transform.position.z);
                    m.transform.eulerAngles= new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+ 90f,transform.eulerAngles.z);
            }
            else{
                  m.transform.position =transform.position;
                  m.transform.rotation= transform.rotation;
            }
            // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            Turning ();

            // Animate the player.
            Animating (h, v);
            
        }


        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set (h, 0f, v);
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition (transform.position + movement);

        }


        void Turning ()
        {
#if !MOBILE_INPUT
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation (playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation (newRotatation);
            }
#else

            Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }
#endif
        }


        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}