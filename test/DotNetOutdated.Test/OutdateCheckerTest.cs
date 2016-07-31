using System;
using Xunit;c

namespace DotNetOutdated.Test
{
    public class OutdateCheckerTest
    {
        [Fact]
        public void EmptyDependencies()
        {
            var checker = new OutdateChecker();
            var result = checker.Run(new Dependency[0]);
        }
    }
}