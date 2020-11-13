using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEffector : MonoBehaviour
{
    public static CubeEffector Instance { get; private set; }

    [SerializeField] private float magnetForce;
    [SerializeField] private Material collectedCubeMat;

    public event Action<CubeEffector> OnEnterArea;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cubic")
        {
            if (other.TryGetComponent(out Renderer rend))
            {
                rend.material = collectedCubeMat;
            }
            other.attachedRigidbody.velocity = (transform.position - other.transform.position ) * magnetForce;
            other.gameObject.layer = 8;
            OnEnterArea?.Invoke(this);
        }
    }

}
