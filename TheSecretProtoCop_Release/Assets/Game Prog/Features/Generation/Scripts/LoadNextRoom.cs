using Networking;
using UnityEngine;

namespace Gameplay.VR
{
    public class LoadNextRoom : MonoBehaviour
    {
        public bool passed { get; set; }

        public DoorBehavior door;

        void OnTriggerEnter(Collider other)
        {
            if (other.name == "[HeadCollider]" && !passed)
            {
                TransmitterManager.instance.SendRoomChangeToAll();

                door.Power = 0;
                door.Unlock();

                passed = true;
            }
        }

        // once the player leaves the Transition Room, send a signal to close the Door, and move the room to the End Anchor
        private void OnTriggerExit(Collider other)
        {
            if (other.name == "[HeadCollider]" && passed)
            {
                /*
                LevelManager.instance.currentEntrance = thisTransitionRoom;

                TransmitterManager.instance.SendRoomChangeToAll();

                door.Power = 0;
                door.Unlock();
                */
                passed = false;
            }
        }
    } 
}
