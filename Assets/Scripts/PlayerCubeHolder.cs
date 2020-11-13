using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeHolder : MonoBehaviour
{
    [SerializeField] private float GRAVITY_PULL = .78f;
    private float gravityRadius = 1f;

    private void Awake()
    {
        gravityRadius = GetComponent<SphereCollider>().radius;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cubic")
        {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / gravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * GRAVITY_PULL * Time.fixedDeltaTime);
        }
    }
}
