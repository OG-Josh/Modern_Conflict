using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQCenter : MonoBehaviour
{

    public float crossHairLerpFactor = 1.0f;

    public SpriteRenderer crossHair;

    public LayerMask terrainLayerMask;
    private void Start()
    {
        //MoveCrossHair();
    }

    private void Update()
    {
        MoveCrossHair();
    }
    private void MoveCrossHair()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance: 300f, terrainLayerMask))
        {
            //paint without terrain
            //crossHair.transform.rotation = Quaternion.Euler(crossHair.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, crossHair.transform.rotation.eulerAngles.z);
            crossHair.transform.position = Vector3.Lerp(crossHair.transform.position, new Vector3(hit.point.x, hit.point.y + 0.02f, hit.point.z), crossHairLerpFactor);
            crossHair.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            crossHair.transform.RotateAround(crossHair.transform.position, crossHair.transform.forward, transform.eulerAngles.y);
        }
    }
}
