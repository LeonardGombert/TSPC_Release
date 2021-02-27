using Gameplay.VR;
using UnityEngine;

[System.Serializable]
public class TransitionRoom : MonoBehaviour
{
    public Transform entranceAnchor;
    public Transform exitAnchor;
    public Transform playerSpawn;

    public TransitionDoorBehaviour entranceDoor, exitDoor;

    // Enable and lock the door behind the player. Unlock the Exit Door
    public void RoomEntranceConfig()
    {
        entranceDoor.Lock();
        entranceDoor.EnableDoor();

        exitDoor.Unlock();
    }

    // Disable and unlock the Entrance door. Lock the Exit door. 
    public void RoomExitConfig()
    {
        entranceDoor.Lock();
        entranceDoor.DisableDoor();

        exitDoor.Lock();
    }
}