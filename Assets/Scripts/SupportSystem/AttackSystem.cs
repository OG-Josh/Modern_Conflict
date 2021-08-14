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
    public Transform startLaunch;
    public int ammo = 5;
    public float dropRate = 0.5f;
    private bool attackReady = true;

    private void Awake()
    {
        sP = GetComponent<SupManager>();
    }
    private void Start()
    {
        Debug.Log("Ammo Count: " + ammo);
    }

    private void Update()
    {
        InitiateAttack();
        checkAmmo();
    }

    private void InitiateAttack()
    {
        if (dropBomb == true && attackReady == true && ammo > 0)
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
        Debug.Log("Dropped!");
        GameObject Bomb = Instantiate(ammoPrefab, startLaunch.transform.position, startLaunch.rotation);
        ammo--;
        yield return new WaitForSeconds(dropRate);
        attackReady = true;
    }
}
