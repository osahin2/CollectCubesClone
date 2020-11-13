using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private float frequency = 10f;
    [SerializeField]
    private float magnitude = 0.5f;

    private Vector3 startPos;
    private Transform damagerParent;

    private void Start()
    {
        damagerParent = transform.parent;
        transform.SetParent(null);
        startPos = transform.position;
        StartCoroutine(MoveDamager());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.SetParent(damagerParent);
            other.gameObject.SetActive(false);
            GameController.Instance.RetryLevel();
        }
    }

    IEnumerator MoveDamager()
    {
        while (true)
        {
            transform.position = startPos + new Vector3(0, 0, Mathf.Sin(Time.time * frequency) * magnitude);
            yield return null;
        }
    }
}
