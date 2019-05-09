namespace ToyGraf.Models
{
    using System;

    public struct Domain
    {
        public float
            MaxCartesian, MaxPolar,
            MinCartesian, MinPolar;
        public bool
            PolarDegrees, UseGraphWidth;

        private const float piOver180 = (float)Math.PI / 180;

        public float MaxDegrees { get => MaxPolar / (PolarDegrees ? 1 : piOver180); }
        public float MaxRadians { get => MaxPolar * (PolarDegrees ? piOver180 : 1); }
        public float MinDegrees { get => MinPolar / (PolarDegrees ? 1 : piOver180); }
        public float MinRadians { get => MinPolar * (PolarDegrees ? piOver180 : 1); }

        public static bool operator ==(Domain a, Domain b) =>
            a.MaxCartesian == b.MaxCartesian && a.MaxPolar == b.MaxPolar &&
            a.MinCartesian == b.MinCartesian && a.MinPolar == b.MinPolar &&
            a.PolarDegrees == b.PolarDegrees && a.UseGraphWidth == b.UseGraphWidth;

        public static bool operator !=(Domain a, Domain b) => !(a == b);

        public override bool Equals(object obj) => obj is Domain d && this == d;

        public override int GetHashCode() =>
            MaxCartesian.GetHashCode() ^ MaxPolar.GetHashCode() ^
            MinCartesian.GetHashCode() ^ MinPolar.GetHashCode() ^
            PolarDegrees.GetHashCode() ^ UseGraphWidth.GetHashCode();
    }
}
