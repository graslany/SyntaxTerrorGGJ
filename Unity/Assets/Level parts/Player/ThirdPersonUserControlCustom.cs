using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacterCustom))]
    public class ThirdPersonUserControlCustom : NetworkBehaviour
    {
        [SerializeField] Camera Cam;
        private ThirdPersonCharacterCustom m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.


        private void Start()
        {
            if (!isLocalPlayer)
            {
                Cam.enabled = false;
            }
            else
            {
                Cam.enabled = true;
        }
            // get the transform of the main camera
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacterCustom>();
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            if (!m_Jump)
            {
             //   m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        public override void OnStartLocalPlayer()
        {
            Cam.gameObject.transform.SetParent(null);
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            // we use world-relative directions in the case of no main camera
            m_CamForward = Vector3.Scale(Cam.transform.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * Cam.transform.right;
            // m_Move = v * Vector3.forward + h * Vector3.right;
#if !MOBILE_INPUT
            // walk speed multiplier
            if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}