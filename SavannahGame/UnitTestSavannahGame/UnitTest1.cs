using System;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestSavannahGame
{
    [TestClass]
    public class UnitTest1
    {
    
        [TestMethod]
        public void Check_If_400_Fields_Is_Created_In_GameLogic()
        {

            GameLogic gl = GameLogic.Getinstance();
            CountData ct = CountData.Getinstance();

            gl.StartGame(10, 10, 10);


            int expected = 400;
            int methodToTest = ct.CountFields();

            Assert.AreEqual(expected, methodToTest);


        }
    }
}
