using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(destroySelf))]
public class BombBehavior : MonoBehaviour
{
    private destroySelf sD;

    [Header("Bomb Settings:")]
    //Type of explosion when collision hits
    public GameObject explosionVFX;
    public float vfxTimer = 1.0f;
    private Rigidbody rb;
    public float momentumSpeed = 5.0f;
    public float tiltSlerp = 1.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
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
        Quaternion target = Quaternion.Euler(80, 0, 0);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * tiltSlerp);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        //Instantiate explosion and call selfdestruct
        GameObject VFX = Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(VFX, vfxTimer);
        Destroy(gameObject);
    }

}
