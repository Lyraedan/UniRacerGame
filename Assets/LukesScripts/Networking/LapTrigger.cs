﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// There should only be 1 lap trigger in the scene
/// </summary>
public class LapTrigger : MonoBehaviour
{

    public static LapTrigger instance;
    public bool active = false;

    public List<LapProgressionTrigger> progressionTriggers = new List<LapProgressionTrigger>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject root = other.transform.root.gameObject;
        if (root.GetComponent<PhotonView>())
        {
            if (root.GetComponent<PhotonView>().IsMine)
            {
                NetworkedUser user = root.GetComponent<NetworkedUser>();
                active = user.currentLapProgression >= progressionTriggers.Count;
                if (!active) return;

                user.NextLap();
                active = false;
            }
        }

    }

}
