using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{
    public float m_Sensitivity = 0.1f; // -1 to 1
    public float m_MaxSpeed = 1.0f;

    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake() {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start() {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    private void Update() {
        // HandleHeight();  
        // HandleHead();
        // CalculateMovement();
    }

    private void HandleHead() {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        // apply y rotation from the head to the character controller
        // necessary to move forward based on the angle we are facing
        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        // since the camera is the child of player, it changes with the player, so we don't need to update the transformation
        // not doing this causes the head to keep on spinning forever
        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
    }

    private void CalculateMovement() {
        // figure out movement orientation
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // if not moving
        if (m_MovePress.GetStateUp(SteamVR_Input_Sources.Any)) {
            m_Speed = 0;
        }

        // if button pressed
        if (m_MovePress.state) {
            // add, clamp
            m_Speed += m_MoveValue.axis.y * m_Sensitivity;
            m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed, m_MaxSpeed); // can reduce backward speed here

            // orientation - moving in the direction we are looking at
            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }

        // apply
        m_CharacterController.Move(movement);
    }

    private void HandleHeight() {
        // get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight; // our pivot point is the center, not the feet, since we are using a capsule

        // cut in half - vertical movement
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height /2;
        newCenter.y += m_CharacterController.skinWidth;

        // move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        // rotate the center of capsule (not the transform)
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        //apply - from the head to the capsule
        m_CharacterController.center = newCenter;
    }
}
