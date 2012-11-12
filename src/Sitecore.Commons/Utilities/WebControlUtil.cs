using System.Collections.Generic;
using System.Collections.Specialized;
using Sitecore.Web.UI;

namespace Sitecore.SharedSource.Commons.Utilities
{
	///<summary>
	///	Provides Utilities for dealing with Sitecore Webcontrols and Sublayouts
	///</summary>
	public class WebControlUtil
	{
		///<summary>
		///	Provides a NameValueCollection of Parameters listed in the
		///	Webcontrol and Sublayout Data Template.
		///	Takes the Parameter string Field and Separates it out as such. 
		///
		///	Title=Some Title
		///	Subtitle=Some SubTitle
		///	Something=Else
		/// 
		///	Takes this, splits it out for each line, then adds to the collection.
		///	The above creates:
		///	{'Title'=>'Some Title','Subtitle'=>'Some SubTitle','Something'=>'Else'} 
		/// 
		///	This overload assumes that the Parameters are separated by line and
		///	the key/value pairs are delimited by an equal sign (=).
		///</summary>
		///<param name = "control">The WebControl object, usually 'this' will work.</param>
		///<returns>NameValueCollection of Parameters.  Returns Empty Collection if nothing.</returns>
		public static NameValueCollection GetParameters(WebControl control)
		{
			List<char> delimiters = new List<char>();
			delimiters.Add('\r');
			delimiters.Add('\n');

			return GetParameters(control, delimiters.ToArray(), '=');
		}

		///<summary>
		///	Provides a NameValueCollection of Parameters listed in the
		///	Webcontrol and Sublayout Data Template.
		///	Takes the Parameter string Field and Separates it out as such. 
		///
		///	Title=Some Title
		///	Subtitle=Some SubTitle
		///	Something=Else
		/// 
		///	Takes this, splits it out for each line, then adds to the collection.
		///	The above creates:
		///	{'Title'=>'Some Title','Subtitle'=>'Some SubTitle','Something'=>'Else'}
		///</summary>
		///<param name = "control">The WebControl object, usually 'this' will work.</param>
		///<param name = "delimiters">char[] of parameter delimiters</param>
		///<param name = "keyPairDelimiter">delimiter of key/value pair separator</param>
		///<returns></returns>
		public static NameValueCollection GetParameters(WebControl control, char[] delimiters, char keyPairDelimiter)
		{
			string[] paramArry = control.Parameters.Split(delimiters);

			NameValueCollection paramcollection = new NameValueCollection();

			foreach (string pair in paramArry)
			{
				string[] keyValue = pair.Split(keyPairDelimiter);
				if (keyValue.Length > 1)
					paramcollection.Add(keyValue[0], keyValue[1]);
			}

			return paramcollection;
		}
	}
}