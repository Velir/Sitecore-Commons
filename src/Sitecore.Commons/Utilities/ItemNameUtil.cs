using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sitecore.SharedSource.Commons.Utilities
{
  /// <summary>
  /// Utilities for dealing with item names
  /// </summary>
  public class ItemNameUtil
  {
    //The longest a name can be in Sitecore
    private const int maxNameLength = 100;

    /// <summary>
    /// Returns a Hashtable with the foreign character to english character mappings.
    /// </summary>
    /// <returns></returns>
    public static Hashtable GetForeignCharacterMap()
    {
      Hashtable foreignCharacterMap = new Hashtable
                                        {
                                          {"à", "a"},
                                          {"è", "e"},
                                          {"ì", "i"},
                                          {"ò", "o"},
                                          {"ù", "u"},
                                          {"À", "A"},
                                          {"È", "E"},
                                          {"Ì", "I"},
                                          {"Ò", "O"},
                                          {"Ù", "U"},
                                          {"á", "a"},
                                          {"é", "e"},
                                          {"í", "i"},
                                          {"ó", "o"},
                                          {"ú", "u"},
                                          {"ý", "y"},
                                          {"Á", "A"},
                                          {"É", "E"},
                                          {"Í", "I"},
                                          {"Ó", "O"},
                                          {"Ú", "U"},
                                          {"Ý", "Y"},
                                          {"â", "a"},
                                          {"ê", "e"},
                                          {"î", "i"},
                                          {"ô", "o"},
                                          {"û", "u"},
                                          {"Â", "A"},
                                          {"Ê", "E"},
                                          {"Î", "I"},
                                          {"Ô", "O"},
                                          {"Û", "U"},
                                          {"ã", "a"},
                                          {"ñ", "n"},
                                          {"õ", "o"},
                                          {"Ã", "A"},
                                          {"Ñ", "N"},
                                          {"Õ", "O"},
                                          {"ä", "a"},
                                          {"ë", "e"},
                                          {"ï", "i"},
                                          {"ö", "o"},
                                          {"ü", "u"},
                                          {"ÿ", "y"},
                                          {"Ä", "A"},
                                          {"Ë", "E"},
                                          {"Ï", "I"},
                                          {"Ö", "O"},
                                          {"Ü", "U"},
                                          {"Ÿ", "Y"},
                                          {"å", "a"},
                                          {"Å", "A"},
                                          {"æ", "ae"},
                                          {"Æ", "AE"},
                                          {"œ", "oe"},
                                          {"Œ", "OE"},
                                          {"ç", "c"},
                                          {"Ç", "C"},
                                          {"ð", "o"},
                                          {"Ð", "D"},
                                          {"ø", "o"},
                                          {"Ø", "O"},
                                          {"¡", "i"},
                                          {"ß", "B"}
                                        };

      return foreignCharacterMap;
    }

    #region Get Valid Item Name Methods

    /// <summary>
    /// Gets the name of the valid.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <returns></returns>
    public static string GetValidItemName(string nameText)
    {
      return GetValidItemName(nameText, true, true);
    }

    /// <summary>
    /// Returns a valid item name for the passed in text
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <param name="stripTags">if set to <c>true</c> [strip tags].</param>
    /// <param name="replaceForeignCharacters">if set to <c>true</c> [replace foreign characters].</param>
    /// <returns>A valid Sitecore item name.</returns>
    public static string GetValidItemName(string nameText, bool stripTags, bool replaceForeignCharacters)
    {
      if(stripTags)
      {
        //Strip HTML tags 
        nameText = CleanNameOfHtml(nameText);
      }

      if(replaceForeignCharacters)
      {
        //Replace foreign characters with their english equivalent
        nameText = ReplaceForeignCharacters(nameText);
      }

      //If the clean name is too long, chop it off
      if(nameText.Length > maxNameLength)
      {
        nameText = nameText.Substring(0, maxNameLength - 1);
      }

      //Get the valid Sitecore item name
      nameText = Sitecore.Data.Items.ItemUtil.ProposeValidItemName(nameText);

      //Strip out any uneeded spaces, both in the middle of the string as well 
      // as trimming any beginning or ending spaces
      nameText = CleanNameOfExtraSpaces(nameText);

      return nameText;
    }

    #endregion

    #region Name Cleaning Methods

    /// <summary>
    /// Replaces the foreign characters in a name with their english equivalents.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <returns></returns>
    public static string ReplaceForeignCharacters(string nameText)
    {
      Hashtable foreignCharacterMap = GetForeignCharacterMap();
      return ReplaceCharacters(nameText, foreignCharacterMap);
    }

    /// <summary>
    /// Takes the passed in characters (the keys in the Hashtable) and replaces them with the Hashtable key value.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <param name="characterMap">The character map.</param>
    /// <returns></returns>
    public static string ReplaceCharacters(string nameText,Hashtable characterMap)
    {
      foreach (var key in characterMap.Keys)
      {
        nameText = nameText.Replace(key.ToString(), characterMap[key].ToString());
      }

      return nameText;
    }

    /// <summary>
    /// Cleans the name of a list of strings.
    /// </summary>
    /// <param name="nameText">The name text to strip text from.</param>
    /// <param name="textToStrip">The text to strip.</param>
    /// <returns>The cleaned name</returns>
    public static string CleanNameOfText(string nameText, List<string> textToStrip)
    {
      //Loop through all of the passed in strings and strip them
      // from the passed in name string.
      foreach (string text in textToStrip)
      {
        if(nameText.Contains(text))
        {
          nameText = nameText.Replace(text, string.Empty);
        }
      }

      return nameText;
    }

    /// <summary>
    /// Replace any text matching the passed in regular expression with a blank string.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <param name="regularExpression">The regular expression.</param>
    /// <returns></returns>
    public static string CleanNameOfText(string nameText, string regularExpression)
    {
      return Regex.Replace(nameText, regularExpression, string.Empty);
    }

    /// <summary>
    /// Cleans the name of extra spaces both internally and at the beginning and end of the name.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <returns></returns>
    public static string CleanNameOfExtraSpaces(string nameText)
    {
      return Regex.Replace(nameText, @"\s{2,}", " ").Trim();
    }

    /// <summary>
    /// Cleans a name of HTML tags.
    /// </summary>
    /// <param name="nameText">The name text.</param>
    /// <returns>The name with the HTML tags stripped out</returns>
    public static string CleanNameOfHtml(string nameText)
    {
      return CleanNameOfText(nameText, @"\<[^\>]*\>");
    }

    #endregion
  }
}
