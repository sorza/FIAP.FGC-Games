namespace Fgc.Domain.Compartilhado.Entidades
{
    public abstract class Entidade(Guid id): IEquatable<Guid>, IEquatable<Entidade>
    {       
        public Guid Id { get; init; } = id;
        public DateTime DataCriacao { get; } = DateTime.UtcNow;
        public DateTime? DataAlteracao { get; set; }
        public bool Equals(Entidade? other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }
        public bool Equals(Guid other) => Id == other;

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Entidade)obj);
        }

        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Entidade? left, Entidade? right) => Equals(left, right);
        public static bool operator !=(Entidade? left, Entidade? right) => !Equals(left, right);
    }
}
