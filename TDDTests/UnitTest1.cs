using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCollections;

namespace TDDTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd_FromEmptySortedSet()
        {
            var sortedSet = new SortedSet<int>();
            Assert.AreEqual(0u, sortedSet.Count);

            Assert.IsTrue(sortedSet.Add(10));
            Assert.AreEqual(1u, sortedSet.Count);
        }

        [TestMethod]
        public void TestEmptyStringConversion()
        {
            var sortedSet = new SortedSet<int>();
            string stringRepresentation = sortedSet;
            Assert.AreEqual("", stringRepresentation);
        }

        [TestMethod]
        public void TestNonEmptyStringConversion()
        {
            var sortedSet = new SortedSet<int>();
            sortedSet.Add(10);
            Assert.AreEqual("10", sortedSet);
            Assert.IsFalse(sortedSet.Add(10));

            sortedSet.Add(5);
            Assert.AreEqual("5 10", sortedSet);

            sortedSet.Add(6);
            Assert.AreEqual("5 6 10", sortedSet);

            sortedSet.Add(20);
            Assert.AreEqual("5 6 10 20", sortedSet);

            sortedSet.Add(13);
            Assert.AreEqual("5 6 10 13 20", sortedSet);

            sortedSet.Add(14);
            Assert.AreEqual("5 6 10 13 14 20", sortedSet);
        }

        [TestMethod]
        public void TestCollectionContains()
        {
            var sortedSet = new SortedSet<int>();
            sortedSet.Add(10);
            sortedSet.Add(5);
            sortedSet.Add(6);
            sortedSet.Add(20);
            sortedSet.Add(13);
            sortedSet.Add(14);

            Assert.IsTrue(sortedSet.Contains(10));
            Assert.IsTrue(sortedSet.Contains(5));
            Assert.IsTrue(sortedSet.Contains(6));
            Assert.IsTrue(sortedSet.Contains(20));
            Assert.IsTrue(sortedSet.Contains(13));
            Assert.IsTrue(sortedSet.Contains(14));

            for (int i = 0; i < 4; i++)
            {
                Assert.IsFalse(sortedSet.Contains(i));
            }
            for (int i = 21; i < 50; i++)
            {
                Assert.IsFalse(sortedSet.Contains(i));
            }
        }
    }
}
