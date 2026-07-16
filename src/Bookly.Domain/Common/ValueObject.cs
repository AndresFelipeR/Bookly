namespace Blookly.Domain.Common;

public abstract class ValueObject
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if(obj == null || obj.GetType() != GetType())// validacion de null y que sean del mismo tipo
         return false;

        var valor= (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valor.GetEqualityComponents());
        
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach ( var component in GetEqualityComponents())
        {
            hashCode.Add(component);
        }

        return hashCode.ToHashCode();
    }
}
