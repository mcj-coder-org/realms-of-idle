using RealmsOfIdle.Core.Infrastructure;

var rng = new DeterministicRng(12345);

Console.WriteLine($"Seed: {rng.Seed}");
Console.WriteLine($"Next(10): {rng.Next(10)}");
Console.WriteLine($"Next(1, 100): {rng.Next(1, 100)}");
Console.WriteLine($"NextDouble(): {rng.NextDouble()}");
Console.WriteLine($"NextBool(): {rng.NextBool()}");
Console.WriteLine($"NextBool(0.3): {rng.NextBool(0.3)}");

var rng2 = rng.WithOffset(100);
Console.WriteLine($"WithOffset(100) - Next(10): {rng2.Next(10)}");

Console.WriteLine("DeterministicRng compiles and runs successfully!");
