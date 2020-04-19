using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : Photon.MonoBehaviour
{
    private const float ConstInterpolate = 0.04f;
    public Vector3 RealPosition = Vector3.zero;
    public Vector3 RealPosition1 = Vector3.zero;

    public Quaternion RealRotation = Quaternion.identity;
    public Quaternion RealRotation1 = Quaternion.identity;

    public Vector3 RealVelocity = Vector3.zero;
    public Vector3 RealVelocity1 = Vector3.zero;

    public Rigidbody rg;
    private void FixedUpdate()
    {
        Debug.Log("[Sync][FixedUpdate] Error");
        if (!photonView.isMine)
        {
            rg.position = Vector3.Lerp(rg.position, RealPosition1, ConstInterpolate);
            rg.rotation = Quaternion.Lerp(rg.rotation, RealRotation1, ConstInterpolate);
            rg.velocity = Vector3.Lerp(rg.velocity, RealVelocity, ConstInterpolate);
            rg.angularVelocity = Vector3.Lerp(rg.angularVelocity, RealVelocity1, ConstInterpolate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, RealPosition, ConstInterpolate);
            transform.rotation = Quaternion.Lerp(transform.rotation, RealRotation, 0.0f);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("[Sync][OnPhotonSerializeView] Error" + info.ToString());
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(rg.position);
            stream.SendNext(rg.rotation);
            stream.SendNext(rg.velocity);
            stream.SendNext(rg.angularVelocity);

        }
        else
        {
            RealPosition = (Vector3)stream.ReceiveNext();
            RealRotation = (Quaternion)stream.ReceiveNext();

            RealPosition1 = (Vector3)stream.ReceiveNext();
            RealRotation1 = (Quaternion)stream.ReceiveNext();
            RealVelocity = (Vector3)stream.ReceiveNext();
            RealVelocity1 = (Vector3)stream.ReceiveNext();
        }
    }
}
