using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject SP;
    private bool ready = true;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ready == true)
        {
            StartCoroutine(SpawnSP());
        }
    }

    IEnumerator SpawnSP()
    {
        ready = false;
        GameObject Plane = Instantiate(SP, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        ready = true;
    }
}
