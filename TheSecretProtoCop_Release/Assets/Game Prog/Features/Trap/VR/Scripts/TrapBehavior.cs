using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Gameplay.VR
{
    [RequireComponent(typeof(BoxCollider))]
    public class TrapBehavior : SwitchableElement
    {
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private AudioSource audioSource;

        public override SwitchableType prefabType{ get { return SwitchableType.Trap; } }

        private void Start()
        {
            Power = power;
        }

        public override void TurnOff() { GetComponent<BoxCollider>().enabled = false; }
        public override void TurnOn() { GetComponent<BoxCollider>().enabled = true; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("I collided with " + other.gameObject.name);

            if (other.GetComponentInParent<IKillable>() != null && !other.GetComponentInParent<IKillable>().isDead)
            {
                ps.Play();
                audioSource.Play();
                Debug.Log(other.gameObject.name);
                other.GetComponentInParent<IKillable>().Die(Vector3.zero);
            }
        }

        [Button]
        void SetColliderPosition()
        {
            GetComponent<BoxCollider>().size = mesh.transform.localScale;
        }
    }
}
