using System.Collections.Generic;
using System.Linq;
using Gecko.NCore.Client.Querying;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gecko.NCore.Client.Tests.Querying
{
	[TestClass]
	public class QueryTranslatorTests
	{
		[TestMethod]
		public void Visit_Take()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().Take(10);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.IsNotNull(queryTranslator.TakeCount);
			Assert.AreEqual(10, queryTranslator.TakeCount.Value);
		}

		[TestMethod]
		public void Visit_Skip()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().Skip(10);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.IsNotNull(queryTranslator.SkipCount);
			Assert.AreEqual(10, queryTranslator.SkipCount.Value);
		}

		[TestMethod]
		public void Visit_Where_B_Equals_Null()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().Where(x => x.B1 == null);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.AreEqual("B1=@", queryTranslator.FilterExpression);
		}

		[TestMethod]
		public void Visit_Where_B_Tittel_Equals_Foo()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().Where(x => x.B1.Tittel == "Foo");

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.AreEqual("B1.Tittel=Foo", queryTranslator.FilterExpression);
		}

		[TestMethod]
		public void Visit_OrderBy_B_Tittel()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().OrderBy(x => x.B1.Tittel);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.AreEqual("@B1.Tittel ASC", queryTranslator.SortExpression);
		}

		[TestMethod]
		public void Visit_OrderByDescending_B_Tittel()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().OrderByDescending(x => x.B1.Tittel);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.AreEqual("@B1.Tittel DESC", queryTranslator.SortExpression);
		}

		[TestMethod]
		public void Visit_OrderByDescending_Id_ThenBy_B_Tittel()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable().OrderByDescending(x => x.Id).ThenBy(x => x.B1.Tittel);

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(queryable.Expression);

			Assert.AreEqual("@Id DESC,@B1.Tittel ASC", queryTranslator.SortExpression);
		}

		[TestMethod]
		public void Visit_Include_Should_Set_RelatedObjects()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable();
			var query = from x in queryable.Include(x => x.B1).Include(x => x.B2).Include(x => x.B1).Include(x => x.B2).Include(x => x.B1).Include(x => x.B2)
						where x.Id == 1
						select x;

			var expression = query.Expression;
			expression = ExpressionEvaluator.PartialEval(expression);
			expression = PredicateOperandAligner.Align(expression);
			expression = PredicateDenormalizer.Denormalize(expression);
			var queryTranslater = new QueryTranslator();
			queryTranslater.Visit(expression);

			Assert.AreEqual(2, queryTranslater.RelatedObjects.Count());
			Assert.AreEqual("B1", queryTranslater.RelatedObjects.FirstOrDefault());
			Assert.AreEqual("B2", queryTranslater.RelatedObjects.LastOrDefault());
		}

		[TestMethod]
		public void Visit_Include_Should_Set_IncludeSelectors()
		{
			var queryable = new List<DummyA>
				                {
									new DummyA { B1 = new DummyB(), B2 = new DummyB() }
								}.AsQueryable();
			var query = from x in queryable.Include(x => x.B1.DummyAs)
						where x.Id == 1
						select x;

			var expression = query.Expression;
			expression = ExpressionEvaluator.PartialEval(expression);
			expression = PredicateOperandAligner.Align(expression);
			expression = PredicateDenormalizer.Denormalize(expression);
			var queryTranslater = new QueryTranslator();
			queryTranslater.Visit(expression);

			Assert.AreEqual(2, queryTranslater.IncludeSelectors.Count());
		}
		
		[TestMethod]
		public void Visit_Untyped_Include_Should_Set_RelatedObjects()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable();
			var query = from x in queryable
						where x.Id == 1
						select x;

			var untypedQuery = (IQueryable) query;
			untypedQuery = untypedQuery.Include("B1").Include("B1.A");

			var expression = untypedQuery.Expression;
			expression = ExpressionEvaluator.PartialEval(expression);
			expression = PredicateOperandAligner.Align(expression);
			expression = PredicateDenormalizer.Denormalize(expression);
			var queryTranslater = new QueryTranslator();
			queryTranslater.Visit(expression);

			Assert.AreEqual(2, queryTranslater.RelatedObjects.Count());
			Assert.AreEqual("B1", queryTranslater.RelatedObjects.FirstOrDefault());
			Assert.AreEqual("B1.A", queryTranslater.RelatedObjects.LastOrDefault());
		}

		[TestMethod]
		public void DirectPredicate_ShouldTranslateTo_DirectReference()
		{
			var queryable = Enumerable.Empty<DummyA>().AsQueryable();

			var query = from x in queryable
						where x.Id == 1 && DirectPredicate<DummyB>.Member(m => m.Id) == 3
						select x;

			var queryTranslator = new QueryTranslator();
			queryTranslator.Visit(query.Expression);

			Assert.AreEqual("Id=1 AND !DummyB.Id=3", queryTranslator.FilterExpression);
		}
	}
}
	