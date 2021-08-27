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

public class HeliMovement : MonoBehaviour
{
    private HeliManager HManager;

    [Header("Movement Settings:")]
    public float fAltitude = 45.0f; //Height at which plane should maintain
    public float fSpeed = 15.0f; //Speed of plane
    private Rigidbody rb;

    //For exit flight path
    [Header("Yawn Settings:")]
    [Space]
    public float yawForce = 1.0f;
    public float yawForce2 = 1.0f;

    public float rotateSpeed = 1.5f;

    private GameObject heliObject;


    private void Start()
    {
        HManager = GetComponent<HeliManager>();  
        rb = GetComponent<Rigidbody>();
        heliObject = gameObject;
    }

    private void startUp()
    {

    }
}
