using System;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Locking;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Security.AccessControl;
using Sitecore.SharedSource.Commons.Abstractions.Databases;
using Version = Sitecore.Data.Version;

namespace Sitecore.SharedSource.Commons.Abstractions.Items
{
	public class ItemWrapper : IItem
	{
		private readonly Item _item;

		public ItemWrapper(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			_item = item;
		}

		public virtual IItemFactory ItemFactory
		{
			get { return Items.ItemFactory.Instance; }
		}

		public IDatabaseFactory DatabaseFactory
		{
			get { return Databases.DatabaseFactory.Instance; }
		}

		public virtual string this[string fieldName]
		{
			get { return _item[fieldName]; }
			set { _item[fieldName] = value; }
		}

		public virtual string this[int index]
		{
			get { return _item[index]; }
			set { _item[index] = value; }
		}

		public virtual string this[ID fieldID]
		{
			get { return _item[fieldID]; }
			set { _item[fieldID] = value; }
		}

		public virtual IItem Add(string name, BranchItem branch)
		{
			return ItemFactory.BuildItem(_item.Add(name, branch));
		}

		public virtual IItem Add(string name, BranchId branchId)
		{
			return ItemFactory.BuildItem(_item.Add(name, branchId));
		}

		public virtual IItem Add(string name, TemplateItem template)
		{
			return ItemFactory.BuildItem(_item.Add(name, template));
		}

		public virtual IItem Add(string name, TemplateID templateID)
		{
			return ItemFactory.BuildItem(_item.Add(name, templateID));
		}

		public virtual void BeginEdit()
		{
			_item.Editing.BeginEdit();
		}

		public virtual void ChangeTemplate(TemplateItem template)
		{
			_item.ChangeTemplate(template);
		}

		public virtual IItem Clone(Item item)
		{
			return ItemFactory.BuildItem(_item.Clone(item));
		}

		public virtual IItem Clone(ID cloneID, Database ownerDatabase)
		{
			return ItemFactory.BuildItem(_item.Clone(cloneID, ownerDatabase));
		}

		public virtual IItem CopyTo(Item destination, string copyName)
		{
			return ItemFactory.BuildItem(_item.CopyTo(destination, copyName));
		}

		public virtual IItem CopyTo(Item destination, string copyName, ID copyID, bool deep)
		{
			return ItemFactory.BuildItem(_item.CopyTo(destination, copyName, copyID, deep));
		}

		public virtual void Delete()
		{
			_item.Delete();
		}

		public virtual int DeleteChildren()
		{
			return _item.DeleteChildren();
		}

		public virtual IItem Duplicate()
		{
			return ItemFactory.BuildItem(_item.Duplicate());
		}

		public virtual IItem Duplicate(string copyName)
		{
			return ItemFactory.BuildItem(_item.Duplicate(copyName));
		}

		public virtual void EndEdit()
		{
			_item.Editing.EndEdit();
		}

		public virtual ChildList GetChildren()
		{
			return _item.GetChildren();
		}

		public virtual ChildList GetChildren(ChildListOptions options)
		{
			return _item.GetChildren(options);
		}

		public virtual string GetOuterXml(bool includeSubitems)
		{
			return _item.GetOuterXml(includeSubitems);
		}

		public virtual void MoveTo(Item destination)
		{
			_item.MoveTo(destination);
		}

		public virtual void Paste(string xml, bool changeIDs, PasteMode mode)
		{
			_item.Paste(xml, changeIDs, mode);
		}

		public virtual ISitecoreItem PasteItem(string xml, bool changeIDs, PasteMode mode)
		{
			return ItemFactory.BuildItem(_item.PasteItem(xml, changeIDs, mode));
		}

		public virtual Guid Recycle()
		{
			return _item.Recycle();
		}

		public virtual int RecycleChildren()
		{
			return _item.RecycleChildren();
		}

		public virtual void Reload()
		{
			_item.Reload();
		}

