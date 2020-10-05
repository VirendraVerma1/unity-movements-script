using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class UpdateCharacterInfo : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public GameObject CameraGo;
    public GameObject CineCamera;

    public PhotonView photonView;
    GameObject MyPlayer;
    void Start()
    {
        InstantialAllComponentsLocally();
    }

    void InstantialAllComponentsLocally()
    {
        GameObject Cam = Instantiate(CameraGo);
        GameObject Cinemachine = Instantiate(CineCamera);
        if(photonView.IsMine)
        photonView.RPC("InstantialGlobally", RpcTarget.AllBuffered);
        MyPlayer.GetComponent<ThirdPersonController>().cam = Cam.transform;
        Cinemachine.GetComponent<CinemachineFreeLook>().LookAt = MyPlayer.transform.Find("CameraPoint").transform;
        Cinemachine.GetComponent<CinemachineFreeLook>().Follow = MyPlayer.transform.Find("CameraPoint").transform;
    }

    [PunRPC]
    void InstantialGlobally()
    {
        MyPlayer = Instantiate(Player);
        gameObject.transform.SetParent(MyPlayer.transform);
        MyPlayer.transform.localPosition = Vector3.zero;
        //MyPlayer.transform.Find("all")
    }
    
}
