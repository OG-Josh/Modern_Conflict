using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlightMovement))]
[RequireComponent(typeof(AttackSystem))]
[RequireComponent(typeof(destroySelf))]
public class SupManager : MonoBehaviour
{
    private FlightMovement mvntData;
    private AttackSystem atkSytm;
    private destroySelf dS;

    //Calls to destroy gameObject on exit state
    [Header("Destruct timer on exit state:")]
    public float destructTimer = 2f;

    //Implement multiple types of status such as carpet bombing, air drop, MOAB style bomb, etc
    //use enum status to establish this in prefab inspector menu to easily alter data

    [Header("Private variables below after setting conditions")]
    public bool isCompleted = false;
    //Counter to call method once rather than 1+ times
    private bool startClearing = false;

    private void Awake()
    {   
        //Initiate data
        mvntData = GetComponent<FlightMovement>();
        dS = GetComponent<destroySelf>();
        atkSytm = GetComponent<AttackSystem>();
    }

    private void Update()
    {
        CompletionStatus();
    }

    private void CompletionStatus()
    {
        //Set up types of exit condition to execute exit style wanted such as A10 vs Airliner

        if (isCompleted == true && startClearing == false)
        {
            dS.delayDestruct(destructTimer);
            startClearing = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        atkSytm.dropBomb = true;
    }
}
