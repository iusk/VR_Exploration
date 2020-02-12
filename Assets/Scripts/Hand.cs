using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContractInteractables = new List<Interactable>();

    void Start()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        // Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Debug.Log(m_Pose.inputSource + " Trigger Up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Handle"))
            return;

        m_ContractInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Handle"))
            return;

        m_ContractInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    public void Pickup()
    {

    }

    public void Drop()
    {

    }

    private Interactable GetNearestInteractable()
    {
        return null;
    }
}
