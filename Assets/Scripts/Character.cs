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
        private bool m_CanWallJump;         // Whether or not the player is grounded.
        private bool m_CanClimbCeil;         // Whether or not the player is grounded.

        private bool m_HasTouchedCeiling;

        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up

        private Transform m_WallCheck;    // A position marking where to check if the player is grounded.
        //const Vector3 k_WallSize = new Vector3(0.2f, 0.2f,0); // Radius of the overlap circle to determine if grounded

        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        private bool m_TouchWall = false;
        private bool m_TouchCeil = false;

        private bool walljumping = false;
        public float wallJumpForce = 5;
        private float walljump_coeff = 0;
        private bool wall_push;

        private bool detecting_time = false;
        const float detect_time = 0.2f;
        private float detect_time_left;

        private bool detecting_ceil_time = false;
        const float detect_ceil_time = 0.3f;
        private float detect_ceil_time_left;

        private bool disable_input = false;
        const float disable_time = 0.1f;
        private float disable_time_left;

        private bool m_OnPlatform;
        private Rigidbody2D m_CurrentPlatform;

        float lastMove = 0;

        private void Start()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_WallCheck = transform.Find("WallCheck");
        }

        private void Awake()
        {
            // Setting up references.
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            detect_time_left = detect_time;
            disable_time_left = disable_time;
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            m_TouchWall = false;

            m_OnPlatform = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
                      
                Collider2D[] colliders_floor = Physics2D.OverlapCircleAll(m_GroundCheck.position, Mathf.Abs(m_GroundCheck.localScale.x * transform.localScale.x), m_WhatIsGround);
                for (int i = 0; i < colliders_floor.Length; i++)
                {
                    if (colliders_floor[i].gameObject != gameObject)
                    {
                        if(colliders_floor[i].gameObject.tag == "MovingPlatorm")
                        {
                            m_OnPlatform = true;
                            m_CurrentPlatform = colliders_floor[i].transform.parent.GetComponent<Rigidbody2D>();

                        }
                        m_Grounded = true;
                        m_CanDoubleJump = true;

                        m_CanWallJump = false;

                    }
                }

                if(!m_OnPlatform)
                {
                    m_CurrentPlatform = null;
                }

            if(detecting_time)
            {
                detect_time_left -= Time.deltaTime;
                m_TouchWall = false;
               
                if(detect_time_left < 0)
                {
                    m_CanWallJump = false;
                    detecting_time = false;
                }
            }

            Vector3 abs_scale_tr = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            if(!detecting_time || detect_time_left < 0)
            {
               Vector3 abs_scale_wallcheck = new Vector3(Math.Abs(m_WallCheck.localScale.x), m_WallCheck.localScale.y, m_WallCheck.localScale.z);

               Collider2D[] colliders_wall  = Physics2D.OverlapBoxAll(m_WallCheck.position, Vector3.Scale(abs_scale_tr, abs_scale_wallcheck), m_WhatIsGround);
               for (int i = 0; i < colliders_wall.Length; i++)
                {
                   if(colliders_wall[i].gameObject != gameObject && !Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(!m_Grounded && colliders_wall[i].gameObject.tag == "JumpableWall")
                        {
                            m_TouchWall = true;
                            m_CanDoubleJump = false;
                            m_CanWallJump = true;
                        }
                    }
                }
                m_Anim.SetBool("Ground", m_Grounded);
                detect_time_left = detect_time;
                //detecting_time = false;
            }
            else
            {
                m_CanDoubleJump = true;
            }


            if(detecting_ceil_time)
            {
                detect_ceil_time_left -= Time.deltaTime;
                m_TouchWall = false;
               
                if(detect_ceil_time_left < 0)
                {
                    m_CanWallJump = false;
                    detecting_ceil_time = false;
                }
            }

           m_TouchCeil = false;
           if(!detecting_ceil_time || detect_ceil_time_left < 0)
           {
                print("DETECT");
               Vector3 abs_scale_ceilcheck = new Vector3(Math.Abs(m_CeilingCheck.localScale.x), m_CeilingCheck.localScale.y, m_CeilingCheck.localScale.z);
               Collider2D[] colliders_ceil  = Physics2D.OverlapBoxAll(m_CeilingCheck.position, Vector3.Scale(abs_scale_tr, abs_scale_ceilcheck), m_WhatIsGround);
               for (int i = 0; i < colliders_ceil.Length; i++)
                {
                   if(colliders_ceil[i].gameObject != gameObject)
                    {
                            
                        if(!m_Grounded && colliders_ceil[i].gameObject.tag == "ClimbableCeiling")
                        {                       
                           m_TouchCeil     = true;
                           m_CanDoubleJump = false;
                           m_CanWallJump   = false;
                           m_CanJump       = false;
                        }
                    }
                    detect_ceil_time_left = detect_ceil_time;
                }
                // Set the vertical animation
                m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            }
            if(m_TouchCeil)
            {
                if(m_HasTouchedCeiling)
                {
                    Debug.Log("Ceiling");
                    m_Rigidbody2D.velocity = new Vector2(0, 0);
                    m_Rigidbody2D.gravityScale = 0;
                    m_HasTouchedCeiling = false;
                    m_CanClimbCeil = true;

                }
            }
            else
            {
                m_HasTouchedCeiling = true;
                m_Rigidbody2D.gravityScale = 5;
                m_CanClimbCeil = false;
            }
            


            
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
            if ((m_Grounded || m_AirControl))
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                    if(m_OnPlatform)
                    {
                        /*
                         float addForce = 

                         
                        if((m_CurrentPlatform.velocity.x < 0 && move > 0) || ( 0 && move < 0))
                        {
                            addForce = move * PlayerVelocity + m_CurrentPlatform.velocity.x;
                        }
                        else if((m_CurrentPlatform.velocity.x < 0 && move < 0) || (m_CurrentPlatform.velocity.x > 0 && move > 0))
                        {
                            addForce = move * PlayerVelocity  - m_CurrentPlatform.velocity.x;
                        }
                        */

                        
                       if(Math.Sign(m_CurrentPlatform.velocity.x) != Math.Sign(m_Rigidbody2D.velocity.x))
                        {
                            m_Rigidbody2D.velocity = new Vector2(move * PlayerVelocity, m_Rigidbody2D.velocity.y);

                        }
                        else if(m_CurrentPlatform.velocity.y != 0)
                        {
                            m_Rigidbody2D.velocity = new Vector2(move * PlayerVelocity + m_CurrentPlatform.velocity.x, m_Rigidbody2D.velocity.y);
                        }
                        else
                        {
                            m_Rigidbody2D.velocity = new Vector2(move * PlayerVelocity + m_CurrentPlatform.velocity.x, m_Rigidbody2D.velocity.y + m_CurrentPlatform.velocity.y);
                        }

                    }
                    else if(((walljumping && move == 0) || (disable_input && !wall_push)) && !m_CanClimbCeil)
                    {
                        m_Rigidbody2D.velocity = new Vector2(walljump_coeff, m_Rigidbody2D.velocity.y);
                    }
                    else
                    {
                        m_Rigidbody2D.velocity = new Vector2(move * PlayerVelocity, m_Rigidbody2D.velocity.y);
                        walljumping = false;
                    }

                //m_Rigidbody2D.AddForce(new Vector2(Math.Sign(move) * 800, 0));

                // If the input is moving the player right and the player is facing left...
                if(!m_CanWallJump)
                {
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
            }

            if((m_TouchWall && !disable_input) || m_Grounded)
            {   
                walljumping = false;
            }
            
            // Wall Grab
           if(!m_Grounded && m_TouchWall)
            {   

                m_Rigidbody2D.velocity = new Vector2(0, 0);

            }

            wall_push = false;

            //Wall Jump
            if(!m_Grounded && m_CanWallJump)
            {
                if(jump)
                {
                    //Debug.Log("WallJump");
                    Vector2 force = new Vector2(0,  0);
                    walljumping = true;

                    m_Rigidbody2D.AddForce(new Vector2(0,800));  
                    
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                    detecting_time = true;
                    m_CanDoubleJump = true;
                    disable_input = true;
                }

                if(m_FacingRight)
                {
                    walljump_coeff = - wallJumpForce;
                    if(Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        wall_push = true;
                    }
                }
                else
                {
                    walljump_coeff = wallJumpForce;
                    if(Input.GetKeyDown(KeyCode.RightArrow))
                    {

                        wall_push = true;
                    }
                }

            }

            // If the player should jump...
            else if ((m_Grounded || m_TouchWall))
            {
                if(jump)
                {

                    // Add a vertical force to the player.
                    m_Grounded = false;
                    m_Anim.SetBool("Ground", false);
                    m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

                    //Debug.Log("Jump");  
                }
            }

            //Double Jump
            else if(!m_Grounded && !m_TouchWall && jump && m_CanDoubleJump)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                Vector3 new_vel = new Vector3(m_Rigidbody2D.velocity.x, 0);
                m_Rigidbody2D.velocity = new_vel;
                m_CanDoubleJump = false;
            }
            else if(m_CanClimbCeil)
            {
                Debug.Log("AAAAAAAAAAAAAAAa");
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                if(jump)
                {
                    Debug.Log("STOP");
                    m_CanClimbCeil = false;
                    detecting_ceil_time = true;
                }
            }

            if(disable_input)
            {
                disable_time_left -= Time.deltaTime;
                if(disable_time_left < 0)
                {
                    disable_input = false;
                    disable_time_left = disable_time;
                }
            }

            


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

            Vector3 ceil_check_pos = transform.Find("CeilingCheck").position;
            Vector3 ceil_check_scale = Vector3.Scale(transform.Find("CeilingCheck").localScale,transform.localScale);
            Gizmos.DrawWireCube(ceil_check_pos, ceil_check_scale);  
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
