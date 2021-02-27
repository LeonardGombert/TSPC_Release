using Networking;
using UnityEngine;

namespace Gameplay.VR
{
    public class LoadNextRoom : MonoBehaviour
    {
        public bool beatRoom { get; set; }
        public TransitionRoom transitionRoom;

        private void Awake()
        {
            if (transitionRoom == LevelManager.instance.currentExitRoom) transitionRoom.RoomExitConfig();
            else if(transitionRoom == LevelManager.instance.currentStartRoom) transitionRoom.RoomEntranceConfig();
        }

        void OnTriggerEnter(Collider other)
        {            
            if (other.name == "[HeadCollider]" && !beatRoom && transitionRoom == LevelManager.instance.currentExitRoom)
            {
                // load the next level
                TransmitterManager.instance.SendRoomChangeToAll();

                // set beat room to true so you don't accidentally loop the event
                beatRoom = true;
            }
        }

        // once the player leaves the Transition Room, reset beatRoom and lock the exit door
        private void OnTriggerExit(Collider other)
        {
            if (other.name == "[HeadCollider]" && beatRoom && transitionRoom == LevelManager.instance.currentStartRoom) beatRoom = false;
        }
    } 
}
