using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    private SupManager sP;

    [Header("Test Input, delete when finished setting condition")]
    public bool dropBomb = false;

    [Header("Behavior Settings:")]
    public GameObject ammoPrefab;
    public Transform[] startLaunch;
    public bool scatterDrop = false;
    public int ammo = 5;
    public float dropRate = 0.5f;
    private bool attackReady = true;
    
    private void Awake()
    {
        sP = GetComponent<SupManager>();
    }

    private void Update()
    {
        InitiateAttack();
        checkAmmo();
    }

    private void InitiateAttack()
    {
        if (dropBomb == true && attackReady == true && ammo > 0 && scatterDrop == true)
        {
            StartCoroutine(InitiateScatterDrop());

        }
        else if (dropBomb == true && attackReady == true && ammo > 0)
        {
            StartCoroutine(InitiateDrop());
        }
    }
    private void checkAmmo()
    {
        if(ammo == 0)
        {
            sP.isCompleted = true;
        }
    }
    //Drop Rate
    IEnumerator InitiateDrop()
    {
        attackReady = false;
        GameObject Bomb = Instantiate(ammoPrefab, startLaunch[0].transform.position, startLaunch[0].rotation);        
        ammo--;
        yield return new WaitForSeconds(dropRate);
        attackReady = true;
    }
    IEnumerator InitiateScatterDrop()
    {

        for(int i = 0; i < startLaunch.Length; i++)
        {
            if(attackReady == true)
            {
                attackReady = false;
                GameObject Bomb = Instantiate(ammoPrefab, startLaunch[i].transform.position, startLaunch[i].rotation);
                ammo--;
                yield return new WaitForSeconds(dropRate);
                attackReady = true;

            }
        }
    }
}
