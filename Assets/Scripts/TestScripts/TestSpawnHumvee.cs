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

public class TestSpawnHumvee : MonoBehaviour
{
    private objectPool ObjectPooler;

    public GameObject SP;
    private bool ready = true;

    private void Start()
    {
        ObjectPooler = objectPool.Instance;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ready == true)
        {
            ObjectPooler.SpawnFromPool("Humvee", transform.position, Quaternion.identity);
            //GameObject Plane = Instantiate(SP, transform.position, Quaternion.identity);
            //StartCoroutine(SpawnSP());
        }
    }

    IEnumerator SpawnSP()
    {
        ready = false;
        //GameObject Plane = Instantiate(SP, transform.position, Quaternion.identity);
        ObjectPooler.SpawnFromPool("Humvee", transform.position, Quaternion.identity); 
        yield return new WaitForSeconds(1.0f);
        ready = true;
    }


}
