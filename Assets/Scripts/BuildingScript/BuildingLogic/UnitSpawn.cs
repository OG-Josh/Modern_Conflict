/*
* Copyright (C) Josh Y. - All Rights Reserved
* Unauthorized copying of this file, via any medium is strictly prohibited 
* Proprietary and confidential
* Written by Josh Y. <joyang112@gmail.com>, June 2017
 */
// This script is to help with sending msg to server to spawn unit
//Documentations:
//================================
//[HelpUrl("URL")] Allows user to set a link to a documentation for reference
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawn : NetworkBehaviour, IPointerClickHandler
{
    //Probably make this an array/list to hold multiple units
    [SerializeField] private GameObject unit_1 = null;
    [SerializeField] private Transform spawnlocation = null;

    #region Server 

    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unitInstance = Instantiate(unit_1, spawnlocation.position, spawnlocation.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient);//unit gameobject then ownership connection
        //Connection to client is server control (Give authority to connection of client 
        //AKA GIVE THIS PERSON OWNERSHIP OF THIS UNIT
    }

    #endregion

    #region Client

    //On client, we want to call the command when we click on this game object so
    //Our method below means whenever we click on this gameObject, unit will call this method for us
    //Implement instantiate below
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Left)
        {
            return;
        }
        //Another thing to note is this is safe for "Enemy" base because we already set the clientAuthority based on the SpawnUnitMethod
        //So its protected and we don't need to bother telling server not to spawn if its an enemy base, however, may implement and should implement 
        //Example below
        if(!hasAuthority)
        {
            return;
        }

        CmdSpawnUnit();
    }


    #endregion




}
