namespace ToyGraf.Models
{
    using System;
    using ToyGraf.Expressions;

    public struct Domain
    {
        public float
            MaxCartesian, MaxPolar,
            MinCartesian, MinPolar;
        public bool
            PolarDegrees, UseGraphWidth;

        public float MaxDegrees { get => PolarDegrees ? MaxPolar : MaxPolar.RadiansToDegrees(); }
        public float MinDegrees { get => PolarDegrees ? MinPolar : MinPolar.RadiansToDegrees(); }

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
