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
            Node[] initialList =
            {
                new Node(4) { Distance = 1 },
                new Node(2) { Distance = 1 },
                new Node(0) { Distance = 5 },
                new Node(1) { Distance = 12 },
                new Node(3) { Distance = 19 },
            };

            PriorityQueue testQueue = new PriorityQueue();
            testQueue.Add(initialList[1]);
            testQueue.Add(initialList[4]);
            testQueue.Add(initialList[0]);
            testQueue.Add(initialList[3]);
            testQueue.Add(initialList[2]);

            Console.WriteLine(testQueue);

            Node[] actual = new Node[5];
            for (int i = 0; i < 5; i++)
            {
                actual[i] = testQueue.Pop();
            }

            Node[] expected =
            {
                initialList[1], initialList[0], initialList[2], initialList[3], initialList[4]
            };

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}