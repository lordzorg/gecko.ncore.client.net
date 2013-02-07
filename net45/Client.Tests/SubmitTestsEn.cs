#if false //example-usage
using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.Ephorte.Client;
using Gecko.Ephorte.ServiceModel.Client.Querying;
using Gecko.Ephorte.ServiceModel.Client.ObjectModel.V3.En;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.Ephorte.ServiceModel.Client.Tests
{
	// ReSharper disable InconsistentNaming

	[TestClass]
	public class SubmitTests : ServiceModelTests
	{
		protected override ObjectModelServiceVersion ObjectModelServiceVersion
		{
			get { return ObjectModelServiceVersion.V3En; }
		}

		private ObjectModelService _service = new ObjectModelServiceClient();

		[TestInitialize]
		public void TestInitialize()
		{
			_service = new ObjectModelServiceClient();
		}

		[TestMethod]
		public void Submit_CanUpdateOneCase()
		{
			const int expectedOperationResultsCount = 1;

			//Arrange
			var case1 = EphorteContext.Query<Case>().Where(c => c.Id > 1).First();
			var caseId = case1.Id;
			var newTitle = "new title" + DateTime.Now.ToShortTimeString();
			SetTitle(case1, newTitle);

			//Act
			var changeEntries = new List<ChangeSetEntry>
			                    	{
			                    		new ChangeSetEntry{Operation = EntryOperation.Update, Entity = case1}
			                    	};

			var result = Submit(new ChangeSet {ChangeSetEntries = changeEntries});

			//Assert
			AssertOperationResultCountIs(expectedOperationResultsCount, result);

			var case1Updated = result.OperationResults.First().Entities.First() as Case;
			Assert.IsNotNull(case1Updated);
			Assert.AreEqual(newTitle, case1Updated.Title, "case1Updated.Title");

			//Fetch the case again and verify title
			Assert.AreEqual(newTitle, EphorteContext.Query<Case>().Where(x => x.Id == caseId).First().Title, "_ephorte.Query<Case>().Where(x => x.Id == caseId).First().Title");
		}

		[TestMethod]
		public void Submit_CanInsertRemark()
		{
			var newRemarkText = "Some remarks regarding case. Current time: " + DateTime.Now;

			const int expectedOperationResultsCount = 1;
			
			//Arrange
			var case1 = EphorteContext.Query<Case>().Where(c => c.Id > 1).First();
			var caseId = case1.Id;

			var remarks = new Remarks { CaseId = caseId, Text = newRemarkText };

			//Act
			var changeEntries = new List<ChangeSetEntry> 
			                    	{
			                    		new ChangeSetEntry {Operation = EntryOperation.Insert, Entity = remarks}
			                    	};

			var result = Submit(new ChangeSet { ChangeSetEntries = changeEntries });

			//Assert
			AssertOperationResultCountIs(expectedOperationResultsCount, result);

			var remarkUpdated = result.OperationResults.First().Entities.First() as Remarks;
			Assert.IsNotNull(remarkUpdated);
			Assert.AreEqual(newRemarkText, remarkUpdated.Text, "remarkUpdated.Text");

			//Fetch the last inserted remark on the case 
			var q = from r in EphorteContext.Query<Remarks>()
						//.Include(x => x.Case)
					where r.CaseId == caseId
					orderby r.CreatedDate descending
					select r;
			var lastRemark = q.FirstOrDefault();
			Assert.IsNotNull(lastRemark, "lastRemark");
			Assert.AreEqual(newRemarkText, lastRemark.Text, "lastRemark.Text");
		}

		[TestMethod]
		public void Submit_DeleteUpdateAndAddRemarks()
		{
			var newRemarkText = "Some remarks regarding case. Current time: " + DateTime.Now;

			const int expectedOperationResultsCount = 3;

			//Arrange
			var case1 = EphorteContext.Query<Case>().Where(c => c.Id > 1).First();
			var caseId = case1.Id;

			//first delete all remarks on case
			DeleteAllRemarksOnCase(caseId.Value);

			//Add two remarks which can be changed and deleted
			var remark1 = new Remarks { CaseId = caseId, Text = newRemarkText + " 1"};
			var remark2 = new Remarks { CaseId = caseId, Text = newRemarkText + " 2"};

			var result = Submit(new ChangeSet { ChangeSetEntries 
				= new List<ChangeSetEntry>
				  	{
				  		new ChangeSetEntry {Operation = EntryOperation.Insert, Entity = remark1},
				  		new ChangeSetEntry {Operation = EntryOperation.Insert, Entity = remark2}
				  	} });
			var inserted = result.OperationResults.SelectMany(entryResult => entryResult.Entities).ToList();
			
			//Act

			var changedRemarkText = "Changed remark " + DateTime.Now;
			var insertRemarkText = "Inserted remark" + DateTime.Now;
			
			var deleteRemark = (Remarks)inserted[0];
			var changeRemark = (Remarks)inserted[1]; changeRemark.Text = changedRemarkText;
			var insertRemark = new Remarks { CaseId = caseId, Text = insertRemarkText };

			result = Submit(new ChangeSet
			{
				ChangeSetEntries
					= new List<ChangeSetEntry>
				  	{
				  		new ChangeSetEntry {Operation = EntryOperation.Delete, Entity = deleteRemark},
				  		new ChangeSetEntry {Operation = EntryOperation.Update, Entity = changeRemark},
				  		new ChangeSetEntry {Operation = EntryOperation.Insert, Entity = insertRemark},
				  	}
			});
			//inserted = result.OperationResults.SelectMany(entryResult => entryResult.Entities).ToList();

			//Assert
			AssertOperationResultCountIs(expectedOperationResultsCount, result);

			//Fetch the remarks on the case 
			var remarks = (from r in EphorteContext.Query<Remarks>()
			               where r.CaseId == caseId
			               orderby r.CreatedDate descending
			               select r).ToList();
			Assert.AreEqual(2, remarks.Count, "remarks.Count");
			Assert.AreEqual(1, remarks.Where(x => x.Text == changedRemarkText).Count());
			Assert.AreEqual(1, remarks.Where(x => x.Text == insertRemarkText).Count());
		}

		[TestMethod]
		public void Submit_CanUpdateOneCaseAndInsertRemark()
		{
			var newCaseTitle = "new title" + DateTime.Now.ToShortTimeString();
			var newRemarkText = "Some remarks regarding case " + newCaseTitle;
			
			const int expectedOperationResultsCount = 2;
			
			//Arrange
			var case1 = EphorteContext.Query<Case>().Where(c => c.Id > 1).First();
			var caseId = case1.Id;
			SetTitle(case1, newCaseTitle);

			var remark = new Remarks {CaseId = case1.Id, Text = newRemarkText};

			//Act
			var changeEntries = new List<ChangeSetEntry>
			                    	{
			                    		new ChangeSetEntry {Operation = EntryOperation.Update, Entity = case1},
			                    		new ChangeSetEntry {Operation = EntryOperation.Insert, Entity = remark}
			                    	};

			var result = Submit(new ChangeSet {ChangeSetEntries = changeEntries});

			//Assert
			AssertOperationResultCountIs(expectedOperationResultsCount, result);

			var case1Updated = result.OperationResults.First().Entities.First() as Case;
			Assert.IsNotNull(case1Updated);
			Assert.AreEqual(newCaseTitle, case1Updated.Title, "case1Updated.Title");

			//Fetch the last inserted remark on the case 
			var q = from r in EphorteContext.Query<Remarks>()
						.Include(x => x.Case)
			        where r.CaseId == caseId orderby r.CreatedDate descending 
			        select r;
			var lastRemark = q.FirstOrDefault();
			Assert.IsNotNull(lastRemark, "lastRemark");
			Assert.AreEqual(newRemarkText, lastRemark.Text, "lastRemark.Text");
			Assert.IsNotNull(lastRemark.Case, "lastRemark.Case");
			Assert.AreEqual(newCaseTitle, lastRemark.Case.Title, "lastRemark.Text");
		}

		private void DeleteAllRemarksOnCase(int caseId)
		{
			var remarks = EphorteContext.Query<Remarks>().Where(r => r.CaseId == caseId);

			Submit(new ChangeSet
			{
				ChangeSetEntries = new List<ChangeSetEntry>(remarks.Select(r => new ChangeSetEntry { Operation = EntryOperation.Delete, Entity = r }))
			});
		}

		private static void AssertOperationResultCountIs(int expected, ChangeSetResult result)
		{
			Assert.IsNotNull(result, "result");
			Assert.IsNotNull(result.OperationResults, "result.OperationResults");
			Assert.AreEqual(expected, result.OperationResults.Count(), "result.OperationResults.Count()");
		}


		private static void SetTitle(Case case1, string newTitle)
		{
			case1.Title = newTitle;
			case1.PublicTitle = newTitle;
			case1.PublicTitleNames = newTitle;
		}

		private ChangeSetResult Submit(ChangeSet changeSet)
		{
			return _service.Submit(_identity, changeSet);
		}
	}
}
#endif