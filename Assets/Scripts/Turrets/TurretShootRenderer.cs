using System;
using TDTest.Time;
using UnityEngine;

namespace TDTest.Combat
{
    public class TurretShootRenderer : MonoBehaviour
    {
        [SerializeField] Transform muzzle;
        [SerializeField] LineRenderer lineRenderer;

        Timer showTimer;

        public void ShootAt(Transform target)
        {
            var positions = new Vector3[]
            {
                muzzle.position,
                target.position,
            };
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;

            showTimer.Start(0.1f);
        }
        
        void Start()
        {
            showTimer = new()
            {
                IsRepeating = false,
                IsScaled = true,
            };
            showTimer.OnComplete += HideLine;

            lineRenderer.useWorldSpace = true;
            lineRenderer.enabled = false;
        }

        void OnDestroy()
        {
            showTimer.OnComplete -= HideLine;
        }

        void HideLine()
        {
            lineRenderer.enabled = false;
        }
    }
}
