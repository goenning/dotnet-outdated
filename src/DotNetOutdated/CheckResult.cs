using System.Collections.Generic;

namespace DotNetOutdated
{
    public class CheckResult
    {
        public IEnumerable<Dependency> Outdated 
        { 
            get { return this.outdated; }
        }

        public CheckResult()
        {
            this.outdated = new List<Dependency>(); 
        }

        internal void AddOutdated(Dependency dependency)
        {
            this.outdated.Add(dependency);
        }

        private List<Dependency> outdated;
    }
}