		public virtual string GetUniqueId()
		{
			return _item.GetUniqueId();
		}

		public virtual ItemAccess Access
		{
			get { return _item.Access; }
		}

		public virtual ItemAppearance Appearance
		{
			get { return _item.Appearance; }
		}

		public virtual ItemAxes Axes
		{
			get { return _item.Axes; }
		}

		public virtual BranchItem Branch
		{
			get { return _item.Branch; }
		}

		public virtual BranchItem[] Branches
		{
			get { return _item.Branches; }
		}

		public virtual ID BranchId
		{
			get { return _item.BranchId; }
			set { _item.BranchId = value; }
		}

		public virtual ChildList Children
		{
			get { return _item.Children; }
		}

		public virtual IDatabase Database
		{
			get { return DatabaseFactory.BuildDatabase(_item.Database); }
		}

		public virtual string DisplayName
		{
			get { return _item.DisplayName; }
		}

		public virtual ItemEditing Editing
		{
			get { return _item.Editing; }
		}

		public virtual bool Empty
		{
			get { return _item.Empty; }
		}

		public virtual FieldCollection Fields
		{
			get { return _item.Fields; }
		}

		public virtual bool HasChildren
		{
			get { return _item.HasChildren; }
		}

		public virtual ItemHelp Help
		{
			get { return _item.Help; }
		}

		public virtual ID ID
		{
			get { return _item.ID; }
		}

		public virtual ItemData InnerData
		{
			get { return _item.InnerData; }
		}

		public virtual bool IsEditing
		{
			get { return _item.Editing.IsEditing; }
		}

		public virtual string Key
		{
			get { return _item.Key; }
		}

		public virtual Language Language
		{
			get { return _item.Language; }
		}

		public virtual Language[] Languages
		{
			get { return _item.Languages; }
		}

		public virtual ItemLinks Links
		{
			get { return _item.Links; }
		}

		public virtual ItemLocking Locking
		{
			get { return _item.Locking; }
		}

		public virtual BranchItem Master
		{
			get { return _item.Branch; }	
		}

		public virtual ID MasterID
		{
			get { return _item.BranchId; }
			set { _item.BranchId = value; }
		}

		public virtual BranchItem[] Masters
		{
			get { return _item.Branches; }
		}

		public virtual bool Modified
		{
			get { return _item.Modified; }
		}

		public virtual string Name
		{
			get { return _item.Name; }
			set { _item.Name = value; }
		}

		public virtual ID OriginatorId
		{
			get { return _item.OriginatorId; }
		}

		public virtual Item Parent
		{
			get { return _item.Parent; }
		}

		public virtual ID ParentID
		{
			get { return _item.ParentID; }
		}

		public virtual ItemPath Paths
		{
			get { return _item.Paths; }
		}

		public virtual ItemPublishing Publishing
		{
			get { return _item.Publishing; }
		}

		public virtual ItemRuntimeSettings RuntimeSettings
		{
			get { return _item.RuntimeSettings; }
		}

		public virtual ItemSecurity Security
		{
			get { return _item.Security; }
			set { _item.Security = value; }
		}

		public virtual ItemState State
		{
			get { return _item.State; }
		}

		public virtual ItemStatistics Statistics
		{
			get { return _item.Statistics; }
		}

		public virtual object SyncRoot
		{
			get { return _item.SyncRoot; }
		}

		public virtual TemplateItem Template
		{
			get { return _item.Template; }
		}

		public virtual ID TemplateID
		{
			get { return _item.TemplateID; }
			set { _item.TemplateID = value; }
		}

		public virtual string TemplateName
		{
			get { return _item.TemplateName; }
		}

		public virtual ItemUri Uri
		{
			get { return _item.Uri; }
		}

		public virtual Version Version
		{
			get { return _item.Version; }
		}

		public virtual ItemVersions Versions
		{
			get { return _item.Versions; }
		}

		public virtual ItemVisualization Visualization
		{
			get { return _item.Visualization; }
		}
	}
}