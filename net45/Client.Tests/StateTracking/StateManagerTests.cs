using System.Linq;
using Gecko.NCore.Client.StateTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Gecko.NCore.Client.Tests.StateTracking
{
    [TestClass]
    public class StateManagerTests
    {
        private IQueryProvider _queryProvider;
        private StateManager _stateManager;
        private TrackableObject _trackableObject;

        [TestInitialize]
        public void TestInitialize()
        {
            _queryProvider = MockRepository.GenerateStub<IQueryProvider>();
            _stateManager = new StateManager(_ => _queryProvider);
            _trackableObject = new TrackableObject();
        }

        [TestMethod]
        public void AddShouldAddStateEntryAdded()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Add(_trackableObject);
            
            Assert.AreEqual(1, _stateManager.Entries.Count(x => x.DataObject == _trackableObject && x.State == DataObjectState.Added));
        }

        [TestMethod]
        public void AddShouldAssignEphorteContextContainer()
        {
            _stateManager.Add(_trackableObject);

            Assert.AreEqual(_queryProvider, _trackableObject.DataObjectCollection.QueryProvider);
        }

        [TestMethod]
        public void RemoveAddedObject()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Add(_trackableObject);
            _stateManager.Remove(_trackableObject);

            Assert.AreEqual(0, _stateManager.Entries.Count());
        }

        [TestMethod]
        public void RemoveAttachedObject()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Attach(_trackableObject);
            _stateManager.Remove(_trackableObject);

            Assert.AreEqual(1, _stateManager.Entries.Count(x => x.DataObject == _trackableObject && x.State == DataObjectState.Removed));
        }

        [TestMethod]
        public void RemoveWeaklyAttachedObject()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.WeakAttach(_trackableObject);
            _stateManager.Remove(_trackableObject);

            Assert.AreEqual(1, _stateManager.Entries.Count(x => x.DataObject == _trackableObject && x.State == DataObjectState.Removed));
        }

        [TestMethod]
        public void AttachShouldAddStateEntry()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Attach(_trackableObject);

            Assert.AreEqual(1, _stateManager.Entries.Count(x => x.DataObject == _trackableObject && x.State == DataObjectState.Modified));
        }

        [TestMethod]
        public void AttachShouldSetEphorteContextContainers()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Attach(_trackableObject);

            Assert.AreEqual(_queryProvider, _trackableObject.DataObjectCollection.QueryProvider);
        }

        [TestMethod]
        public void DetatchObjectShouldRemoveStateEntry()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Add(_trackableObject);
            _stateManager.Detach(_trackableObject);

            Assert.AreEqual(0, _stateManager.Entries.Count(x => x.DataObject == _trackableObject));
        }

        [TestMethod]
        public void DetatchObjectShouldClearEphorteContextContainers()
        {
            _trackableObject.TrackableProperty = "Testing";
            _stateManager.Add(_trackableObject);
            _stateManager.Detach(_trackableObject);

            Assert.IsNull(_trackableObject.DataObjectCollection.QueryProvider);
        }

        
        [TestMethod]
        public void WeakAttachObjectShouldNotAddStateEntry()
        {
            _stateManager.WeakAttach(_trackableObject);

            Assert.AreEqual(0, _stateManager.Entries.Count(x => x.DataObject == _trackableObject));
        }

        [TestMethod]
        public void WeakAttachThenModifyObjectShouldAddStateEntry()
        {
            _stateManager.WeakAttach(_trackableObject);
            _trackableObject.TrackableProperty = "Testing";
            Assert.AreEqual(1, _stateManager.Entries.Count(x => x.DataObject == _trackableObject && x.State == DataObjectState.Modified));
        }

        [TestMethod]
        public void WeakAttachObjectShouldSetEphorteContextContainers()
        {
            _stateManager.WeakAttach(_trackableObject);

            Assert.AreEqual(_queryProvider, _trackableObject.DataObjectCollection.QueryProvider);
        }
    }
}
