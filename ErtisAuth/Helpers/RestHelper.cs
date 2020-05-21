using System.Net.Http;
using System.Net.Http.Headers;
using ErtisAuth.Infrastructure;
using ErtisAuth.Json;
using ErtisAuth.Queries.MongoQueries;

namespace ErtisAuth.Helpers
{
	public static class RestHelper
	{
		public static HttpContent GenerateBody(RequestBody body, HttpMethod method)
		{
			if (body == null)
				return null;
			
			HttpContent content = null;
			
			if (body.Type == RequestBody.BodyTypes.Json)
			{
				string json;
				if (method == HttpMethod.Post)
				{
					json = Newtonsoft.Json.JsonConvert.SerializeObject(body.Context, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
					{
						ContractResolver = new JsonContractResolver(HttpMethod.Post)
					});	
				}
				else if (method == HttpMethod.Put)
				{
					json = Newtonsoft.Json.JsonConvert.SerializeObject(body.Context, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
					{
						ContractResolver = new JsonContractResolver(HttpMethod.Put)
					});	
				}
				else
				{
					json = Newtonsoft.Json.JsonConvert.SerializeObject(body.Context);
				}
				
				var buffer = System.Text.Encoding.UTF8.GetBytes(json);
				content = new ByteArrayContent(buffer);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}
			else if (body.Type == RequestBody.BodyTypes.Xml)
			{
				string xml = body.Context.ToString();
				var buffer = System.Text.Encoding.UTF8.GetBytes(xml);
				content = new ByteArrayContent(buffer);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
			}
			else if (body.Type == RequestBody.BodyTypes.Binary)
			{
				var buffer = (byte[]) body.Context;
				content = new ByteArrayContent(buffer);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
			}
			else if (body.Type == RequestBody.BodyTypes.UrlEncoded)
			{
				if (body.Context is FormUrlEncodedContent formUrlEncodedContent)
				{
					content = formUrlEncodedContent;
				}
				else
				{
					content = new FormUrlEncodedContent(RequestBody.DecodeUrlEncoded(body.Context.ToString()));	
				}
				
				//content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			}
			else if (body.Type == RequestBody.BodyTypes.MongoQuery)
			{
				if (body.Context is IQuery query)
				{
					string json = query.ToString();
					var buffer = System.Text.Encoding.UTF8.GetBytes(json);
					content = new ByteArrayContent(buffer);
				}
				
				content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}
			else
			{
				var buffer = System.Text.Encoding.UTF8.GetBytes(body.Context.ToString());
				content = new ByteArrayContent(buffer);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
			}
			
			return content;
		}
	}
}