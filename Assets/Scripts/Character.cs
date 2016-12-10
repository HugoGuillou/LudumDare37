using System;
using UnityEngine;


    public class Character : MonoBehaviour
    {
        [SerializeField] private float PlayerVelocity = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

        private bool m_Grounded; 
        private bool m_CanJump;         // Whether or not the player is grounded.          
        private bool m_CanDoubleJump;         // Whether or not the player is grounded.

        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        private Transform m_WallCheck;    // A position marking where to check if the player is grounded.
        //const Vector3 k_WallSize = new Vector3(0.2f, 0.2f,0); // Radius of the overlap circle to determine if grounded

        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private bool m_TouchWall = false;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_WallCheck = transform.Find("WallCheck");

            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            m_TouchWall = false;
            Debug.Log(m_GroundCheck.localScale.x * transform.localScale.x);
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders_floor = Physics2D.OverlapCircleAll(m_GroundCheck.position, Mathf.Abs(m_GroundCheck.localScale.x * transform.localScale.x), m_WhatIsGround);
            for (int i = 0; i < colliders_floor.Length; i++)
            {
                if (colliders_floor[i].gameObject != gameObject)
                {
                   
                    m_Grounded = true;
                    m_CanDoubleJump = true;
                }
            }

            Collider2D[] colliders_wall  = Physics2D.OverlapBoxAll(m_WallCheck.position, Vector3.Scale(m_WallCheck.localScale, transform.localScale), m_WhatIsGround);
            for (int i = 0; i < colliders_wall.Length; i++)
            {
                if(colliders_wall[i].gameObject != gameObject && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
                {
                    m_TouchWall = true;
                    m_CanDoubleJump = false;
                }
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }


            
   

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));


                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*PlayerVelocity, m_Rigidbody2D.velocity.y);

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
            }
            // If the player should jump...
            if ((m_Grounded || m_TouchWall) && jump )
            {
                // Add a vertical force to the player.
                Debug.Log("Jump");
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }

            // Wall Grab
            else if(!m_Grounded && m_TouchWall)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                Debug.Log("Grab");

                if(jump)
                {
                    Debug.Log("WallJump");
                    m_Rigidbody2D.AddForce(new Vector2(m_JumpForce, m_JumpForce));
                }
            }

            //Double Jump
            else if(!m_Grounded && !m_TouchWall && jump && m_CanDoubleJump)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                Vector3 new_vel = new Vector3(m_Rigidbody2D.velocity.x, 0);
                m_Rigidbody2D.velocity = new_vel;
                m_CanDoubleJump = false;
                Debug.Log("double jump");
            }

            Debug.Log("Ground " + m_Grounded);



        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 ground_transform_pos = transform.Find("GroundCheck").position; 
            float ground_transform_scale = transform.Find("GroundCheck").localScale.x * transform.localScale.x;
            Gizmos.DrawWireSphere(ground_transform_pos, ground_transform_scale);

            Vector3 wall_check_pos = transform.Find("WallCheck").position;
            Vector3 wall_check_scale = Vector3.Scale(transform.Find("WallCheck").localScale,transform.localScale);
            Gizmos.DrawWireCube(wall_check_pos, wall_check_scale);
            print(transform.Find("GroundCheck").localScale.x * transform.localScale.x);
            


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
