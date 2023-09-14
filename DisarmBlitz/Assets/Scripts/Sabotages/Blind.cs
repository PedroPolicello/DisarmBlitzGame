using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody blindRig;

    private void Awake()
    {
        blindRig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        blindRig.AddForce(transform.up * 10);
        blindRig.AddForce(transform.forward * force);
        print("foi");
    }
}
