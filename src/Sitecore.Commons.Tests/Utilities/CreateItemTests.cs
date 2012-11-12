using Sitecore.Data;
using NUnit.Framework;
using Sitecore.SharedSource.Commons.Utilities;

namespace Sitecore.SharedSource.Commons.Tests.Utilities
{
	[TestFixture]
	public class CreateItemTests
	{

		/// <summary>
		/// case 1: item is found, don't create item
		/// case 2: item is not found, create item
		/// </summary>
		[Test]
		public void StandardTest()
		{
			// assemble
			ID parentId = ID.NewID;
			string cleanName = "new item name";
			ID templateId = ID.NewID;
			string dbName = "master";
			bool itemCreated;
			ICreateItem iCreateItemFound = new ItemFound(parentId, cleanName, templateId, dbName);
			ICreateItem iCreateItemNotFound = new ItemNotFound(parentId, cleanName, templateId, dbName);

			// action
			// assert
			ID foundItemId = CreateItemUtil.GetOrCreateItem(iCreateItemFound, out itemCreated);
			Assert.IsFalse(itemCreated);
			Assert.IsNotNull(foundItemId);

			ID createdItemId = CreateItemUtil.GetOrCreateItem(iCreateItemNotFound, out itemCreated);
			Assert.IsTrue(itemCreated);
			Assert.IsNotNull(createdItemId);
		}

		[Test]
		public void NullTest()
		{
			bool itemCreated;

			ID nullItemId = CreateItemUtil.GetOrCreateItem(null, out itemCreated);
			Assert.IsFalse(itemCreated);
			Assert.IsNull(nullItemId);
		}



	}

	public class ItemFound : CreateItem
	{
		public ItemFound(ID parentId, string cleanName, ID templateId, string dbName)
			: base(parentId, cleanName, templateId, dbName)
		{
		}

		public override ID GetChild()
		{
			return ID.NewID;
		}

		public override ID CreateChild()
		{
			return ID.NewID;
		}
	}

	public class ItemNotFound : CreateItem
	{
		public ItemNotFound(ID parentId, string cleanName, ID templateId, string dbName)
			: base(parentId, cleanName, templateId, dbName)
		{
		}

		public override ID GetChild()
		{
			return (ID)null;
		}

		public override ID CreateChild()
		{
			return ID.NewID;
		}
	}


}
