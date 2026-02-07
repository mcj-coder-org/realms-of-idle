using RealmsOfIdle.Core.Infrastructure;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Infrastructure;

/// <summary>
/// Unit tests for DeterministicRng
/// </summary>
public class DeterministicRngTests
{
    [Fact]
    public void Constructor_WithSeed_SetsSeedProperty()
    {
        // Arrange
        const int expectedSeed = 42;

        // Act
        var rng = new DeterministicRng(expectedSeed);

        // Assert
        Assert.Equal(expectedSeed, rng.Seed);
    }

    [Fact]
    public void Determinism_SameSeed_ProducesIdenticalSequences()
    {
        // Arrange
        const int seed = 12345;
        var rng1 = new DeterministicRng(seed);
        var rng2 = new DeterministicRng(seed);

        // Act
        var values1 = new int[10];
        var values2 = new int[10];
        for (int i = 0; i < 10; i++)
        {
            values1[i] = rng1.Next(100);
            values2[i] = rng2.Next(100);
        }

        // Assert
        Assert.Equal(values1, values2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void Next_MaxValue_ReturnsValueInRange(int maxValue)
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var result = rng.Next(maxValue);

        // Assert
        Assert.InRange(result, 0, maxValue - 1);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(5, 15)]
    [InlineData(-100, 100)]
    [InlineData(1000, 2000)]
    public void Next_MinValueMaxValue_ReturnsValueInRange(int minValue, int maxValue)
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var result = rng.Next(minValue, maxValue);

        // Assert
        Assert.InRange(result, minValue, maxValue - 1);
    }

    [Fact]
    public void NextDouble_ReturnsValueInUnitRange()
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var result = rng.NextDouble();

        // Assert
        Assert.InRange(result, 0.0, 1.0);
        Assert.True(result >= 0.0 && result < 1.0);
    }

    [Fact]
    public void NextBytes_FillsEntireBuffer()
    {
        // Arrange
        var rng = new DeterministicRng(42);
        var buffer = new byte[100];

        // Act
        rng.NextBytes(buffer);

        // Assert
        // Verify not all bytes are zero (extremely unlikely for all 100 bytes)
        bool hasNonZero = false;
        foreach (byte b in buffer)
        {
            if (b != 0)
            {
                hasNonZero = true;
                break;
            }
        }
        Assert.True(hasNonZero, "Buffer should contain non-zero bytes");
    }

    [Fact]
    public void NextBool_ReturnsBoolean()
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var result = rng.NextBool();

        // Assert
        Assert.IsType<bool>(result);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void NextBool_WithValidProbability_DoesNotThrow(double probability)
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act & Assert
        var exception = Record.Exception(() => rng.NextBool(probability));
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(-1.0)]
    [InlineData(1.1)]
    [InlineData(2.0)]
    public void NextBool_WithInvalidProbability_ThrowsArgumentOutOfRangeException(double probability)
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => rng.NextBool(probability));
    }

    [Fact]
    public void NextBool_WithProbabilityZero_AlwaysReturnsFalse()
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var results = new bool[100];
        for (int i = 0; i < 100; i++)
        {
            results[i] = rng.NextBool(0.0);
        }

        // Assert
        Assert.DoesNotContain(results, x => x);
    }

    [Fact]
    public void NextBool_WithProbabilityOne_AlwaysReturnsTrue()
    {
        // Arrange
        var rng = new DeterministicRng(42);

        // Act
        var results = new bool[100];
        for (int i = 0; i < 100; i++)
        {
            results[i] = rng.NextBool(1.0);
        }

        // Assert
        Assert.All(results, x => Assert.True(x));
    }

    [Fact]
    public void WithOffset_CreatesInstanceWithAdjustedSeed()
    {
        // Arrange
        const int baseSeed = 100;
        const int offset = 50;
        var rng = new DeterministicRng(baseSeed);

        // Act
        var offsetRng = rng.WithOffset(offset);

        // Assert
        Assert.Equal(baseSeed + offset, offsetRng.Seed);
    }

    [Fact]
    public void WithOffset_CreatesIndependentInstance()
    {
        // Arrange
        const int seed = 42;
        var rng1 = new DeterministicRng(seed);
        var rng2 = rng1.WithOffset(1);

        // Act
        var value1 = rng1.Next(1000);
        var value2 = rng2.Next(1000);

        // Assert
        // Different seeds should produce different first values
        Assert.NotEqual(value1, value2);
    }

    [Fact]
    public void WithOffset_Determinism_SameOffsetProducesSameResult()
    {
        // Arrange
        const int seed = 42;
        const int offset = 10;
        var rng1 = new DeterministicRng(seed);
        var rng2 = new DeterministicRng(seed);

        // Act
        var offsetRng1 = rng1.WithOffset(offset);
        var offsetRng2 = rng2.WithOffset(offset);
        var value1 = offsetRng1.Next(1000);
        var value2 = offsetRng2.Next(1000);

        // Assert
        Assert.Equal(value1, value2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1000)]
    public void WithOffset_WithVariousOffsets_AdjustsSeedCorrectly(int offset)
    {
        // Arrange
        const int baseSeed = 50;
        var rng = new DeterministicRng(baseSeed);

        // Act
        var offsetRng = rng.WithOffset(offset);

        // Assert
        Assert.Equal(baseSeed + offset, offsetRng.Seed);
    }
}
