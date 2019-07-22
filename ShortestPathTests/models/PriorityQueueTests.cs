using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ShortestPath.models.Tests
{
    [TestClass()]
    public class PriorityQueueTests
    {
        [TestMethod()]
        public void AddAndPopTest()
        {
            Node[] realSortedNodes =
            {
                new Node(2) { Distance = 1 },
                new Node(0) { Distance = 5 },
                new Node(1) { Distance = 12 },
                new Node(3) { Distance = 19 }
            };

            PriorityQueue testQueue = new PriorityQueue();
            testQueue.Add(realSortedNodes[1]);
            testQueue.Add(realSortedNodes[2]);
            testQueue.Add(realSortedNodes[0]);
            testQueue.Add(realSortedNodes[3]);

            Console.WriteLine(testQueue);

            Node[] sortedNodes = new Node[4];
            for (int i = 0; i < 4; i++)
            {
                sortedNodes[i] = testQueue.Pop();
            }

            CollectionAssert.AreEqual(realSortedNodes, sortedNodes);
        }
    }
}