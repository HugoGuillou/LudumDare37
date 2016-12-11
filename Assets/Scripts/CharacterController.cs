using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


    [RequireComponent(typeof (Character))]
    public class CharacterController : MonoBehaviour
    {
        private Character m_Character;
        private bool m_Jump;

        private void Awake()
        {
            m_Character = GetComponent<Character>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = Input.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            
            int h = Math.Sign(Input.GetAxisRaw("Horizontal")); 



            /*
            if(Input.GetKey(KeyCode.LeftArrow))
                h = -1;

            else if(Input.GetKey(KeyCode.RightArrow))
                h = 1;
            
            */
           
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            
            m_Jump = false;
        }
    }

