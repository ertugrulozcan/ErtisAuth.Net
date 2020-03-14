using System;
using System.Collections.Generic;
using System.Linq;

namespace ErtisAuth.Infrastructure
{
	public class RequestBody
	{
		#region Enums

		public enum BodyTypes
		{
			None,
			FormData,
			UrlEncoded,
			Text,
			Javascript,
			Json,
			Html,
			Xml,
			Binary,
			GraphQL
		}

		#endregion
		
		#region Properties

		public BodyTypes Type { get; }

		public string ContentType
		{
			get
			{
				switch (this.Type)
				{
					case BodyTypes.None:
					case BodyTypes.FormData:
					case BodyTypes.Binary:	
					case BodyTypes.GraphQL:
						return null;
					case BodyTypes.UrlEncoded:
						return "application/x-www-form-urlencoded";
					case BodyTypes.Text:
						return "text/plain";
					case BodyTypes.Javascript:
						return "application/javascript";
					case BodyTypes.Json:
						return "application/json";
					case BodyTypes.Html:
						return "text/html";
					case BodyTypes.Xml:
						return "application/xml";
				}
				
				return null;
			}
		}
		
		public object Context { get; }

		#endregion
		
		#region Constructors

		/// <summary>
		/// Constructor for empty payload
		/// </summary>
		public RequestBody()
		{
			this.Type = BodyTypes.Json;
			this.Context = new object();
		}
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="body"></param>
		/// <param name="type"></param>
		public RequestBody(object body, BodyTypes type = BodyTypes.Json)
		{
			this.Type = type;
			this.Context = body;
		}
		
		#endregion

		#region Implicit & Explicit Operators

		public static implicit operator RequestBody(string body) => new RequestBody(body);
		
		public static explicit operator string(RequestBody body) => body.ToString();

		#endregion

		#region Methods

		public static RequestBody CreateUrlEncoded(IDictionary<string, string> dictionary)
		{
			if (dictionary == null || !dictionary.Any())
				return null;
			
			return new RequestBody(string.Join(Environment.NewLine, dictionary.Select(x => $"{x.Key}:{x.Value}")), BodyTypes.UrlEncoded);
		}

		public static IDictionary<string, string> DecodeUrlEncoded(string content)
		{
			try
			{
				return content.Split(Environment.NewLine).Select(x => new KeyValuePair<string, string>(x.Split(':')[0], x.Split(':')[1])).ToDictionary(x => x.Key, y => y.Value);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
		
		public override string ToString()
		{
			return this.Context.ToString();
		}

		#endregion
	}
}