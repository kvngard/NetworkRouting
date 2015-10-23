using System;
using NetworkRouting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void CheckInsertPop()
        {
            PriorityQueue pq = new PriorityQueue(2,10);
            node n = pq.insert(10);
            Assert.AreEqual(n, pq.pop());
        }

        [TestMethod]
        public void CheckUpdate()
        {
            PriorityQueue pq = new PriorityQueue(2, 10);
            pq.update(1, 1, pq.peek());
            pq.update(2, 10, pq.peek());
            Assert.AreEqual(1, pq.pop().value);
            Assert.AreEqual(10, pq.pop().value);
        }
    }
}
