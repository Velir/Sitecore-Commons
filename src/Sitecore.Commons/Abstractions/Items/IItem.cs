using System;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Locking;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Security.AccessControl;
using Version = Sitecore.Data.Version;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public interface IItem : ISitecoreItem
	{
		ItemAccess Access { get; }

		ItemAppearance Appearance { get; }

		ItemAxes Axes { get; }

		BranchItem Branch { get; }

		BranchItem[] Branches { get; }

		ID BranchId { get; set; }

		ChildList Children { get; }

		ItemEditing Editing { get; }

		bool Empty { get; }

		FieldCollection Fields { get; }

		bool HasChildren { get; }

		ItemHelp Help { get; }

		ItemData InnerData { get; }

		bool IsEditing { get; }

		string Key { get; }

		Language Language { get; }

		Language[] Languages { get; }

		ItemLinks Links { get; }

		ItemLocking Locking { get; }

		BranchItem Master { get; }

		ID MasterID { get; set; }

		BranchItem[] Masters { get; }

		bool Modified { get; }

		ID OriginatorId { get; }

		Item Parent { get; }

		ID ParentID { get; }

		ItemPath Paths { get; }

		ItemPublishing Publishing { get; }

		ItemRuntimeSettings RuntimeSettings { get; }

		ItemSecurity Security { get; set; }

		ItemState State { get; }

		ItemStatistics Statistics { get; 		}

		object SyncRoot { get; }

		TemplateItem Template { get; }

		ID TemplateID { get; set; }

		string TemplateName { get; }

		ItemUri Uri { get; }

		Version Version { get; }

		ItemVersions Versions { get; }

		ItemVisualization Visualization { get; }

		void ChangeTemplate(TemplateItem template);

		IItem Add(string name, BranchItem branch);
		IItem Add(string name, BranchId branchId);
		IItem Add(string name, TemplateItem template);
		IItem Add(string name, TemplateID templateID);
	
		IItem Clone(Item item);
		IItem Clone(ID cloneID, Database ownerDatabase);
		
		IItem CopyTo(Item destination, string copyName);
		IItem CopyTo(Item destination, string copyName, ID copyID, bool deep);
		
		void Delete();
		
		int DeleteChildren();
		
		IItem Duplicate();
		IItem Duplicate(string copyName);
		
		ChildList GetChildren();
		ChildList GetChildren(ChildListOptions options);
		
		string GetOuterXml(bool includeSubitems);
		
		void MoveTo(Item destination);
		
		void Paste(string xml, bool changeIDs, PasteMode mode);
		
		ISitecoreItem PasteItem(string xml, bool changeIDs, PasteMode mode);
		
		Guid Recycle();
		
		int RecycleChildren();
		
		void Reload();
		
		string GetUniqueId();
	}
}