namespace NewTankio.Gameplay.Player
{
    public static class SpringUtils
    {
        private const float Eps = 1e-5f;
        private const float Ln2 = 0.69314718056f;

        public static float HalflifeToDamping(float halflife)
        {
            return (4.0f + Ln2) / (halflife + Eps);
        }

        public static float FastNegExp(float x)
        {
            return 1.0f / (1.0f + x + 0.48f * x * x + 0.235f * x * x * x);
        }
    }
}
