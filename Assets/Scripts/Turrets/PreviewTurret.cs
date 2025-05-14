using TDTest.Combat;
using UnityEngine;

namespace TDTest.Building
{
    public class PreviewTurret : MonoBehaviour
    {
        public TurretSkeleton Skeleton { get; private set; }

        [SerializeField] Transform sphereRange;
        [SerializeField] MeshRenderer sphereRangeRenderer;
        [SerializeField] Transform skeletonHolder;

        public void Initialize(TurretDescription description)
        {
            Skeleton = Instantiate(description.SkeletonPrefab, skeletonHolder);
            sphereRange.localScale = new(description.Radius, description.Radius, description.Radius);
        }
    }
}
