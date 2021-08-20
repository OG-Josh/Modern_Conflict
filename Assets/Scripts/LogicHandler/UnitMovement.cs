/*
* Copyright (C) Josh Y. - All Rights Reserved
* Unauthorized copying of this file, via any medium is strictly prohibited 
* Proprietary and confidential
* Written by Josh Y. <joyang112@gmail.com>, June 2017
*/
//Documentations:
//================================
//[HelpUrl("URL")] Allows user to set a link to a documentation for reference
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    private Camera playerCamera;

    #region Server

    [Command]
    private void CmdMove(Vector3 Position)
    {
        if(!NavMesh.SamplePosition(Position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        playerCamera = Camera.main;
    }

    [ClientCallback]

    private void Update()
    {
        if(!hasAuthority)
        {
            return;
        }

        //For old input system
        //if(!Input.GetMouseButtonDown(1))
        //{
        //    return;
        //}

        //For new input system
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        //For old input system
        //Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        //For new input system
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        CmdMove(hit.point);
    }

    #endregion
}
