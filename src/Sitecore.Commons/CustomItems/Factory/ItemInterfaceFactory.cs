using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.SharedSource.Commons.CustomItems.Factory
{
	/// <summary>
	/// Builds a static dictionary of interface types and custom items types that implement them.
	/// Caches only interfaces tagged with the FactoryInterface attribute.
	/// </summary>
	public static class ItemInterfaceFactory
	{
		private static IDictionary<Type, IDictionary<string, Type>> CustomItemTemplateCache { get; set; }
		private static readonly object padlock = new object();

		public static List<T> GetItems<T>(List<Item> items)
		{
			if (items == null)
			{
				return new List<T>();
			}
			return items.Select(GetItem<T>).ToList();
		}

		/// <summary>
		/// Returns a custom item that implements the interface T. Will check the items
		/// base templates as well, one level deep, to find an implementation of T.
		/// </summary>
		/// <typeparam name="T">An interface tagged with the FactoryInterface attribute. </typeparam>
		/// <param name="item">The Sitecore item</param>
		/// <returns>A custom item that implements T, or null.</returns>
		public static T GetItem<T>(Item item)
		{
			var attributes = typeof(T).GetCustomAttributes(false);
			foreach (var attribute in attributes)
			{
				if (!(attribute is FactoryInterface)) continue;
				if (CustomItemTemplateCache == null)
				{
					lock (padlock)
					{
						if (CustomItemTemplateCache == null)
						{
							PopulateTemplateCache(item, typeof(T));
						}
					}
				}
				if (CustomItemTemplateCache.ContainsKey(typeof(T)))
				{
					var myObject = (T)GetItemFromInterface(item, typeof(T));
					if (Equals(myObject, default(T)))
					{
						if (item == null) continue;
						myObject = default(T);
					}
					return myObject;
				}
			}

			return default(T);
		}

		private static object GetItemFromInterface(Item item, Type interfaceType)
		{
			if (item == null || interfaceType == null) return null;
			var templateId = item.TemplateID.ToString();

			if (CustomItemTemplateCache == null || !CustomItemTemplateCache.ContainsKey(interfaceType))
			{
				Log.Info("ItemInterfaceFactory - Could not locate type in template cache.", typeof(ItemInterfaceFactory));
				return null;
			}

			if (CustomItemTemplateCache[interfaceType].ContainsKey(templateId))
			{
				var type = CustomItemTemplateCache[interfaceType][templateId];
				return ConstructNewItem(type, item);
			}
			// Try base templates
			foreach (TemplateItem baseTemplate in item.Template.BaseTemplates)
			{
				if (CustomItemTemplateCache[interfaceType].ContainsKey(baseTemplate.ID.ToString()))
				{
					var type = CustomItemTemplateCache[interfaceType][baseTemplate.ID.ToString()];
					return ConstructNewItem(type, item);
				}
			}
			//Try one level deeper
			foreach (TemplateItem baseTemplate in item.Template.BaseTemplates)
			{
				foreach (TemplateItem baseTemplate2 in baseTemplate.BaseTemplates)
				{
					if (CustomItemTemplateCache[interfaceType].ContainsKey(baseTemplate2.ID.ToString()))
					{
						var type = CustomItemTemplateCache[interfaceType][baseTemplate2.ID.ToString()];
						return ConstructNewItem(type, item);
					}
				}
			}

			//default
			return null;
		}

		private static void PopulateTemplateCache(Item item, Type groupType)
		{
			CustomItemTemplateCache = new Dictionary<Type, IDictionary<string, Type>>();
			var assembly = Assembly.GetAssembly(groupType);
			var types = assembly.GetExportedTypes();
			foreach (var type in types)
			{
				AnalyzeType(type, item);
			}
		}

		private static void AnalyzeType(Type type, Item item)
		{
			if (!type.IsClass) return;
			var interfaces = type.GetInterfaces();
			foreach (var iface in interfaces)
			{
				AnalyzeInterface(iface, type, item);
			}
		}

		private static void AnalyzeInterface(Type iface, Type classType, Item item)
		{
			var attributes = iface.GetCustomAttributes(true);
			foreach (var attribute in attributes)
			{
				if (!(attribute is FactoryInterface)) continue;

				if (!CustomItemTemplateCache.ContainsKey(iface))
				{
					CustomItemTemplateCache.Add(iface, new Dictionary<string, Type>());
				}

				var id = GetTemplateIdFromCustomItem(classType, item);
				if (!string.IsNullOrEmpty(id) && !CustomItemTemplateCache[iface].ContainsKey(id))
				{
					CustomItemTemplateCache[iface].Add(id, classType);
				}
			}
		}

		private static object ConstructNewItem(Type type, Item item)
		{
			ConstructorInfo constructor = type.GetConstructor(new[] { typeof(Item) });
			if (constructor == null) return null;

			return constructor.Invoke(new[] { item });
		}

		private static string GetTemplateIdFromCustomItem(Type type, Item item)
		{
			var child = ConstructNewItem(type, item);
			if (child == null) return null;
			FieldInfo field = type.GetField("TemplateId", BindingFlags.Static | BindingFlags.Public);
			return (field != null) ? field.GetValue(child) as string : null;
		}
	}

	[AttributeUsage(AttributeTargets.Interface)]
	public class FactoryInterface : Attribute
	{
	}
}
