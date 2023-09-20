using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATest : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stoppingDistance = 1f;

    private float normalSpeed;
    private Vector3 currentDestination;
    void Start()
    {
        normalSpeed = moveSpeed;
        SetRandomDestination();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, currentDestination) < stoppingDistance)
        {
            SetRandomDestination();
        }

        MoveTowardsDestination();
    }

    void SetRandomDestination()
    {
        float x = Random.Range(-180f, 180f);
        float z = Random.Range(-115f, 180f);
        currentDestination = new Vector3(x, transform.position.y, z);
    }

    void MoveTowardsDestination()
    {
        Vector3 moveDirection = currentDestination - transform.position;
        moveDirection.Normalize();
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotaciona o agente na direção do movimento
        if (moveDirection != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 10f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BearTrap")
        {
            StartCoroutine(BearTrap());
        }

        SetRandomDestination();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DashStun")
        {
            StartCoroutine(DashStun());
        }

        else if (other.gameObject.tag == "HoneySlow")
        {
            moveSpeed = normalSpeed / 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HoneySlow")
        {
            moveSpeed = normalSpeed;
        }
    }

    IEnumerator BearTrap()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(3f);
        moveSpeed = normalSpeed;
    }

    IEnumerator DashStun()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(2f);
        moveSpeed = normalSpeed;
    }
}