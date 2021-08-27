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
using UnityEngine;
using UnityEngine.AI;

public class NavmeshControl : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent nav;
    public float delay = 1.0f;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    [ContextMenu("Chase")]
    void Chase()
    {
        StartCoroutine(Chasing());
    }

    IEnumerator Chasing()
    {
        while(true)
        {
            nav.SetDestination(Target.position);
            yield return new WaitForSeconds(delay);
        }
    }
}
