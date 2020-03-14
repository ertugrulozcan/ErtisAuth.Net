using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ErtisAuth.Infrastructure;
using ErtisAuth.Json;

namespace ErtisAuth.Rest.Impl
{
	public class SystemRestHandler : IRestHandler
	{
		#region Fields

		private readonly HttpClient Client;

		#endregion


		#region Constructors

		public SystemRestHandler()
		{
			this.Client = new HttpClient();
		}

		#endregion
		
		#region Methods

		public IResponseResult<TResult> ExecuteRequest<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			return this.ExecuteRequestAsync<TResult>(method, url, body, headers).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult<TResult>> ExecuteRequestAsync<TResult>(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			var response = await this.ExecuteRequestAsync(method, url, body, headers);
			if (response.IsSuccess)
			{
				return new ResponseResult<TResult>(true)
				{
					Data = Newtonsoft.Json.JsonConvert.DeserializeObject<TResult>(response.Data.ToString())
				};
			}
			else
			{
				if (response.HttpCode != null)
				{
					return new ResponseResult<TResult>(response.HttpCode.Value, response.Message);
				}
				else
				{
					return new ResponseResult<TResult>(false, response.Message);	
				}
			}
		}
		
		public IResponseResult ExecuteRequest(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			return this.ExecuteRequestAsync(method, url, body, headers).ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public async Task<IResponseResult> ExecuteRequestAsync(HttpMethod method, string url, RequestBody body, IHeaderCollection headers)
		{
			var payload = this.PrepareBody(body);
			
			this.Client.DefaultRequestHeaders.Clear();
			if (headers != null)
			{
				foreach (var header in headers)
				{
					this.Client.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
				}
			}
			
			HttpResponseMessage response = null;
			if (method == HttpMethod.Get)
			{
				response = await this.Client.GetAsync(url);
			}
			else if (method == HttpMethod.Post)
			{
				response = await this.Client.PostAsync(url, payload);
			}
			else if (method == HttpMethod.Put)
			{
				response = await this.Client.PutAsync(url, payload);
			}
			else if (method == HttpMethod.Delete)
			{
				response = await this.Client.DeleteAsync(url);
			}

			if (response != null)
			{
				if (response.IsSuccessStatusCode)
				{
					return new ResponseResult(response.StatusCode, string.Empty)
					{
						Data = await response.Content.ReadAsStringAsync()
					};
				}
				else
				{
					return new ResponseResult(response.StatusCode, await response.Content.ReadAsStringAsync());
				}
			}
			else
			{
				return new ResponseResult(false, "Response is null!");
			}
		}

		private HttpContent PrepareBody(RequestBody body)
		{
			if (body == null)
				return null;
			
			HttpContent content = null;
			
			if (body.Type == RequestBody.BodyTypes.Json)
			{
				string json = Newtonsoft.Json.JsonConvert.SerializeObject(body.Context, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings()
				{
					ContractResolver = new BlupointJsonContractResolver()
				});
				
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
				content = new FormUrlEncodedContent(RequestBody.DecodeUrlEncoded(body.Context.ToString()));
				content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
			}
			else
			{
				var buffer = System.Text.Encoding.UTF8.GetBytes(body.Context.ToString());
				content = new ByteArrayContent(buffer);
				content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
			}
			
			return content;
		}

		#endregion
	}
}