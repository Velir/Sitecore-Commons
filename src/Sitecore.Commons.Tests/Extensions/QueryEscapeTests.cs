using NUnit.Framework;
using Sitecore.SharedSource.Commons.Extensions;

namespace Sitecore.SharedSource.Commons.Tests.Extensions
{
	[TestFixture]
	public class QueryEscapeTests
	{


		#region QueryEscape

		[Test]
		public void BestCaseTest()
		{
			string path = "/sitecore/content/Home/Certified Education/Certified Learning/EO_MKM_0512/EO_Activity 0512/Module EAO";
			string escapedPath = path.QueryEscape();
			string expectedPath = "/#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#";
			Assert.AreEqual(expectedPath, escapedPath);
		}

		[Test]
		public void NullTest()
		{
			string path = null;
			string escapedPath = path.QueryEscape();
			Assert.IsNull(escapedPath);
		}

		[Test]
		public void EmptyTest()
		{
			string path = string.Empty;
			string escapedPath = path.QueryEscape();
			Assert.IsNull(escapedPath);
		}

		[Test]
		public void SlashesTest()
		{
			string path = "///";
			string escapedPath = path.QueryEscape();
			string expectedPath = "/";
			Assert.AreEqual(expectedPath, escapedPath);
		}

		[Test]
		public void NoLeadingSlashTest()
		{
			// result should be the same as a properly formatted string
			string path = "sitecore/content/Home/Certified Education/Certified Learning/EO_MKM_0512/EO_Activity 0512/Module EAO";
			string escapedPath = path.QueryEscape();
			string expectedPath = "/#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#";
			Assert.AreEqual(expectedPath, escapedPath);
		}

		[Test]
		public void WithTrailingSlashTest()
		{
			// result should be the same as a properly formatted string
			string path = "/sitecore/content/Home/Certified Education/Certified Learning/EO_MKM_0512/EO_Activity 0512/Module EAO/";
			string escapedPath = path.QueryEscape();
			string expectedPath = "/#sitecore#/#content#/#Home#/#Certified Education#/#Certified Learning#/#EO_MKM_0512#/#EO_Activity 0512#/#Module EAO#";
			Assert.AreEqual(expectedPath, escapedPath);
		}

		#endregion

	}
}
