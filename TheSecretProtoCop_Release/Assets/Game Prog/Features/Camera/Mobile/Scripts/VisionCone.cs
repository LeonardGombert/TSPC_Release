using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Mobile
{

    public class VisionCone : MonoBehaviour
    {
        [SerializeField] float viewAngle = 360;
        [SerializeField] float viewRadius = 10;
        [SerializeField] float meshResolution = 12;
        [SerializeField] LayerMask obstacleMask;
        [SerializeField] List<Vector3> hitLocations;
        RaycastHit hit;
        float angle;
        Vector3 dir;
        public MeshFilter viewMeshFilter;
        Mesh viewMesh;

        [SerializeField] bool isMovingCam = false;

        private void Start()
        {
            viewMesh = new Mesh();
            viewMeshFilter.mesh = viewMesh;
            RaycastConeOfVision();
            ConstructMesh();
        }

        void RaycastConeOfVision()
        {
            hitLocations.Clear();
            int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
            float stepAngleSize = viewAngle / stepCount;

            for (int i = 0; i <= stepCount; i++)
            {
                angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));

                if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask)) hitLocations.Add(hit.point);
                else hitLocations.Add(dir * viewRadius + new Vector3(transform.position.x, 0, transform.position.z));
            }
        }

        void ConstructMesh()
        { 
            int vertexCount = hitLocations.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(hitLocations[i]);

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            viewMesh.Clear();

            viewMesh.vertices = vertices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }
    }
}

