using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for GridPosition
/// </summary>
[Trait("Category", "Unit")]
public class GridPositionTests
{
    [Fact]
    public void Constructor_WithCoordinates_SetsProperties()
    {
        // Arrange
        const int x = 5;
        const int y = 10;

        // Act
        var position = new GridPosition(x, y);

        // Assert
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    [Fact]
    public void Equality_SameCoordinates_AreEqual()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(5, 10);

        // Act
        var result = pos1.Equals(pos2);

        // Assert
        Assert.True(result);
        Assert.Equal(pos1.GetHashCode(), pos2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentCoordinates_AreNotEqual()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(6, 10);

        // Act
        var result = pos1.Equals(pos2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void EqualityOperator_SameCoordinates_ReturnsTrue()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(5, 10);

        // Act
        var result = pos1 == pos2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InequalityOperator_DifferentCoordinates_ReturnsTrue()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(6, 10);

        // Act
        var result = pos1 != pos2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ToString_ReturnsCoordinateString()
    {
        // Arrange
        var position = new GridPosition(5, 10);

        // Act
        var result = position.ToString();

        // Assert
        Assert.Equal("(5, 10)", result);
    }

    [Fact]
    public void GetHashCode_SameCoordinates_ReturnsSameHash()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(5, 10);

        // Act
        var hash1 = pos1.GetHashCode();
        var hash2 = pos2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void Add_ReturnsNewPositionWithAddedCoordinates()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(2, 3);

        // Act
        var result = pos1.Add(pos2);

        // Assert
        Assert.Equal(7, result.X);
        Assert.Equal(13, result.Y);
    }

    [Fact]
    public void ManhattanDistance_CalculatesCorrectDistance()
    {
        // Arrange
        var pos1 = new GridPosition(0, 0);
        var pos2 = new GridPosition(5, 10);

        // Act
        var distance = pos1.ManhattanDistanceTo(pos2);

        // Assert
        Assert.Equal(15, distance); // |5-0| + |10-0| = 5 + 10 = 15
    }

    [Fact]
    public void ManhattanDistance_SamePosition_ReturnsZero()
    {
        // Arrange
        var pos1 = new GridPosition(5, 10);
        var pos2 = new GridPosition(5, 10);

        // Act
        var distance = pos1.ManhattanDistanceTo(pos2);

        // Assert
        Assert.Equal(0, distance);
    }

    [Fact]
    public void AdjacentPositions_ReturnsFourNeighbors()
    {
        // Arrange
        var position = new GridPosition(5, 5);

        // Act
        var adjacent = position.AdjacentPositions;

        // Assert
        Assert.Equal(4, adjacent.Count);
        Assert.Contains(new GridPosition(4, 5), adjacent); // Left
        Assert.Contains(new GridPosition(6, 5), adjacent); // Right
        Assert.Contains(new GridPosition(5, 4), adjacent); // Up
        Assert.Contains(new GridPosition(5, 6), adjacent); // Down
    }
}
