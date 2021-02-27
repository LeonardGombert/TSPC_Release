using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LevelAssembler : MonoBehaviour
    {
        public Platform platform;

        [ShowIf("platform", Platform.VR)]
        [Title("Assembler VR")]
        public AssemblerVR assemblerVR;

        [ShowIf("platform", Platform.Mobile)]
        [Title("Assembler Mobile")]
        public AssemblerMobile assemblerMobile;

        public Assembler assembler
        {
            get {
                if (platform == Platform.VR) return assemblerVR;
                else return assemblerMobile;
            }
        }

        public void AssembleLevel()
        {
            assembler.PickRooms();

            assembler.CreateLevel();

            LevelManager.instance.StartLevel();
        }
    }

    [System.Serializable]
    public abstract class Assembler
    {
        [Button]
        public void FindAllRoomChunks()
        {
            roomChunks.Clear();
            RoomManager[] rooms = Object.FindObjectsOfType<RoomManager>();
            foreach (RoomManager room in rooms) roomChunks.Add(room);
        }

        public List<RoomManager> roomChunks = new List<RoomManager>();

        public LevelVariable levelHolder;
        public List<RoomManager> selectedRooms { get; protected set; } = new List<RoomManager>();

        public Transform LevelParent;
        
        public TransitionRoom playerSpawnRoom { get => LevelManager.instance.currentStartRoom; }
        public TransitionRoom playerExitRoom { get => LevelManager.instance.currentExitRoom; }

        public void PickRooms()
        {
            foreach (RoomData roomData in levelHolder.pickedRooms)
            {
                foreach (RoomManager roomManager in roomChunks)
                {
                    if (roomManager.room.roomName == roomData.roomName)
                    {
                        roomManager.room.roomModifier = roomData.roomModifier;
                        selectedRooms.Add(roomManager);
                    }
                }
            }
            selectedRooms.Reverse();
        }

        public abstract void CreateLevel();
    }
    
    // this is the function you need to change for moving the transition room between levels
    [System.Serializable]
    [HideLabel]
    public class AssemblerVR : Assembler
    {
        //public Transform levelEntranceAnchor;
        
        public override void CreateLevel()
        {
            LevelManager.instance.LevelRooms.Clear();
            LevelManager.instance.LevelRooms = selectedRooms;

            //Transform currentAnchor = levelEntranceAnchor;

            RoomVR indexRoomVR;

            for (int i = 0; i < selectedRooms.Count; i++)
            {
                indexRoomVR = (RoomVR)selectedRooms[i].room; // get the room 
                /*
                Vector3 translation = currentAnchor.position - indexRoomVR.entranceAnchor.localPosition;
                indexRoomVR.transform.position = translation; // change the position of the room

                float angle = currentAnchor.rotation.eulerAngles.y - indexRoomVR.entranceAnchor.localRotation.eulerAngles.y;
                indexRoomVR.transform.RotateAround(currentAnchor.position, Vector3.up, angle); // channge the rotation of the room */

                indexRoomVR.transform.parent = LevelParent; // set the parent of the room to be the assembler (keep)
                indexRoomVR.transform.gameObject.SetActive(false); // turn off all other rooms

                //currentAnchor = indexRoomVR.exitAnchor;
            }

            RoomVR firstRoom = (RoomVR)selectedRooms[0].room; // get the first room again

            // Get distance between current room entrance and Transition Room 1 exit Anchor
            Vector3 spawnTranslation = firstRoom.entranceAnchor.position - playerSpawnRoom.exitAnchor.localPosition;
            playerSpawnRoom.transform.position = spawnTranslation; // change the position of the entrance transition room
            float spawnRoomAngle = firstRoom.entranceAnchor.rotation.eulerAngles.y - playerSpawnRoom.exitAnchor.rotation.eulerAngles.y;
            playerSpawnRoom.transform.RotateAround(firstRoom.entranceAnchor.position, Vector3.up, spawnRoomAngle); // channge the rotation of the room */

            // Get distance between current room exit and Transition Room 2 entrance Anchor
            Vector3 endTranslation = firstRoom.exitAnchor.position - playerExitRoom.entranceAnchor.localPosition;
            playerExitRoom.transform.position = endTranslation; // change the position of the exit transition room
            float exitRoomAngle = firstRoom.exitAnchor.rotation.eulerAngles.y - playerExitRoom.entranceAnchor.rotation.eulerAngles.y;
            playerExitRoom.transform.RotateAround(firstRoom.exitAnchor.position, Vector3.up, exitRoomAngle); // channge the rotation of the room */

        }
    }

    [System.Serializable]
    [HideLabel]
    public class AssemblerMobile : Assembler
    {
        public override void CreateLevel()
        {
            LevelManager.instance.LevelRooms.Clear();
            LevelManager.instance.LevelRooms = selectedRooms;

            RoomMobile indexRoomMobile;

            for (int i = 0; i < selectedRooms.Count; i++)
            {
                indexRoomMobile = (RoomMobile)selectedRooms[i].room;

                indexRoomMobile.transform.parent = LevelParent;
                indexRoomMobile.transform.gameObject.SetActive(false);
            }
        }
    }
}
