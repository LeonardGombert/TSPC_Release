using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.VR.Player
{
    public class BonePositionTracker : MonoBehaviour
    {
        public Vector3Variable boneToTrack;
        public BoolVariable _ghostMode;

        int framesPassed;
        int frequency = 4;

        // for traps
        //private void Awake() => GetComponentInChildren<Collider>().enabled = !_ghostMode.Value;

        private void Update()
        {
            framesPassed++;
            if (framesPassed % frequency == 0)
            {
                if(_ghostMode.Value == false) boneToTrack.Value = transform.position;
                framesPassed = 0;
            }
        }
    }
}
