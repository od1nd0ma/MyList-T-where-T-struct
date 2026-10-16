using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices.Marshalling;

namespace повторяюшарп
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumericBox<int> boxInt32 = new();
            NumericBox<double> boxDouble = new();
            for (int i = 0; i < 10; i++)
            {
                boxInt32.Add(Random.Shared.Next(1,123));
                boxDouble.Add(Math.Round(Random.Shared.Next(1,123) + Random.Shared.NextDouble(),2));
            }
            boxInt32.Sort();
            boxDouble.Sort();
            PrintBox(boxInt32);
            PrintBox(boxDouble);
            void PrintBox<T>(NumericBox<T> box) where T : struct , IComparable<T>
            {
                for (int i = 0; i < box.Count; i++)
                    Console.Write($"{box[i]}\t");
                Console.WriteLine();
            }
        }
    }
}