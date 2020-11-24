using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeHolder : MonoBehaviour
{
    [SerializeField] private float GRAVITY_PULL = .78f;
    [SerializeField] private SphereCollider holderCollider;

    //private float gravityRadius = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cubic")
        {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / holderCollider.radius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * GRAVITY_PULL * Time.fixedDeltaTime);
        }
    }
}
