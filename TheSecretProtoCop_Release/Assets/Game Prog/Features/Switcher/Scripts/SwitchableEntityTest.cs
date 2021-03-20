using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SwitchableEntityTest : SwitchableElement
    { 
        [SerializeField] private Color offColor;
        [SerializeField] private Color onColor;

        public override SwitchableType prefabType => throw new System.NotImplementedException();

        private void OnEnable()
        {
            Power = power;
        }

        public override void TurnOff() { GetComponent<Image>().color = offColor; }
        public override void TurnOn() { GetComponent<Image>().color = onColor; }

        public void SwitchNode(int changeNodes)
        {
            throw new System.NotImplementedException();
        }
    }
}

