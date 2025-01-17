﻿using Photon.Pun;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun.Demo.Asteroids;

public class PositionTracker : MonoBehaviourPunCallbacks
{

    public static PositionTracker instance;

    public int yourPosition { get; private set; }

    int position = 1;
    float distance = 100f;
    int myProgression = 0;
    int myLap = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        yourPosition = CalculatePosition();
    }

    int CalculatePosition()
    {
        position = 1;
        foreach (NetworkedUser user in FindObjectsOfType<NetworkedUser>())
        {
            if (user.tag.Equals("Player"))
            {
                distance = user.DistanceToNextProgressionPoint();
                myProgression = user.currentLapProgression;
                myLap = user.currentLap;
            }
            else
            {
                float theirDistance = user.DistanceToNextProgressionPoint();
                int theirProgression = user.currentLapProgression;
                int theirLap = user.currentLap;

                bool increasePosition = false;

                bool theyAreAheadOfMe = theirDistance < distance;
                bool theyAreALapAhead = theirLap > myLap;
                bool theyAreProgressedMore = theirProgression > myProgression;

                bool sameLapAsMe = theirLap == myLap;
                bool sameProgressionAsMe = theirProgression == myProgression;

                // Logic checks
                if (theyAreALapAhead)
                    increasePosition = true;
                else if (sameLapAsMe && theyAreProgressedMore)
                    increasePosition = true;
                else if (sameLapAsMe && sameProgressionAsMe && theyAreAheadOfMe)
                    increasePosition = true;

                if (increasePosition)
                    position++;
            }
        }
        return position;
    }
}
