using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stundenplan.Domain.Universal;
using Xunit;

namespace Stundenplan.Tests
{
    public class TestCreateEmail
    {
        [Fact]
        public void NoAtInEmail()
        {
            Exception ex = Assert.Throws<ParseEmailException>(() => new Email("eineEmailOhneAtexample.com"));
            Assert.Equal(ParseEmailException.NO_AT, ex.Message);
        }
    }
}
