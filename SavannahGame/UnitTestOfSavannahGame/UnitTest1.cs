using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using SavannahGame;
namespace UnitTestOfSavannahGame
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_If_400_Fields_Are_Created()
        {
            CountData cd = CountData.Getinstance();

            int test = cd.CountField();

            int expected = 400;

            Assert.AreEqual(expected, test);

        }
    }
}
