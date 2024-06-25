using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameObjectThrowController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TablewareManager tablewareManager;
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] ObjectGrabInteractable objectGrabInteractable;
    [SerializeField] Animator knifeAnimator;
    public float throwPower = 12f;
    public float repelPower = 10f;
    public Transform target;
    private Rigidbody rb;
    private Vector3 throwDirectinon;
    private bool isEndThrow = false;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void Throw()
    {
        transform.parent = null;
        knifeAnimator.SetBool("Throw", true); //enable animation rotation
        throwDirectinon = (target.position - transform.position).normalized; //set direction
        rb.isKinematic = false;
        knifeAnimator.SetBool("Throw", true); //enable animation rotation
        throwDirectinon = (target.position - transform.position).normalized; //set direction
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.AddForce(throwDirectinon * throwPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            PlayerLeanBack.instance.CallLeanBack();
        }

        if (isEndThrow == false)
        {
            RestoreState();
        }

        
    }

    public void TakeRepel(Vector3 _direction)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(_direction * repelPower, ForceMode.Impulse);
    }

    private void RestoreState()
    {
        isEndThrow = true;
        //end animation
        knifeAnimator.SetBool("Throw", false);
        //permit rb move & rotate
        rb.constraints = RigidbodyConstraints.None; 
        //permit gravity
        rb.useGravity = true; 
        // enable component
        tablewareManager.enabled = true; 
        grabInteractable.enabled = true;
        objectGrabInteractable.enabled = true;
        // disable this system
        this.enabled = false;
    }
}
