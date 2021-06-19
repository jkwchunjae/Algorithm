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
        [InlineData(4, 3, 7, 5, true, 7)]
        [InlineData(4, 3, 4, 4, true, 4)]
        [InlineData(7, 5, 4, 4, true, 12)]
        [InlineData(12, 4, 32, 4, true, 0)]
        [InlineData(13, 4, 33, 4, true, 1)]
        [InlineData(4, 3, 5, 3, false, 0)]
        [InlineData(7, 30, 17, 25, true, 67)]
        [InlineData(7, 30, 18, 25, false, 67)]
        [InlineData(7, 30, 17, 1124, true, 11257)]
        [InlineData(12, 8, 32, 6, true, 20)]
        public void Test_FirstMeet(int e1Base, int e1Term, int e2Base, int e2Term, bool expectedMeet, int expectedFirstMeet)
        {
            var e1 = (e1Base, e1Term);
            var e2 = (e2Base, e2Term);

            var meet = Program.TryGetFirst(e1, e2, out var first);

            Assert.Equal(expectedMeet, meet);
            if (meet)
            {
                Assert.Equal(expectedFirstMeet, first);
            }
        }

        [Theory]
        [InlineData(12, 8, 32, 6, 50, true)]
        [InlineData(12, 8, 32, 6, 40, false)]
        [InlineData(7, 5, 4, 4, 12, true)]
        [InlineData(7, 5, 4, 4, 11, false)]
        [InlineData(12, 43, 32, 6, 97, false)]
        [InlineData(12, 43, 32, 6, 98, true)]
        [InlineData(12, 43, 392, 6, 600, false)]
        [InlineData(12, 43, 392, 6, 700, true)]
        public void Test_IsConnected(int e1Base, int e1Term, int e2Base, int e2Term, int N, bool expectedConnect)
        {
            var e1 = (e1Base, e1Term);
            var e2 = (e2Base, e2Term);
            var connected = Program.IsConnected(e1, e2, N);

            Assert.Equal(expectedConnect, connected);
        }
    }
}
