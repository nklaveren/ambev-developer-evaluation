using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects;

public class ValueObjectsTests
{
    [Fact(DisplayName = "Value objects should be equals when all properties are the same")]
    public void ValueObjects_ShouldBeEqualsWhenAllPropertiesAreTheSame()
    {
        var valueObject1 = new ValueObjectStub(1, "Property 1");
        var valueObject2 = new ValueObjectStub(1, "Property 1");
        Assert.Equal(valueObject1, valueObject2);
        Assert.True(valueObject1.Equals(valueObject2));
        Assert.Equal(valueObject1.GetHashCode(), valueObject2.GetHashCode());
        Assert.Equal(valueObject1.ToString(), valueObject2.ToString());
    }

    [Fact(DisplayName = "Value objects should not be equals when any property is different")]
    public void ValueObjects_ShouldNotBeEqualsWhenAnyPropertyIsDifferent()
    {
        var valueObject1 = new ValueObjectStub(1, "Property 1");
        var valueObject2 = new ValueObjectStub(2, "Property 1");
        Assert.NotEqual(valueObject1, valueObject2);
        Assert.True(valueObject1 != valueObject2);
        Assert.False(valueObject1.Equals(valueObject2));
        Assert.NotEqual(valueObject1.GetHashCode(), valueObject2.GetHashCode());
        Assert.NotEqual(valueObject1.ToString(), valueObject2.ToString());
    }

    [Fact(DisplayName = "Value objects should have the same hash code when all properties are the same")]
    public void ValueObjects_ShouldHaveTheSameHashCodeWhenAllPropertiesAreTheSame()
    {
        var valueObject1 = new ValueObjectStub(1, "Property 1");
        var valueObject2 = new ValueObjectStub(1, "Property 1");
        Assert.Equal(valueObject1.GetHashCode(), valueObject2.GetHashCode());
    }

    [Fact(DisplayName = "Value objects should not be equals when first is null")]
    public void ValueObjects_ShouldNotBeEqualsWhenFirstIsNull()
    {
        var valueObject1 = new ValueObjectStub(1, "Property 1");
        ValueObjectStub? valueObject2 = null;
        Assert.False(valueObject1.Equals(valueObject2));
    }

}

public class ValueObjectStub : ValueObject
{
    public ValueObjectStub(int property1, string property2)
    {
        Property1 = property1;
        Property2 = property2;
    }

    public int Property1 { get; }
    public string Property2 { get; } = string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Property1;
        yield return Property2;
    }

    public override string ToString()
    {
        return $"{Property1} {Property2}";
    }
}