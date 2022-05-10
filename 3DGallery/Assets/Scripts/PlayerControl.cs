using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool soruAcilsin;
    private float speed;
    private float angle;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        angle = 45f;
        speed = .5f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * vertical * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * angle  * horizontal * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Question")
        {
            soruAcilsin = true;
        }
    }
}
