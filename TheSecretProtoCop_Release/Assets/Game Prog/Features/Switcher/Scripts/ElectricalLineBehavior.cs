using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Gameplay.Mobile
{
    
    public class ElectricalLineBehavior : SwitchableElement
    {

        private Material mat;
        private Color color;
        public List<GameObject> cylinders;
        public override SwitchableType prefabType { get { return SwitchableType.Null; } }

        private void OnEnable()
        {

            mat = new Material(cylinders[0].GetComponent<MeshRenderer>().material);
            color = mat.GetColor("_EmissionColor");
            for (int i = 0; i < cylinders.Count; i++)
            {
                cylinders[i].GetComponent<MeshRenderer>().material = mat;
            }
            
            
            Power = power;
        }

        public override void TurnOff()
        {
            mat.DOColor(color * .4f, "_EmissionColor", .5f);
        }

        public override void TurnOn()
        {
            mat.DOColor(color * 2, "_EmissionColor", .5f);
        }
        public void SwitchNode(int changeNodes)
        {
            throw new System.NotImplementedException();
        }
        
        public void AddCylinder(GameObject cylinder) => cylinders.Add(cylinder);

    }
}

