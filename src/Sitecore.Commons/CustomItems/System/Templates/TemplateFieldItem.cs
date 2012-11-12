using CustomItemGenerator.Fields.LinkTypes;
using CustomItemGenerator.Fields.SimpleTypes;
using Sitecore.Data.Items;

namespace Sitecore.SharedSource.Commons.CustomItems.System.Templates
{
	public partial class TemplateFieldItem : CustomItem
	{

		#region Boilerplate CustomItem Code

		public TemplateFieldItem(Item innerItem)
			: base(innerItem)
		{
		}

		public static implicit operator TemplateFieldItem(Item innerItem)
		{
			return innerItem != null ? new TemplateFieldItem(innerItem) : null;
		}

		public static implicit operator Item(TemplateFieldItem customItem)
		{
			return customItem != null ? customItem.InnerItem : null;
		}

		#endregion //Boilerplate CustomItem Code


		#region Field Instance Methods


		public CustomLookupField FieldSourceGenerator
		{
			get
			{
				return new CustomLookupField(InnerItem, InnerItem.Fields["Field Source Generator"]);
			}
		}

		public CustomTextField Source
		{
			get
			{
				return new CustomTextField(InnerItem, InnerItem.Fields["Source"]);
			}
		}

		#endregion //Field Instance Methods
	}
}
