using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Gecko.NCore.Client.ObjectModel.V2;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests
{
	[TestClass]
	public class BasicLinqTests
	{
		FakeEphorteContext _queryContext;

		[TestInitialize]
		public void TestInitialize()
		{
			_queryContext = new FakeEphorteContext();
		}

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
		public void HandlesMultipleNumberCriteriaOnField()
		{
			var expectedResult = new List<Sak> { new Sak { Id = 1001, SaksstatusId = "R" } };
			_queryContext.Results.Enqueue(expectedResult);

			var requiredId = new List<int?> { 1001, 1002, 1003 };

			var list = (from s in _queryContext.Query<Sak>()
						where requiredId.Contains(s.Id) && s.JournalEnhetId == "enhetsid"
						select s).ToList();

			Assert.AreEqual(expectedResult.Count, list.Count);

			var query = _queryContext.QueryArguments[0];
			Assert.AreEqual("Sak", query.DataObjectName, "DataObjectName");
			Assert.IsNull(query.SortExpression, "SortExpression");
			Assert.AreEqual("Id=1001,1002,1003 AND JournalEnhetId=enhetsid", query.FilterExpression, "FilterExpression");
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

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

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
			var query2 = 
				from sak in _queryContext.Query<Sak>() 
				where sak.Id == 1 && sak.SaksansvarligPersonId == 3 
				orderby sak.Tittel, sak.Saksdato 
				select sak;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query2.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("Sak", queryArguments.DataObjectName);
			Assert.AreEqual("@Tittel ASC,@Saksdato ASC", queryArguments.SortExpression);
			Assert.AreEqual("Id=1 AND SaksansvarligPersonId=3", queryArguments.FilterExpression);
			Assert.AreEqual(null, queryArguments.SkipCount);
			Assert.AreEqual(null, queryArguments.TakeCount);
		}

		[TestMethod]
		public void WhenDoingACountQuery_CountIsHandled()
		{
			_queryContext.Results.Enqueue(GetSakerWithIds(1, 2));

			var count = _queryContext.Query<Sak>().Count(s => s.Id == 1);
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

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

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

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var qq = _queryContext.QueryArguments[0];
			Assert.AreEqual("Journalpost", qq.DataObjectName);
			Assert.AreEqual("Id=1 AND Sak.SaksansvarligPersonId=3", qq.FilterExpression);
			Assert.AreEqual("Sak,Sak.JournalEnhet,Saksbehandler", string.Join(",", qq.RelatedObjects.ToArray()));
		}

		[TestMethod]
		public void SearchForPersonnavnWherePersonIdIs_PN_ID()
		{
			_queryContext.Results.Enqueue(new List<Personnavn> { new Personnavn() });

			var query = 
				from personnavn in _queryContext.Query<Personnavn>()
				where personnavn.Id == QueryContext.Current.ActiveUserNameId
				select personnavn;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArgument = _queryContext.QueryArguments[0];
			Assert.AreEqual("Personnavn", queryArgument.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArgument.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|PN_ID|", queryArgument.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArgument.SkipCount, "SkipCount");
			Assert.IsNull(queryArgument.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForPersonWherePersonIdIs_PE_ID()
		{
			_queryContext.Results.Enqueue(new List<Person> { new Person() });

			var query = 
				from person in _queryContext.Query<Person>()
				where person.Id == QueryContext.Current.ActiveUserId
				select person;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("Person", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|PE_ID|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForAdministrativEnhetWhereIdIs_AI_ID()
		{
			_queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var query = 
				from person in _queryContext.Query<AdministrativEnhet>()
				where person.Id == QueryContext.Current.ActiveAdministrativeUnitId
				select person;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("AdministrativEnhet", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|AI_ID|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForAdministrativEnhetWhereIdIs_AI_UND()
		{
			_queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var query = 
				from enhet in _queryContext.Query<AdministrativEnhet>()
				where QueryContext.Current.ActiveAdministrativeUnitSubHierarchyIds.Contains(enhet.Id.Value)
				select enhet;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("AdministrativEnhet", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|AI_UND|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForAdministrativEnhetWhereIdIs_AI_RED()
		{
			_queryContext.Results.Enqueue(new List<AdministrativEnhet> { new AdministrativEnhet() });

			var query = 
				from enhet in _queryContext.Query<AdministrativEnhet>()
				where QueryContext.Current.WritableAdministrativeUnitIds.Contains(enhet.Id.Value)
				select enhet;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("AdministrativEnhet", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|AI_RED|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForJournalEnhetWhereIdIs_JE_JENHET()
		{
			_queryContext.Results.Enqueue(new List<JournalEnhet> { new JournalEnhet() });

			var query = 
				from enhet in _queryContext.Query<JournalEnhet>()
				where enhet.Id == QueryContext.Current.ActiveRegistryManagementUnitId
				select enhet;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("JournalEnhet", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("Id=|JE_JENHET|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void SearchForSak_AI_FULL()
		{
			_queryContext.Results.Enqueue(new List<Sak> { new Sak() });

			var query = 
				from enhet in _queryContext.Query<Sak>()
				where QueryContext.Current.ActiveAdministrativeUnitHierarchyIds.Contains(enhet.SaksansvarligEnhetId.Value)
				select enhet;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("Sak", queryArguments.DataObjectName, "DataObjectName");
			Assert.IsNull(queryArguments.SortExpression, "SortExpression");
			Assert.AreEqual("SaksansvarligEnhetId=|AI_FULL|", queryArguments.FilterExpression, "FilterExpression");
			Assert.IsNull(queryArguments.SkipCount, "SkipCount");
			Assert.IsNull(queryArguments.TakeCount, "TakeCount");
		}

		[TestMethod]
		public void TestQueryOnDateFormattedInEnglish()
		{
			_queryContext.Results.Enqueue(new List<Sak> { new Sak() });

			var currentCulture = Thread.CurrentThread.CurrentCulture;
			var currentUiCulture = Thread.CurrentThread.CurrentUICulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			var date = new DateTime(2008, 1, 1);

			var query = 
				from s in _queryContext.Query<Sak>() 
				where s.Saksdato == date 
				select s;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];

			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentUiCulture;
			
			Assert.AreEqual("Saksdato=01.01.2008 00:00:00", queryArguments.FilterExpression);
		}

		[TestMethod]
		public void String_StartsWith_EndsWith_Contains()
		{
			_queryContext.Results.Enqueue(new List<Sak> { new Sak() });

			var query = 
				from sak in _queryContext.Query<Sak>()
				where sak.Tittel.StartsWith("STARTSWITH") || sak.Tittel.EndsWith("ENDSWITH") || sak.Tittel.Contains("CONTAINS")
				select sak;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed
			query.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

			var queryArguments = _queryContext.QueryArguments[0];
			Assert.AreEqual("Sak", queryArguments.DataObjectName, "DataObjectName");
			Assert.AreEqual("Tittel=STARTSWITH* OR Tittel=*ENDSWITH OR Tittel=*CONTAINS*", queryArguments.FilterExpression, "FilterExpression");
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

