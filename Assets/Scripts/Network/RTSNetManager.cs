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

public class RTSNetManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawner = null;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        //player connects
        base.OnServerAddPlayer(conn);

        //BELOW IS GOOD FOR A HQ START IN AN RTS GAME
        GameObject unitSpawn = Instantiate(unitSpawner, conn.identity.transform.position, conn.identity.transform.rotation);
        //Now we got this on the server, we need to communicate this to our network 
        NetworkServer.Spawn(unitSpawn, conn);
        //Above will spawn in (show) on all the client and give authority to connected client aka "conn" param of our method

    }
}
