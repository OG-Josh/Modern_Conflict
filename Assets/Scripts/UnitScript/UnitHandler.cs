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
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitHandler : MonoBehaviour
{
    //Variables regarding multi select zone
    [SerializeField] private RectTransform unitSelectionArea = null;
    private Vector2 startPosition;
    private RTSPlayer player;

    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Camera mainCamera;

    public List<Unit> SelectedUnits { get; } = new List<Unit>();

    private void Start()
    {
        mainCamera = Camera.main;
        //player = NetworkClient.connection.identity.GetComponent<RTSPlayer>(); //Solution for player
    }

    private void Update()
    {
        if(player == null)
        {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>(); //Solution for player
        }


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
        else if(Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionArea();
        }
    }

    //When mouse starts to drag, it starts the selection box
    private void StartSelectionArea()
    {
        if(!Keyboard.current.leftShiftKey.isPressed)
        {
            //==========================
            //NOTE: if we want a specific key to be press and not clear list, COMMENT THIS SECTION OUT!!!
            //Deselecting units
            foreach (Unit selectedUnit in SelectedUnits)
            {
                selectedUnit.Deselect(); //Turn on/off our highlight aka our method for selecting our unit in a way regarding visual feedback
            }
            //empty list of selected units
            SelectedUnits.Clear();
            //==========================
        }

        unitSelectionArea.gameObject.SetActive(true);

        startPosition = Mouse.current.position.ReadValue();

        UpdateSelectionArea();
    }
    //Keeps updating on screen box when dragging to select multiple units in scene
    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float areaWidth = mousePosition.x - startPosition.x;
        float areaHeight = mousePosition.y - startPosition.y;

        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = startPosition + new Vector2(areaWidth / 2, areaHeight / 2);
    }
    private void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);

        if (unitSelectionArea.sizeDelta.magnitude == 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            //Layer mask states the stuff that raycast can hit based our logic
            //For instance, most, if not all units should have a "Unit" layermask for players to raycast and select
            //In short, checks raycast hit
            if (!Physics.Raycast(ray, out RaycastHit Hit, Mathf.Infinity, layerMask))
            {
                return;
            }

            //Here says if we raycast to select our unit and its not a unit, then return
            //In short, checks if its a selectable unit
            if (!Hit.collider.TryGetComponent<Unit>(out Unit unit))
            {
                return;
            }

            //Otherwise, it continues on below

            //Checks if client has authority to select this unit
            //In short, check its ownership
            if (!unit.hasAuthority)
            {
                return;
            }

            //Adds to the list of selected units to control or activate regarding logic
            SelectedUnits.Add(unit);

            foreach (Unit selectedUnit in SelectedUnits)
            {
                selectedUnit.Select(); //Turn on/off our highlight aka our method for selecting our unit in a way regarding visual feedback
            }

            return;
        }

        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

        foreach(Unit unit in player.GetMyUnits())
        {
            //When we shift+hold to select more unit, this won't keep calling for the ones already added in the list
            if (SelectedUnits.Contains(unit))
            {
                continue;
            }

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            //If unit detected is within this frame/window/selectionBox then select unit
            if(screenPosition.x > min.x 
            && screenPosition.x < max.x
            && screenPosition.y > min.y 
            && screenPosition.y < max.y)
            {
                //Add to unit list and select it
                SelectedUnits.Add(unit);
                unit.Select();
            }
        }
    }

}
