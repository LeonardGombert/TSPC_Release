using Gameplay.VR.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

public class TeleportAreaHandler : MonoBehaviour
{
    [HideInInspector] public bool levelIsActive;
    public VisualEffect[] teleportParticles;
    public TeleportationArea[] teleportGlow;

    [Button]
    private void AssignTeleportAreaReferences()
    {
        teleportParticles = GetComponentsInChildren<VisualEffect>();
        teleportGlow = GetComponentsInChildren<TeleportationArea>();
    }
}
