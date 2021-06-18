using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    public class UnitTest_Problem
    {
        [Theory]
        [InlineData(3, 4, 5, 7, true, 7)]
        public void Test_FirstMeet(int e1Term, int e1Base, int e2Term, int e2Base, bool expectedMeet, int expectedFirstMeet)
        {
            var e1 = (e1Base, e1Term);
            var e2 = (e2Base, e2Term);

            var meet = Program.TryGetFirstMeet(e1, e2, out var firstMeet);

            Assert.Equal(expectedMeet, meet);
            if (meet)
            {
                Assert.Equal(expectedFirstMeet, firstMeet);
            }
        }
    }
}
