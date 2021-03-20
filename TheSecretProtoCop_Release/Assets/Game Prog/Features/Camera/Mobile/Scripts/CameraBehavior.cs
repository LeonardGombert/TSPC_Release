using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Gameplay.Mobile
{
    public class CameraBehavior : SwitchableElement
    {
        //[SerializeField] private LineRenderer visionLine;
        [SerializeField] private Color color;
        [ SerializeField] private GameObject visionCone;
        private Material visionConeMat;
        [SerializeField] private GameObject visualCam;
        private Material visualCamMat;

        public override SwitchableType prefabType { get { return SwitchableType.StaticCamera; } }


        private void OnEnable()
        {
            visionConeMat = new Material(visionCone.GetComponent<MeshRenderer>().material);
            visionCone.GetComponent<MeshRenderer>().material = visionConeMat;

            visualCamMat = new Material(visualCam.GetComponent<MeshRenderer>().material);
            visualCam.GetComponent<MeshRenderer>().material = visualCamMat;
            //color = GetComponent<Image>().color;
        }

        private void Start() => Power = power;

        public override void TurnOff() 
        {
            visionConeMat.DOColor(Color.black * 0, "ScanLineColor", .5f);
            visionConeMat.DOFloat(0, "Speed1", .5f);
            visionConeMat.DOFloat(0, "Speed2", .5f);

            visualCamMat.DOColor(color, "_EmissionColor", .5f);
        }

        public override void TurnOn()
        {
            visionConeMat.DOColor(Color.red * 2, "ScanLineColor", .5f);
            visionConeMat.DOFloat(0.01f, "Speed1", .5f);
            visionConeMat.DOFloat(0.02f, "Speed2", .5f);

            visualCamMat.DOColor(color * 3, "_EmissionColor", .5f);
        }
    }
}

