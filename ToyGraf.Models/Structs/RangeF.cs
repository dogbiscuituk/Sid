namespace ToyGraf.Models.Structs
{
    public struct RangeF
    {
        public RangeF(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float Min, Max;
    }
}
