using System;

namespace Interface_Example // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle { Radius = 5 };
            var square = new Square { SideLength = 10 };
            var triangle = new Triangle { Base = 8, Height = 6 };

            List<IShape> shapes = new List<IShape> { circle, square, triangle };
            try
            {
                foreach (var shape in shapes)
                {
                    Console.WriteLine($"Area: {shape.CalculateArea()}");
                    Console.WriteLine($"Perimeter: {shape.CalculatePerimeter()}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }
        public interface IShape
        {
            double CalculateArea();
            double CalculatePerimeter();
        }
        public class Circle : IShape
        {
            public double Radius { get; set; }

            public double CalculateArea()
            {
                return Math.PI * Math.Pow(Radius, 2);
            }

            public double CalculatePerimeter()
            {
                return 2 * Math.PI * Radius;
            }
        }

        public class Square : IShape
        {
            public double SideLength { get; set; }

            public double CalculateArea()
            {
                return Math.Pow(SideLength, 2);
            }

            public double CalculatePerimeter()
            {
                return 4 * SideLength;
            }
        }

        public class Triangle : IShape
        {
            public double Base { get; set; }
            public double Height { get; set; }

            public double CalculateArea()
            {
                return 0.5 * Base * Height;
            }

            public double CalculatePerimeter()
            {
                // Not implemented for triangles
                throw new NotImplementedException();
            }
        }

    }
}