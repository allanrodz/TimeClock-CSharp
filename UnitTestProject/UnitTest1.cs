using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleApp18;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
       

        [TestMethod]
        public void TestMethod1()
        {

                checkEmployeeNumber cen = new checkEmployeeNumber();

                bool actual = cen.checkEmployeeNo(1010);

                bool expected = true;

                Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void TestMethod2()
        {

            TimeRegistering tg = new TimeRegistering();

                    
            string actual = tg.selectStartHour(1010);

            string expected = "19:27";

            Assert.AreEqual(expected, actual);

        }
    }
}
