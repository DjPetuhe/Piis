
namespace NelderMead.Structure
{
    public class Point : ICloneable
    {
        public const int DIMENSIONS = 3;
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point() : this(0, 0, 0) { }

        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double EvaluateFunction()
        {
            return -4 * Math.Pow((X - Y), 2) 
                   +2 * Math.Pow(X, 2) * Y
                   -3 * X * Y * Z
                   +7 * Math.Pow(X, 2) * Math.Pow(Z, 2);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static Point operator +(Point a, Point b) 
            => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Point operator -(Point a, Point b) 
            => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Point operator *(Point a, double b) 
            => new(a.X * b, a.Y * b, a.Z * b);

        public static Point operator *(double b, Point a)
            => new(a.X * b, a.Y * b, a.Z * b);

        public static Point operator /(Point a, double b) 
            => new(a.X / b, a.Y / b, a.Z / b);

        public double this[int key]
        {
            get
            {
                return key switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => throw new ArgumentOutOfRangeException(nameof(key))
                };
            }
            set
            {
                switch (key)
                {
                    case 0:
                        X = value;
                        break;
                    case 1: 
                        Y = value;
                        break;
                    case 2: 
                        Z = value;
                        break;
                    default: throw new ArgumentOutOfRangeException(nameof(key));
                }
            }
        }
    }
}
