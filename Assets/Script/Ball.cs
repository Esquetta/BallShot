using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    Renderer color;
    public GameManager manager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        color = GetComponent<Renderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {


            Bucket();
            manager.BallIn();

        }
        else if (other.CompareTag("Destroy"))
        {

            BallDestroy();
            manager.BallOut();

        }
    }
    void BallDestroy()
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    void Bucket()
    {
        manager.PlayBallEfect(gameObject.transform.position, color.material.color);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
