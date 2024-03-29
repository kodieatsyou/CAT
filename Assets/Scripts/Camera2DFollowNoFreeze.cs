﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollowNoFreeze : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;
    private Vector3 m_LookInfrontPos;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {

        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target.position - m_LastTargetPosition).x;
        float yMoveDelta = (target.position - m_LastTargetPosition).y;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
        bool updateLookInfrontTarget = Mathf.Abs(yMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        } else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.fixedDeltaTime * lookAheadReturnSpeed);
        }
        if(updateLookInfrontTarget)
        {
            m_LookInfrontPos = lookAheadFactor * Vector3.forward * Mathf.Sign(yMoveDelta);
        } else
        {
            m_LookInfrontPos = Vector3.MoveTowards(m_LookInfrontPos, Vector3.zero, Time.fixedDeltaTime * lookAheadReturnSpeed); 
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 infrontTargetPos = target.position + m_LookInfrontPos + Vector3.left * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = target.position;
        }
    }
