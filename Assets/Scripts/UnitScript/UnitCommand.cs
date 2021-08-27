using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommand : MonoBehaviour
{
    [SerializeField] private UnitHandler unitHandler = null;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //When we right click
        if(!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }
        //We do a raycast for the point location
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //If we don't hit anything then return
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }
        //But if we do, then we get our movement
        TryMove(hit.point);
    }

    private void TryMove(Vector3 point)
    {
        //Move all our selected units 
        foreach(Unit unit in unitHandler.SelectedUnits)
        {
            //To that point
            unit.GetUnitMovement().CmdMove(point);
        }
    }
}
