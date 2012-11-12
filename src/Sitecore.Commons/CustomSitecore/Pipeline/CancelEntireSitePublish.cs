using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Jobs;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace Sitecore.SharedSource.Commons.CustomSitecore.Pipeline
{
	/// <summary>
	/// CancelEntireSitePublish is a safe guard against a publish of a home item and its sub items. 
	/// Checks the root item for a child of /sitecore/content and if the deep option is true.
	/// </summary>
	public class CancelEntireSitePublish
	{

		/// <summary>
		/// Option to overwrite the sitecoreBasePaths by setting the AppSettings configuration Sitecore.SharedSource.Commons.CancelEntireSitePublish
		/// Option to allow admins to perform the publish and override this functionality by setting the AppSettings configuration Sitecore.SharedSource.Commons.AllowAdminEntireSitePublish to "1" or "true"
		/// Option to specify exclusion paths that won't go through this functionality by setting the AppSettings configuration Sitecore.SharedSource.Commons.CancelEntireSitePublishExclusionPaths
		/// Option to specify the message written when this process cancels a site publish by setting the AppSettings configuration Sitecore.SharedSource.Commons.CancelEntireSitePublishMessage
		/// 
		/// Add a handler to the itemProcessing pipeline as follows in this example.
		/// 
		///  <event name="publish:itemProcessing" help="Receives an argument of type ItemProcessingEventArgs (namespace: Sitecore.Publishing.Pipelines.PublishItem)">
		///    <handler type="Sitecore.SharedSource.Commons.Pipelines.CancelEntireSitePublish, Sitecore.SharedSource.Commons" method="CheckProcessing" />
		///  </event>
		/// 
		/// Build using Sitecore.Kernel Version 6.0 081022
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void CheckProcessing(object sender, EventArgs args)
		{
			#region initialize sitecoreBasePaths to /sitecore/content and check for local config to determine paths to run this check for
			List<string> sitecoreBasePaths = new List<string>() { "/sitecore/content" };
			try
			{
				string pathList =
					System.Configuration.ConfigurationManager.AppSettings["Sitecore.SharedSource.Commons.CancelEntireSitePublish"];
				if (pathList != null)
				{
					sitecoreBasePaths = pathList.ToLower().Split('|').ToList();
				}
			}
			catch (Exception ex) { }
			#endregion

			#region initialize sitecoreBaseExclusionPaths to empty and check for local config to determine paths to run this check for
			List<string> sitecoreBaseExclusionPaths = new List<string>();
			try
			{
				string pathList =
					System.Configuration.ConfigurationManager.AppSettings["Sitecore.SharedSource.Commons.CancelEntireSitePublishExclusionPaths"];
				if (pathList != null)
				{
					sitecoreBaseExclusionPaths = pathList.ToLower().Split('|').ToList();
				}
			}
			catch (Exception ex) { }
			#endregion

			#region initialize cancelMessage to "Publishing stop due to full site publish." and check for local config to determine message
			string cancelMessage = "Publishing stop due to full site publish.";
			try
			{
				string localCancelMessage =
					System.Configuration.ConfigurationManager.AppSettings["Sitecore.SharedSource.Commons.CancelEntireSitePublishMessage"];
				if (!string.IsNullOrEmpty(localCancelMessage))
				{
					cancelMessage = localCancelMessage;
				}
			}
			catch (Exception ex) { }
			#endregion

			#region if we set our config to allow admins and the current user is an admin, continue as normal
			bool allowAdmin = false;
			try
			{
				string strAllowAdmin = System.Configuration.ConfigurationManager.AppSettings["Sitecore.SharedSource.Commons.AllowAdminEntireSitePublish"];
				if (!string.IsNullOrEmpty(strAllowAdmin))
				{
					if (strAllowAdmin == "1" || strAllowAdmin.ToUpper() == "TRUE")
						allowAdmin = true;
				}
			}
			catch (Exception ex) { }

			if (allowAdmin && Sitecore.Context.User.IsAdministrator) return;
			#endregion

			ItemProcessingEventArgs theArgs = args as ItemProcessingEventArgs;
			if (theArgs == null) return;
			Item currentItem = theArgs.Context.PublishHelper.GetSourceItem(theArgs.Context.ItemId);
			if ((currentItem == null) || (!currentItem.Paths.IsContentItem)) return;
			Item rootItem = theArgs.Context.PublishOptions.RootItem;
			#region if this item is in an exclusion path, return
			string myItemPath = rootItem.Paths.Path.ToLower();
			if (sitecoreBaseExclusionPaths.Contains(myItemPath)) return;
			#endregion
			#region if this item's parent is in an inclusion path and we are publishing child items, stop the job
			string rootItemPath = rootItem.Parent.Paths.Path.ToLower();
			if (sitecoreBasePaths.Contains(rootItemPath)
					&& theArgs.Context.PublishOptions.Deep)
			{
				Job currentJob = theArgs.Context.Job;
				JobStatus currentJobStatus = currentJob.Status;
				currentJobStatus.Messages.Add(cancelMessage);
				theArgs.Cancel = true;
			}
			#endregion
		}
	}
}