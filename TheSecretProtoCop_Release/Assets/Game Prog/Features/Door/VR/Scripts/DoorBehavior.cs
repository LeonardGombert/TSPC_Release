using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.VR
{
    public class DoorBehavior : MonoBehaviour, ISwitchable
    {
        [Range(0, 1), SerializeField] private int state;
        [Range(0, 1), SerializeField] private int power;

        [SerializeField] private Material red, green, blue;
        [SerializeField] private Renderer keyPassRenderer;
        [SerializeField] private Animator anim;
        [SerializeField] private UnityEngine.Events.UnityEvent _OnTurnOn;
        [SerializeField] private UnityEngine.Events.UnityEvent _OnTurnOff;

        public int State
        {
            get { return state; }

            set { state = value; }
        }

        public int Power
        {
            get { return power; }

            set
            {
                power = value;

                if (power == 1) TurnOn();
                else TurnOff();
            }
        }

        public VRPrefabTypes prefabType { get { return VRPrefabTypes.Door; } }

        public GameObject MyGameObject { get { return this.gameObject; } set { MyGameObject = value; } }

        private void Start() => Power = power;

        // Hodor (Lock)
        public void TurnOn()
        {
            _OnTurnOn.Invoke();

            anim.ResetTrigger("Open");
            anim.SetTrigger("Close");
        }

        // Open Door (Unlock)
        public void TurnOff()
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

