namespace ToyGraf.Models.Structs
{
    using ToyGraf.Expressions;

    public struct DomainInfo
    {
        public float
            MaxCartesian, MaxPolar,
            MinCartesian, MinPolar;
        public bool
            PolarDegrees, UseGraphWidth;

        public float MaxDegrees { get => PolarDegrees ? MaxPolar : MaxPolar.RadiansToDegrees(); }
        public float MaxRadians { get => PolarDegrees ? MaxPolar.DegreesToRadians() : MaxPolar; }
        public float MinDegrees { get => PolarDegrees ? MinPolar : MinPolar.RadiansToDegrees(); }
        public float MinRadians { get => PolarDegrees ? MinPolar.DegreesToRadians() : MinPolar; }

        public static bool operator ==(DomainInfo a, DomainInfo b) =>
            a.MaxCartesian == b.MaxCartesian && a.MaxPolar == b.MaxPolar &&
            a.MinCartesian == b.MinCartesian && a.MinPolar == b.MinPolar &&
            a.PolarDegrees == b.PolarDegrees && a.UseGraphWidth == b.UseGraphWidth;

        public static bool operator !=(DomainInfo a, DomainInfo b) => !(a == b);

        public override bool Equals(object obj) => obj is DomainInfo d && this == d;

        public override int GetHashCode() =>
            MaxCartesian.GetHashCode() ^ MaxPolar.GetHashCode() ^
            MinCartesian.GetHashCode() ^ MinPolar.GetHashCode() ^
            PolarDegrees.GetHashCode() ^ UseGraphWidth.GetHashCode();

        public override string ToString() => $@"Cartesian: {
            CartesianDomain}, Polar: {
            (PolarDegrees ? PolarDomainDegrees : PolarDomainRadians)}, UseGraphWidth = {
            UseGraphWidth}";

        private string CartesianDomain => $"{MinCartesian} < x < {MaxCartesian}";
        private string PolarDomainDegrees => $"{MinDegrees} < x < {MaxDegrees}";
        private string PolarDomainRadians => $"{MinRadians} < x < {MaxRadians}";
    }
}
