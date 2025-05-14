using TDTest.Combat;
using UnityEngine;

namespace TDTest.Building
{
    public class PreviewTurret : MonoBehaviour
    {
        public TurretSkeleton Skeleton { get; private set; }
        
        public void Initialize(TurretDescription description)
        {
            Skeleton = Instantiate(description.SkeletonPrefab);
        }
    }
}
