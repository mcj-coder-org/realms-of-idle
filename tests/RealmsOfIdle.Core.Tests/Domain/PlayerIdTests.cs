using RealmsOfIdle.Core.Domain;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Domain;

/// <summary>
/// Unit tests for PlayerId
/// </summary>
[Trait("Category", "Unit")]
public class PlayerIdTests
{
    [Fact]
    public void Constructor_WithValue_SetsValueProperty()
    {
        // Arrange
        const string expectedValue = "player123";

        // Act
        var playerId = new PlayerId(expectedValue);

        // Assert
        Assert.Equal(expectedValue, playerId.Value);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        // Arrange
        const string value = "player456";
        var playerId = new PlayerId(value);

        // Act
        var result = playerId.ToString();

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void Equals_SameValue_ReturnsTrue()
    {
        // Arrange
        const string value = "player789";
        var id1 = new PlayerId(value);
        var id2 = new PlayerId(value);

        // Act
        var result = id1.Equals(id2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_DifferentValue_ReturnsFalse()
    {
        // Arrange
        var id1 = new PlayerId("player1");
        var id2 = new PlayerId("player2");

        // Act
        var result = id1.Equals(id2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_Null_ReturnsFalse()
    {
        // Arrange
        var id = new PlayerId("player1");

        // Act
        var result = id.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        // Arrange
        var id = new PlayerId("player1");

        // Act
        var result = id.Equals(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetHashCode_SameValue_ReturnsSameHashCode()
    {
        // Arrange
        const string value = "player999";
        var id1 = new PlayerId(value);
        var id2 = new PlayerId(value);

        // Act
        var hash1 = id1.GetHashCode();
        var hash2 = id2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void EqualityOperator_SameValue_ReturnsTrue()
    {
        // Arrange
        const string value = "player111";
        var id1 = new PlayerId(value);
        var id2 = new PlayerId(value);

        // Act
        var result = id1 == id2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void InequalityOperator_SameValue_ReturnsFalse()
    {
        // Arrange
        const string value = "player222";
        var id1 = new PlayerId(value);
        var id2 = new PlayerId(value);

        // Act
        var result = id1 != id2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ImplicitStringConversion_ReturnsValue()
    {
        // Arrange
        const string value = "player333";
        var playerId = new PlayerId(value);

        // Act
        string result = playerId;

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public void ExplicitStringConversion_CreatesPlayerId()
    {
        // Arrange
        const string value = "player444";

        // Act
        var playerId = (PlayerId)value;

        // Assert
        Assert.Equal(value, playerId.Value);
    }
}
