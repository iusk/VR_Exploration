using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
        HandleHead();
        CalculateMovement();
        HandleHeight();        
    }

    private void HandleHead() {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f); // apply y rotation from the head to the character controller

        // since the camera is the child of player, it changes with the player, so we don't need to update the transformation
        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
    }

    private void CalculateMovement() {

    }

    private void HandleHeight() {

    }
}
