using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using WebToolkit.Common.Extensions;
using Xunit;

namespace WebToolkit.Tests
{
    public class QueryableExtensionTests
    {
        [Fact]
        public void IncludeMany_Includes()
        {
            var testQueryable = new List<IncludeTestClass>().AsQueryable();

            testQueryable = testQueryable.IncludeMany(builder => builder
                .AddExpression(a => a.ChildMember)
                .AddExpression(a => a.ChildMember1)
                .AddExpression(a => a.ChildMember2));

            Assert.IsAssignableFrom<IIncludableQueryable<IncludeTestClass, object>>(testQueryable);
        }

        public class IncludeTestClass
        {
            public IncludeTestClass ChildMember { get; set; }
            public IncludeTestClass ChildMember1 { get; set; }
            public IncludeTestClass ChildMember2 { get; set; }
        }
    }
}