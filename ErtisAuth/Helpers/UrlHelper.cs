using System;
using System.Collections.Generic;

namespace ErtisAuth.Helpers
{
	public static class UrlHelper
	{
		private const string STRING_FORMAT_START_BRACE = "{{";
		private const string STRING_FORMAT_END_BRACE = "}}";

		public static string ToUrlParam(this string parameter)
		{
			return $"{STRING_FORMAT_START_BRACE}{parameter}{STRING_FORMAT_END_BRACE}";
		}
		
		private static string ExtractPropertyName(string stringFormat, out int braceOpenIndex, out int braceCloseIndex, int startIndex = 0)
		{
			braceOpenIndex = stringFormat.IndexOf(STRING_FORMAT_START_BRACE, startIndex, StringComparison.Ordinal);
			if (braceOpenIndex >= 0)
			{
				braceCloseIndex = stringFormat.IndexOf(STRING_FORMAT_END_BRACE, braceOpenIndex, StringComparison.Ordinal);
				if (braceCloseIndex >= 0)
				{
					string innerText = stringFormat.Substring(braceOpenIndex + 1, braceCloseIndex - braceOpenIndex - 1).TrimStart().TrimEnd();
					return string.IsNullOrEmpty(innerText) ? null : innerText;
				}
			}
			
			braceCloseIndex = -1;
			return null;
		}

		public static string FormatWithProps(this object obj, string stringFormat, int startIndex = 0)
		{
			var propertyName = ExtractPropertyName(stringFormat, out int braceOpenIndex, out int braceCloseIndex, startIndex);
			if (!string.IsNullOrEmpty(propertyName))
			{
				var value = GetPropertyValue(obj, propertyName, out bool isExist);
				if (!isExist)
				{
					return obj.FormatWithProps(stringFormat, braceCloseIndex);
				}
				
				string valueStr = value != null ? value.ToString() : string.Empty;
				string formattedText = stringFormat.Replace(stringFormat.Substring(braceOpenIndex, braceCloseIndex - braceOpenIndex + 1), valueStr);
				return obj.FormatWithProps(formattedText);
			}

			return stringFormat;
		}

		private static object GetPropertyValue(object obj, string propertyPath, out bool isExist)
		{
			var propertyQueue = new Queue<string>(propertyPath.Split('.'));
			var propertyName = propertyQueue.Dequeue();
			var property = obj.GetType().GetProperty(propertyName);
			if (property == null)
			{
				isExist = false;
				return null;
			}

			var value = property.GetValue(obj);
			if (value == null)
			{
				isExist = true;
				return null;
			}
			else
			{
				if (propertyQueue.Count == 0)
				{
					isExist = true;
					return value;
				}
				else
				{
					var path = string.Join(".", propertyQueue);
					return GetPropertyValue(value, path, out isExist);
				}	
			}
		}

		public static string ReplaceOrRemove(string stringFormat, string tag, string value)
		{
			if (string.IsNullOrEmpty(stringFormat))
				return stringFormat;

			tag = STRING_FORMAT_START_BRACE + tag.TrimStart().TrimEnd() + STRING_FORMAT_END_BRACE;
			if (stringFormat.Contains(tag))
			{
				return stringFormat.Replace(tag, value ?? string.Empty);
			}
			else
			{
				stringFormat = stringFormat.Replace($"/{tag}", string.Empty);
				return stringFormat.Replace(tag, string.Empty);
			}
		}

		public static string ClearTags(string url)
		{
			if (string.IsNullOrEmpty(url))
				return url;

			// Regex.Match(url, "/<(.*?)>/g");

			int startIndex = url.IndexOf(STRING_FORMAT_START_BRACE, StringComparison.Ordinal);
			if (startIndex >= 0)
			{
				int endIndex = url.IndexOf(STRING_FORMAT_END_BRACE, StringComparison.Ordinal);
				if (endIndex >= 0)
				{
					string tag = url.Substring(startIndex, endIndex - startIndex + STRING_FORMAT_END_BRACE.Length);
					url = url.Replace($"/{tag}", string.Empty);
					url = url.Replace(tag, string.Empty);

					return ClearTags(url);
				}
			}
			
			return url;
		}

		public static string ClearRepeatedSlashes(string url)
		{
			while (url.Contains("//"))
			{
				url = url.Replace("//", "/");
			}

			return url;
		}
	}
}