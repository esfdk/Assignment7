namespace Assignment7
{

    using NUnit.Framework;

    /// <summary>
    /// Test class for the collection classes.
    /// </summary>
    [TestFixture]
    public class TestOfCollections
    {
        /// <summary>
        /// Bag used for testing.
        /// </summary>
        private readonly Bag<int> bag = new Bag<int>();

        /// <summary>
        /// Set used for testing.
        /// </summary>
        private readonly Set<int> set = new Set<int>();

        /// <summary>
        /// List used for testing.
        /// </summary>
        private readonly List<int> list = new List<int>();

        /// <summary>
        /// Runs all the tests.
        /// </summary>
        /// <param name="args">
        /// No arguments needed.
        /// </param>
        public static void Main(string[] args)
        {
            var toC = new TestOfCollections();
            toC.TestBag();
            toC.TestList();
            toC.TestSet();
            toC.TestMap();
        }

        /// <summary>
        /// Tests for the bag class.
        /// </summary>
        [Test]
        public void TestBag()
        {
            Assert.That(bag.IsEmpty());
            bag.Add(3);
            Assert.That(!bag.IsEmpty());
            Assert.AreEqual(1, bag.Size);
            Assert.That(bag.Contains(3));
            bag.Add(5);
            Assert.AreEqual(2, bag.Size);
            bag.Add(3);
            Assert.AreEqual(2, bag.Count(3));
            bag.Remove(3);
            Assert.AreEqual(1, bag.Count(3));
            bag.RemoveAll();
            Assert.That(bag.IsEmpty());
        }

        /// <summary>
        /// Tests for the list class.
        /// </summary>
        [Test]
        public void TestList()
        {
            Assert.That(list.IsEmpty());
            list.Add(3);
            Assert.That(!list.IsEmpty());
            Assert.That(list.Contains(3));
            list.Add(5);
            list.AddAt(3, 1);
            Assert.AreEqual(0, list.FirstIndex(3));
            Assert.AreEqual(1, list.LastIndex(3));
            Assert.AreEqual(2, list.Count(3));
            list.Remove(3);
            Assert.AreEqual(1, list.Count(3));
            Assert.AreEqual(5, list.Get(1));
            list.RemoveAt(0);
            Assert.AreEqual(5, list.Get(0));
            list.RemoveAll();
            Assert.That(list.IsEmpty());
        }

        /// <summary>
        /// Tests for the set class.
        /// </summary>
        [Test]
        public void TestSet()
        {
            Assert.That(set.IsEmpty());
            set.Add(3);
            Assert.That(!set.IsEmpty());
            Assert.That(set.Contains(3));
            set.Add(5);
            set.Add(3);
            Assert.AreEqual(1, set.Count(3));
            set.Remove(3);
            Assert.AreEqual(0, set.Count(3));
            set.RemoveAll();
            Assert.That(set.IsEmpty());
        }

        /// <summary>
        /// Test of the map function.
        /// </summary>
        [Test]
        public void TestMap()
        {

            bag.Add(3);
            bag.Add(5);
            bag.Add(3);
            Bag<double> doubleBag = (Bag<double>)bag.Map<double>(o => o * 10);
            Assert.That(doubleBag.Contains(30) & doubleBag.Contains(50));
            Assert.AreEqual(3, doubleBag.Size);
            bag.RemoveAll();

            list.Add(3);
            list.Add(5);
            list.AddAt(3, 1);
            List<double> doubleList = (List<double>)list.Map<double>(o => o * 10);
            Assert.That(doubleList.Contains(30) & doubleList.Contains(50));
            Assert.AreEqual(3, doubleList.Size);
            list.RemoveAll();

            set.Add(3);
            set.Add(5);
            set.Add(3);
            Set<double> doubleSet = (Set<double>)set.Map<double>(o => o * 10);
            Assert.That(doubleSet.Contains(30) & doubleSet.Contains(50));
            Assert.AreEqual(2, doubleSet.Size);
            set.RemoveAll();
        }
    }
}