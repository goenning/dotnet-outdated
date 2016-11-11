using NuGet.Versioning;
using System;

namespace DotNetOutdated
{
    public class Dependency : IEquatable<Dependency>
    {
        public string Name { get; private set; }
        public SemanticVersion CurrentVersion { get; private set; }

        public Dependency(string name, string current)
            : this(name, SemanticVersion.Parse(current))
        {
        }

        public Dependency(string name, SemanticVersion current)
        {
            this.Name = name;
            this.CurrentVersion = current;
        }
        
        public bool Equals(Dependency other)
        {
            if(other == null) return false;

            return Name == other.Name &&
                CurrentVersion == other.CurrentVersion;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as Dependency);
        }

        public override int GetHashCode()
        {
            unchecked {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ CurrentVersion.GetHashCode();
                return hashCode;
            }
        }
    }
}