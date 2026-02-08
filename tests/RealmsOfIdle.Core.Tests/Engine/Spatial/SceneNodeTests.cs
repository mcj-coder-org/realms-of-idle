using RealmsOfIdle.Core.Engine.Spatial;
using Xunit;

namespace RealmsOfIdle.Core.Tests.Engine.Spatial;

/// <summary>
/// Unit tests for SceneNode
/// </summary>
[Trait("Category", "Unit")]
public class SceneNodeTests
{
    [Fact]
    public void Constructor_WithNameAndPosition_CreatesNode()
    {
        // Arrange
        const string name = "kitchen";
        var position = new GridPosition(5, 10);

        // Act
        var node = new SceneNode(name, position);

        // Assert
        Assert.Equal(name, node.Name);
        Assert.Equal(position, node.Position);
    }

    [Fact]
    public void Constructor_WithAreaId_SetsAreaId()
    {
        // Arrange
        const string areaId = "main_hall";

        // Act
        var node = new SceneNode("entrance", new GridPosition(0, 5), areaId);

        // Assert
        Assert.Equal(areaId, node.AreaId);
    }

    [Fact]
    public void Constructor_WithoutAreaId_UsesEmptyString()
    {
        // Act
        var node = new SceneNode("test", new GridPosition(0, 0));

        // Assert
        Assert.Equal(string.Empty, node.AreaId);
    }

    [Fact]
    public void Equality_SameNameAndPosition_AreEqual()
    {
        // Arrange
        var node1 = new SceneNode("kitchen", new GridPosition(5, 10), "area1");
        var node2 = new SceneNode("kitchen", new GridPosition(5, 10), "area2");

        // Act
        var result = node1.Equals(node2);

        // Assert
        Assert.True(result);
        Assert.Equal(node1.GetHashCode(), node2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentName_AreNotEqual()
    {
        // Arrange
        var node1 = new SceneNode("kitchen", new GridPosition(5, 10));
        var node2 = new SceneNode("bar", new GridPosition(5, 10));

        // Act
        var result = node1.Equals(node2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Equality_DifferentPosition_AreNotEqual()
    {
        // Arrange
        var node1 = new SceneNode("kitchen", new GridPosition(5, 10));
        var node2 = new SceneNode("kitchen", new GridPosition(6, 10));

        // Act
        var result = node1.Equals(node2);

        // Assert
        Assert.False(result);
    }
}
