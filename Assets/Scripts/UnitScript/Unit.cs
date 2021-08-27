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
using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    private UnitMovement unitMovement = null;
    [SerializeField]
    private UnityEvent onSelected = null;
    [SerializeField]
    private UnityEvent onDeSelected = null;

    public static event Action<Unit> ServerOnUnitSpawned; // Called on server when unit is formed
    public static event Action<Unit> ServerOnUnitDeSpawned; //Called on server when unit is gone
    //We trigger above variables in OnStartServer and OnStopServer override methods

    public static event Action<Unit> AuthorityOnUnitSpawned; 
    public static event Action<Unit> AuthorityOnUnitDeSpawned;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Server 
    public override void OnStartServer()
    {
        //This and OnStopServer method tells us that THIS HAPPENED, doesn't care what happens
        //Therefore, we know that a unit is spawn and go to RTSPlayer script to add our unit to that list
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDeSpawned?.Invoke(this);
    }

    #endregion

    #region Client

    [Client]

    public override void OnStartClient()
    {
        if(!isClientOnly || !hasAuthority)
        {
            return;
        }
        AuthorityOnUnitSpawned.Invoke(this);
    }
    public override void OnStopClient()
    {
        if (!isClientOnly || !hasAuthority)
        {
            return;
        }
        AuthorityOnUnitDeSpawned?.Invoke(this);
    }

    public void Select()
    {
        if(!hasAuthority)
        {
            return;
        }

        onSelected?.Invoke();
    }
    
    [Client]
    public void Deselect()
    {
        if (!hasAuthority)
        {
            return;
        }

        onDeSelected?.Invoke();
    }

    #endregion

}
