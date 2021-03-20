using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Gameplay.Mobile
{
    public class TrapBehavior : SwitchableElement
    {
        [SerializeField] private MeshRenderer mesh;
        [SerializeField] private SpriteRenderer icon;
        private Material mat;

        public override SwitchableType prefabType { get { return SwitchableType.Trap; } }

        private void OnEnable()
        {
            mat = new Material(mesh.material);
            mesh.material = mat;
        }

        private void Start() => Power = power;


        public override void TurnOff()
        {
            icon.DOColor(Color.grey, .5f);
            mat.DOColor(Color.red * .4f, "_EmissionColor", .5f);

        }
        public override void TurnOn()
        {
            icon.DOColor(Color.white, .5f);
            mat.DOColor(Color.red * 2, "_EmissionColor", .5f);
        }

    }

}
