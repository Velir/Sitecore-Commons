namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public interface ICustomItem : ISitecoreItem
	{
		string Icon { get; }
		IItem InnerItem { get; }
	}
}