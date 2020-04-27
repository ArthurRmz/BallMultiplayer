using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : Photon.PunBehaviour
{
    public float velocitySpeed = 5.0f;
    public float x;
    public float y;

    private void Start()
    {
        if (!photonView.isMine)
        {
            Destroy(this);
            //Destroy(this.GetComponent<Rigidbody>());
        }
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        transform.Translate(x * Time.deltaTime * velocitySpeed, 0, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocitySpeed);
    }

}
