using TDTest.Structural;

namespace TDTest.Combat
{
    public struct EnemySpawnEvent
    {
        public EnemyDescription Description { get; private set; }
        public Structure Structure { get; private set; }

        public EnemySpawnEvent(EnemyDescription description, Structure structure)
        {
            Description = description;
            Structure = structure;
        }
    }
}
