#if UNITY_STANDALONE
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;

namespace Gameplay.VR
{
    public class CameraBehavior : SwitchableElement
    {
        [SerializeField] CameraPowerBehaviour cameraPowerBehaviour;
        public UnityEvent camerasOff, camerasOn;

        private void Awake() => Power = power; 
        public override SwitchableType prefabType { get { return SwitchableType.StaticCamera; } }

        [Button] public override void TurnOff() => cameraPowerBehaviour.PowerOff();

        [Button] public override void TurnOn() => cameraPowerBehaviour.PowerOn();
    }
}
#endif