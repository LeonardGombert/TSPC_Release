 #if UNITY_STANDALONE
using Gameplay.VR.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace Gameplay.VR.Feedbacks
{
    public class TeleportationFeedbackManager : MonoBehaviour
    {
        [SerializeField] private VisualEffectAsset teleportationAreaAsset, teleportationEffectAsset;
        [SerializeField] private List<VisualEffect> particles = new List<VisualEffect>();
        [SerializeField] private List<TeleportationArea> teleportGlowers = new List<TeleportationArea>();

        private void Awake()
        {
            GE_RefreshScene();
        }

        public void GE_TeleportationAreaVisualsOn()
        {
            for (int i = 0; i < particles.Count; i++)
                particles[i].Play();
        }

        public void GE_TeleportationAreaVisualsOff()
        {
            for (int i = 0; i < particles.Count; i++)
                particles[i].Stop();
        }

        public void GE_TeleportationAreaGlowOn()
        {
            for (int i = 0; i < teleportGlowers.Count; i++) 
                teleportGlowers[i].On();
        }

        public void GE_TeleportationAreaGlowOff()
        {
            for (int i = 0; i < teleportGlowers.Count; i++) 
                teleportGlowers[i].Off();
        }

        public void GE_RefreshScene()
        {
            Debug.LogError("REFRESHING");

            particles.Clear();
            teleportGlowers.Clear();

            VisualEffect[] iteratorList = FindObjectsOfType<VisualEffect>();

            // sort through all VFX graphs in the scene
            for (int i = 0; i < iteratorList.Length; i++)
                if (iteratorList[i].visualEffectAsset == teleportationAreaAsset && iteratorList[i].enabled)
                    particles.Add(iteratorList[i]);

            for (int i = 0; i < particles.Count; i++)
                particles[i].Stop();

            teleportGlowers.AddRange(FindObjectsOfType<TeleportationArea>());

            foreach (var item in teleportGlowers) if (item.enabled == false) Debug.Log(item.name + " is inactive !!!");
        }
    }
}
#endif