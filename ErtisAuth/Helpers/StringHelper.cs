using System;

namespace ErtisAuth.Helpers
{
	public static class StringHelper
	{
		public static string ToStringAsRestFormat(object value)
		{
			if (value == null)
				return null;

			if (value is bool boooleanValue)
				return $"{boooleanValue.ToString().ToLower()}";
			
			if (value is DateTime dateTime)
				return $"{dateTime.ToString("yyyy-MM-ddTHH:mm:ss.FFF000Z")}";

			return value.ToString();
		}
	}
}