using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.VR
{
    public class DoorBehavior : SwitchableElement
    {
        [SerializeField] private Material red, green, blue;
        [SerializeField] private Renderer keyPassRenderer;
        [SerializeField] private Animator anim;
        [SerializeField] private UnityEngine.Events.UnityEvent _OnTurnOn;
        [SerializeField] private UnityEngine.Events.UnityEvent _OnTurnOff;
        public override SwitchableType prefabType { get { return SwitchableType.Door; } }

        private void Start() => Power = power;

        // Hodor (Lock)
        public override void TurnOn()
        {
            _OnTurnOn.Invoke();

            anim.ResetTrigger("Open");
            anim.SetTrigger("Close");
        }

        // Open Door (Unlock)
        public override void TurnOff()
        {
            _OnTurnOff.Invoke();
            anim.ResetTrigger("Close");
            anim.SetTrigger("Open");
        }

        [Button("Unlock")]
        public void Unlock() => TurnOff();

        [Button("Lock")]
        public void Lock() => TurnOn();

        [InfoBox("Only for Debug")]
        [Button("Change Power")]
        public void ChangePower()
        {
            if (Power == 1) Power = 0;
            else Power = 1;
        }
    }
}

