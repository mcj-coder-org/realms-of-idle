namespace RealmsOfIdle.Core.Infrastructure;

using System;

/// <summary>
/// Deterministic random number generator that produces repeatable sequences based on a seed.
/// Useful for testing scenarios where you need predictable random results.
/// </summary>
public class DeterministicRng
{
    private readonly int _seed;
    private readonly Random _random;

    /// <summary>
    /// Initializes a new instance of the DeterministicRng class with the specified seed.
    /// </summary>
    /// <param name="seed">The seed value to initialize the random number generator.</param>
    public DeterministicRng(int seed)
    {
        _seed = seed;
        _random = new Random(seed);
    }

    /// <summary>
    /// Gets the seed value used to initialize the random number generator.
    /// </summary>
    public int Seed => _seed;

    /// <summary>
    /// Returns a non-negative random integer that is less than the specified maximum.
    /// </summary>
    /// <param name="maxValue">The exclusive upper bound of the random number to be generated.</param>
    /// <returns>A non-negative random integer that is less than maxValue.</returns>
    public int Next(int maxValue)
    {
        return _random.Next(maxValue);
    }

    /// <summary>
    /// Returns a random integer that is within a specified range.
    /// </summary>
    /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
    /// <param name="maxValue">The exclusive upper bound of the random number returned.</param>
    /// <returns>A random integer that is greater than or equal to minValue and less than maxValue.</returns>
    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }

    /// <summary>
    /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
    /// </summary>
    /// <returns>A random floating-point number that is greater than or equal to 0.0, and less than 1.0.</returns>
    public double NextDouble()
    {
        return _random.NextDouble();
    }

    /// <summary>
    /// Fills the elements of a specified array of bytes with random numbers.
    /// </summary>
    /// <param name="buffer">The array to be filled with random numbers.</param>
    public void NextBytes(byte[] buffer)
    {
        _random.NextBytes(buffer);
    }

    /// <summary>
    /// Returns a random boolean value.
    /// </summary>
    /// <returns>A random boolean value.</returns>
    public bool NextBool()
    {
        return _random.Next(2) == 1;
    }

    /// <summary>
    /// Returns a random boolean value based on a probability threshold.
    /// </summary>
    /// <param name="probability">The probability of returning true (0.0 to 1.0).</param>
    /// <returns>true if a random number is less than probability; otherwise, false.</returns>
    public bool NextBool(double probability)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(probability, 0.0);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(probability, 1.0);

        return _random.NextDouble() < probability;
    }

    /// <summary>
    /// Creates a new DeterministicRng instance with an offset from the current seed.
    /// Useful for generating multiple independent but deterministic sequences.
    /// </summary>
    /// <param name="offset">The offset value to add to the current seed.</param>
    /// <returns>A new DeterministicRng instance with the adjusted seed.</returns>
    public DeterministicRng WithOffset(int offset)
    {
        return new DeterministicRng(_seed + offset);
    }
}
