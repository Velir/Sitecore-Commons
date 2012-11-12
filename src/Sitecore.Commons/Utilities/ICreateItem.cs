using Sitecore.Data;

namespace Sitecore.SharedSource.Commons.Utilities
{
	/// <summary>
	/// Interface for creating a sitecore item
	/// </summary>
	public interface ICreateItem
	{
		#region properties
		ID ParentId { get; set; }
		string CleanName { get; set; }
		ID TemplateId { get; set; }
		string DbName { get; set; }
		#endregion

		#region methods
		ID GetChild();
		ID CreateChild();
		#endregion
	}
}
