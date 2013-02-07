using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Gecko.NCore.Client.ObjectModel.V2;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InconsistentNaming

namespace Gecko.NCore.Client.Tests
{
	/// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BasicLinqTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        FakeEphorteContext _queryContext;

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void TestInitialize()
        {
            _queryContext = new FakeEphorteContext();
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WhenServiceReturnsOneSak_ResultReturnsOneSak()
        {
            const int expectedSakId = 1001;
            _queryContext.Results.Enqueue(new List<Sak> { new Sak { Id = expectedSakId } });
			var list = _queryContext.Query<Sak>().Where(s => s.Id == 1).ToList();
			Assert.AreEqual(1, list.Count());
			Assert.AreEqual(expectedSakId, list.First().Id);


            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=1", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void HandlesMultipleCriteriasOnField()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, SaksstatusId = "R" } };
            _queryContext.Results.Enqueue(expectedResult);

            var requiredStatus = new[] { "R", "F" };

			var list = (from s in _queryContext.Query<Sak>()
                        where s.SaksstatusId == string.Join(",", requiredStatus) && s.JournalEnhetId == "enhetsid"
                        select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("SaksstatusId=R,F AND JournalEnhetId=enhetsid", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

		[TestMethod]
		public void QueryUsingContainsString()
		{
			_queryContext.Results.Enqueue(new List<Journalpost> { new Journalpost { Id = 1 } });

			var status = new List<string> { "I", "U" };

			var query = from jp in _queryContext.Query<Journalpost>()
						where status.Contains(jp.JournalstatusId)
						select jp;

			var list = query.ToList();

			Assert.AreEqual(1, list.Count());

			var q = _queryContext.QueryArguments[0];
			Assert.AreEqual("Journalpost", q.DataObjectName, "DataObjectName");
			Assert.AreEqual("JournalstatusId=I,U", q.FilterExpression, "FilterExpression");
		}

		[TestMethod]
		public void QueryUsingContainsStringThatContainsNoItems()
		{
			_queryContext.Results.Enqueue(new List<Journalpost> { new Journalpost { Id = 1 } });

			var status = new List<string>().AsEnumerable();

			var query = from jp in _queryContext.Query<Journalpost>()
						where status.Contains(jp.JournalstatusId)
						select jp;

			var list = query.ToList();

			var q = _queryContext.QueryArguments[0];
			Assert.AreEqual("Journalpost", q.DataObjectName, "DataObjectName");
			Assert.AreEqual("JournalstatusId=@", q.FilterExpression, "FilterExpression");
		}

		[TestMethod]
		public void QueryUsingContainsInt()
		{
			var status = new List<int> { 1, 2 }.AsEnumerable();

			_queryContext.Results.Enqueue(new List<Journalpost> { new Journalpost { Id = 1 } });

			var query = from jp in _queryContext.Query<Journalpost>()
						where status.Contains(jp.Id.Value)
						select jp;

			var list = query.ToList();

			Assert.AreEqual(1, list.Count());
			var q = _queryContext.QueryArguments[0];
			Assert.AreEqual("Journalpost", q.DataObjectName, "DataObjectName");
			Assert.AreEqual("Id=1,2", q.FilterExpression, "FilterExpression");
		}

		[TestMethod]
		public void HandlesDateCriteria()
		{
			var expectedDate = new DateTime(2010, 01, 31);
			var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = expectedDate } };
			_queryContext.Results.Enqueue(expectedResult);

			var list = (from s in _queryContext.Query<Sak>()
                        where s.Saksdato == expectedDate
                        select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Saksdato=31.01.2010 00:00:00", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }


        [TestMethod]
        public void HandlesDateCriteria_QueryForNull()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

			var list = (from s in _queryContext.Query<Sak>()
                        where s.Saksdato == null
                        select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Saksdato=@", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void HandlesDateCriteria_QueryForNotNull()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

			var list = (from s in _queryContext.Query<Sak>()
                        where s.Saksdato != null
                        select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Saksdato!=@", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }


        [TestMethod]
        public void HandlesDateCriteria_GreaterThen()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

            var startDate = new DateTime(2008, 1, 31);
			var list = (from s in _queryContext.Query<Sak>() where s.Saksdato > startDate select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Saksdato>31.01.2008 00:00:00", query.FilterExpression, "FilterExpression");
        }

        [TestMethod]
        public void HandlesDateCriteria_GreaterThenOrEqual()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

            var startDate = new DateTime(2008, 1, 31);
			var list = (from s in _queryContext.Query<Sak>() where s.Saksdato >= startDate select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Saksdato>=31.01.2008 00:00:00", query.FilterExpression, "FilterExpression");
        }

        [TestMethod]
        public void HandlesDateCriteria_LessThen()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

            var startDate = new DateTime(2008, 1, 31);
			var list = (from s in _queryContext.Query<Sak>() where s.Saksdato < startDate select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Saksdato<31.01.2008 00:00:00", query.FilterExpression, "FilterExpression");
        }

        [TestMethod]
        public void HandlesDateCriteria_LessThenOrEqual()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

            var startDate = new DateTime(2008, 1, 31);
			var list = (from s in _queryContext.Query<Sak>() where s.Saksdato <= startDate select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Saksdato<=31.01.2008 00:00:00", query.FilterExpression, "FilterExpression");
        }

        [TestMethod]
        public void HandlesDateCriteria_Interval()
        {
            var expectedResult = new List<Sak> { new Sak { Id = 1001, Saksdato = DateTime.Today } };
            _queryContext.Results.Enqueue(expectedResult);

            var startDate = new DateTime(2008, 1, 31);
            var endDate = new DateTime(2009, 1, 1);
			var list = (from s in _queryContext.Query<Sak>()
                        where s.Saksdato > startDate && s.Saksdato < endDate
                        select s).ToList();

            Assert.AreEqual(expectedResult.Count, list.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Saksdato>31.01.2008 00:00:00 AND Saksdato<01.01.2009 00:00:00", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SimpleWhere_SkipTenTake5()
        {
            const int expectedSakId = 1001;
			_queryContext.Results.Enqueue(GetSakerWithIds(expectedSakId));

			var list = _queryContext.Query<Sak>().Where(s => s.Id == 1 && (s.SaksansvarligPersonId == 3)).OrderBy(s => s.Tittel).ThenBy(s => s.Saksdato).Skip(10).Take(5).ToList();
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(expectedSakId, list.First().Id);

            Assert.AreEqual(1, _queryContext.QueryArguments.Count);

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName);
			Assert.AreEqual("@Tittel ASC,@Saksdato ASC", query.SortExpression);
            Assert.AreEqual("Id=1 AND SaksansvarligPersonId=3", query.FilterExpression);
            Assert.AreEqual(10, query.SkipCount);
            Assert.AreEqual(5, query.TakeCount);
        }

        [TestMethod]
        public void SimpleWhere_OrderBy()
        {
			var list = (from sak in _queryContext.Query<Sak>() where sak.Id == 1 && sak.SaksansvarligPersonId == 3 orderby sak.Tittel, sak.Saksdato select sak).ToList();

            //_queryContext.WriteQueryCommandsToConsole();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName);
            Assert.AreEqual("@Tittel ASC,@Saksdato ASC", query.SortExpression);
            Assert.AreEqual("Id=1 AND SaksansvarligPersonId=3", query.FilterExpression);
            Assert.AreEqual(null, query.SkipCount);
            Assert.AreEqual(null, query.TakeCount);
        }

		[TestMethod]
        public void WhenDoingACountQuery_CountIsHandled()
        {
			_queryContext.Results.Enqueue(GetSakerWithIds(1, 2));

			var count = _queryContext.Query<Sak>().Where(s => s.Id == 1).Count();
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void WhenServiceReturnsOneSak_ResultReturnsOneSak_Linq()
        {
            const int expectedSakId = 1001;
			_queryContext.Results.Enqueue(GetSakerWithIds(expectedSakId));

			var list = (from s in _queryContext.Query<Sak>() where s.Id == expectedSakId select s).ToList();
            Assert.AreEqual(1, list.Count());
            Assert.AreEqual(expectedSakId, list.First().Id);
        }

        [TestMethod]
        public void QueryUsingProjection1()
        {
        	const int expectedSaId = 3;
			_queryContext.Results.Enqueue(GetSakerWithIds(expectedSaId));

			var query = from s in _queryContext.Query<Sak>()
                        select new { MyId = s.Id };

			Assert.AreEqual(expectedSaId, query.First().MyId);
        }

		[TestMethod]
		public void QueryUsingProjection2()
		{
			const int expectedSaId = 3;
			_queryContext.Results.Enqueue(GetSakerWithIds(expectedSaId));

			var query = from s in _queryContext.Query<Sak>()
						select s.Id;

			Assert.AreEqual(expectedSaId, query.First());
		}

        private static IEnumerable<Sak> GetSakerWithIds(params int[] requiredIds)
        {
            return requiredIds.Select(id => new Sak { Id = id });
        }


        [TestMethod]
        public void QueryForJpWithRelatedSak()
        {
			var query = from jp in _queryContext.Query<Journalpost>()
                            .Include(j => j.Sak)
                            .Include(j => j.Saksbehandler)
                        where jp.Id == 1
                        select jp;

            query.ToList();

            var qq = _queryContext.QueryArguments[0];
            Assert.AreEqual("Journalpost", qq.DataObjectName);
			Assert.AreEqual("Id=1", qq.FilterExpression);
			Assert.AreEqual("Sak,Saksbehandler", string.Join(",", qq.RelatedObjects.ToArray()));
        }

        [TestMethod]
        public void QueryForJpWithRelatedSakWithCriteriaOnSakField()
        {
			var query = from jp in _queryContext.Query<Journalpost>()
                            .Include(x => x.Sak.JournalEnhet)
                            .Include(x => x.Saksbehandler)
                        where jp.Id == 1 && jp.Sak.SaksansvarligPersonId == 3
                        select jp;

            var list = query.ToList();

			var qq = _queryContext.QueryArguments[0];
			Assert.AreEqual("Journalpost", qq.DataObjectName);
			Assert.AreEqual("Id=1 AND Sak.SaksansvarligPersonId=3", qq.FilterExpression);
			Assert.AreEqual("Sak,Sak.JournalEnhet,Saksbehandler", string.Join(",", qq.RelatedObjects.ToArray()));
		}

        [TestMethod]
        public void SearchForPersonnavnWherePersonIdIs_PN_ID()
        {
            _queryContext.Results.Enqueue(new List<Personnavn> { new Personnavn() });

			var list = (from personnavn in _queryContext.Query<Personnavn>()
                                                           where personnavn.Id == QueryContext.Current.ActiveUserNameId
                                                           select personnavn).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Personnavn", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|PN_ID|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForPersonWherePersonIdIs_PE_ID()
        {
            _queryContext.Results.Enqueue(new List<Person> { new Person() });

			var list = Enumerable.ToList<Person>((from person in _queryContext.Query<Person>()
                                                       where person.Id == QueryContext.Current.ActiveUserId
                                                       select person));

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Person", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|PE_ID|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForAdministrativEnhetWhereIdIs_AI_ID()
        {
            _queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var list = (from person in _queryContext.Query<AdministrativEnhet>()
                        where person.Id == QueryContext.Current.ActiveAdministrativeUnitId
                        select person).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("AdministrativEnhet", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|AI_ID|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForAdministrativEnhetWhereIdIs_AI_UND()
        {
            _queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var list = ((from enhet in _queryContext.Query<AdministrativEnhet>()
                                                                  where QueryContext.Current.ActiveAdministrativeUnitSubHierarchyIds.Contains(enhet.Id.Value)
                                                                  select enhet)).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("AdministrativEnhet", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|AI_UND|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForAdministrativEnhetWhereIdIs_AI_RED()
        {
            _queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var list = ((from enhet in _queryContext.Query<AdministrativEnhet>()
                                                                  where QueryContext.Current.WritableAdministrativeUnitIds.Contains(enhet.Id.Value)
                                                                  select enhet)).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("AdministrativEnhet", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|AI_RED|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForJournalEnhetWhereIdIs_JE_JENHET()
        {
            _queryContext.Results.Enqueue(new List<JournalEnhet> { new JournalEnhet() });

			var list = ((from enhet in _queryContext.Query<JournalEnhet>()
                                                            where enhet.Id == QueryContext.Current.ActiveRegistryManagementUnitId
                                                            select enhet)).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("JournalEnhet", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("Id=|JE_JENHET|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void SearchForSak_AI_FULL()
        {
            _queryContext.Results.Enqueue(new List<Sak> { new Sak() });

			var list = ((from enhet in _queryContext.Query<Sak>()
                                                   where QueryContext.Current.ActiveAdministrativeUnitHierarchyIds.Contains(enhet.SaksansvarligEnhetId.Value)
                                                   select enhet)).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.IsNull(query.SortExpression, "SortExpression");
            Assert.AreEqual("SaksansvarligEnhetId=|AI_FULL|", query.FilterExpression, "FilterExpression");
            Assert.IsNull(query.SkipCount, "SkipCount");
            Assert.IsNull(query.TakeCount, "TakeCount");
        }

        [TestMethod]
        public void TestQueryOnDateFormattedInEnglish()
        {
            _queryContext.Results.Enqueue(new List<Sak> { new Sak() });

            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var currentUICulture = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            var date = new DateTime(2008, 1, 1);

			var list = (from s in _queryContext.Query<Sak>() where s.Saksdato == date select s).ToList();

            var query = _queryContext.QueryArguments[0];

            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
            
            Assert.AreEqual("Saksdato=01.01.2008 00:00:00", query.FilterExpression);
        }

        [TestMethod]
        public void String_StartsWith_EndsWith_Contains()
        {
            _queryContext.Results.Enqueue(new List<Sak> { new Sak() });

			var list = ((from sak in _queryContext.Query<Sak>()
                                                   where sak.Tittel.StartsWith("STARTSWITH") || sak.Tittel.EndsWith("ENDSWITH") || sak.Tittel.Contains("CONTAINS")
                                                   select sak)).ToList();

            var query = _queryContext.QueryArguments[0];
            Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
            Assert.AreEqual("Tittel=STARTSWITH* OR Tittel=*ENDSWITH OR Tittel=*CONTAINS*", query.FilterExpression, "FilterExpression");
        }

		[TestMethod]
		public void SaveChanges_ShouldRaise_SavingChanges()
		{
			var eventHandlerRaised = false;

			_queryContext.SavingChanges += (sender, e) => eventHandlerRaised = true;
			_queryContext.SaveChanges();

			Assert.IsTrue(eventHandlerRaised);
		}

		[TestMethod]
		public void SaveChanges_ShouldRaise_SavedChanges()
		{
			var eventHandlerRaised = false;

			_queryContext.SavedChanges += (sender, e) => eventHandlerRaised = true;
			_queryContext.SaveChanges();

			Assert.IsTrue(eventHandlerRaised);
		}
	}
}

// ReSharper restore InconsistentNaming

