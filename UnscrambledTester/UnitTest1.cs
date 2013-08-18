using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unscrambled.Domain;
using UnscrambledMVC4.Infrastructure;


namespace UnscrambledTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LeaderboardRepository lr = new LeaderboardRepository();

            lr.GenerateLeaderboards(47);
        }
    }
}
