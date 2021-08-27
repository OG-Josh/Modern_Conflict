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
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpawner : MonoBehaviour
{
    public GameObject SP;
    private bool ready = true;
    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            StartCoroutine(SpawnSP());
        }
    }

    IEnumerator SpawnSP()
    {
        ready = false;
        GameObject spawnObject = Instantiate(SP, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        ready = true;
    }
}
