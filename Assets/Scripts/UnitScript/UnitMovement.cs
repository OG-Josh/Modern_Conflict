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
//using UnityEngine.InputSystem;
public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    //private Camera playerCamera;

    #region Server

    [ServerCallback]
    private void Update()
    {
        //Stops if agent already has path
        if(!agent.hasPath)
        {
            return;
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }

        agent.ResetPath();
        //This method sets it so that multiple unit doesn't fight over selected destination when reached
        //Smoother destination setter by resetting/clearing destination when unit reaches it while others are still
        //enroute to destination
        //NOTE: Have to set stopping distance in nav mesh agent (Set stopping distance to radius of nav mesh agent)
    }

    [Command]
    public void CmdMove(Vector3 Position)
    {
        if(!NavMesh.SamplePosition(Position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    #endregion

    //#region Client

    //public override void OnStartAuthority()
    //{
    //    playerCamera = Camera.main;
    //}

    //[ClientCallback]

    //private void Update()
    //{
    //    if(!hasAuthority)
    //    {
    //        return;
    //    }

    //    //For old input system
    //    //if(!Input.GetMouseButtonDown(1))
    //    //{
    //    //    return;
    //    //}

    //    //For new input system
    //    if (!Mouse.current.rightButton.wasPressedThisFrame)
    //    {
    //        return;
    //    }

    //    //For old input system
    //    //Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
    //    //For new input system
    //    Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());


    //    if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
    //    {
    //        return;
    //    }

    //    CmdMove(hit.point);
    //}

    //#endregion
}
