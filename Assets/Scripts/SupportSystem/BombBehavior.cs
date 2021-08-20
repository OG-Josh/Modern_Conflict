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

public class BombBehavior : MonoBehaviour//, IPooledObject
{
    private objectPool objectPooler;

    [Header("Bomb Settings:")]
    //Type of explosion when collision hits
    public string vfxTag;
    private Rigidbody rb;
    [Space]
    public float momentumSpeed = 5.0f;
    public float tiltDirection = 75.0f;
    public float tiltSlerp = 1.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        objectPooler = objectPool.Instance;
    }
    private void FixedUpdate()
    {
        tiltBehavior();
        momentumForce();
    }
    //Momentum Force
    private void momentumForce()
    {
        rb.AddRelativeForce(0, 0, momentumSpeed);

    }

    //Tilt Behavior
    private void tiltBehavior()
    {
        Quaternion target = Quaternion.Euler(tiltDirection, 0, 0);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, target, /*Time.deltaTime */ tiltSlerp);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        //Instantiate explosion and call selfdestruct
        //GameObject VFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        objectPooler.SpawnFromPool(vfxTag, transform.position, Quaternion.identity);
        //Debug.Log("BOOM!");
        gameObject.SetActive(false);
    }

}
