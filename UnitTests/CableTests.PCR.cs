using System;
using CodingDojo.S02E11.PCR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingDojo.UnitTests
{
    [TestClass]
    public class CableTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CableIdentiques()
        {
            new Cable(1, 1).SeCroiseAvec(new Cable(1, 1));

            Assert.Fail("Ce test doit lever une exception.");
        }

        [TestMethod]
        public void CablesQuiSeCroisent()
        {
            Assert.IsTrue(new Cable(1, 2).SeCroiseAvec(new Cable(2, 1)));
            Assert.IsTrue(new Cable(2, 1).SeCroiseAvec(new Cable(1, 2)));
        }


        [TestMethod]
        public void CablesQuiNeSeCroisentPas()
        {
            Assert.IsTrue(new Cable(1, 1).NeSeCroisePasAvec(new Cable(1, 2)));
            Assert.IsTrue(new Cable(1, 1).NeSeCroisePasAvec(new Cable(2, 2)));
            Assert.IsTrue(new Cable(1, 1).NeSeCroisePasAvec(new Cable(2, 1)));
            Assert.IsTrue(new Cable(1, 2).NeSeCroisePasAvec(new Cable(2, 2)));
            Assert.IsTrue(new Cable(2, 1).NeSeCroisePasAvec(new Cable(2, 2)));
        }
    }
}