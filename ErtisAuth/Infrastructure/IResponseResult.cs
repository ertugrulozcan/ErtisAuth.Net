using System;

namespace ErtisAuth.Infrastructure
{
	public interface IResponseResult : IResponseResult<object>
	{
		#region Methods

		ResponseResult<T> Cast<T>(Func<ResponseResult, T> dataPredicate);

		#endregion
	}
	
	public interface IResponseResult<T>
	{
		#region Properties

		bool IsSuccess { get; }
		
		System.Net.HttpStatusCode? HttpCode { get; }
		
		string Message { get; set; }
		
		T Data { get; set; }

        byte[] RawData { get; set; }

        Exception Exception { get; set; }

		#endregion
	}
	
	[Serializable]
	public class ResponseResult<T> : IResponseResult<T>
	{
		#region Fields

		private bool isSuccess;

		#endregion
		
		#region Properties

		public bool IsSuccess
		{
			get
			{
				if (this.HttpCode != null)
				{
					int code = (int)this.HttpCode;
					return code >= 200 && code < 300;
				}
				else
				{
					return this.isSuccess;
				}
			}

			set
			{
				this.isSuccess = value;
				if (value)
				{
					this.HttpCode = System.Net.HttpStatusCode.OK;
				}
				else
				{
					this.HttpCode = null;
				}
			}
		}

		public System.Net.HttpStatusCode? HttpCode { get; private set; }

		public string Message { get; set; }

		public T Data { get; set; }

        public byte[] RawData { get; set; }

        public Exception Exception { get; set; }

		#endregion
		
		#region Constructors

		public ResponseResult(bool isSuccess)
		{
			this.IsSuccess = isSuccess;
		}
		
		public ResponseResult(bool isSuccess, string message)
		{
			this.IsSuccess = isSuccess;
			this.Message = message;
		}
		
		public ResponseResult(System.Net.HttpStatusCode httpCode, string message)
		{
			this.HttpCode = httpCode;
			this.Message = message;
		}

		#endregion

		#region Methods

		public override string ToString()
		{
			return this.Message;
		}

		#endregion
	}

	[Serializable]
	public class ResponseResult : ResponseResult<object>, IResponseResult
	{
		#region Constructors
		
		public ResponseResult(bool isSuccess) : base(isSuccess)
		{ }

		public ResponseResult(bool isSuccess, string message) : base(isSuccess, message)
		{ }

		public ResponseResult(System.Net.HttpStatusCode httpCode, string message) : base(httpCode, message)
		{ }
		
		#endregion

		#region Methods

		public ResponseResult<T> Cast<T>(Func<ResponseResult, T> dataPredicate)
		{
			if (this.HttpCode != null)
			{
				return new ResponseResult<T>(this.HttpCode.Value, this.Message)
				{
					Data = dataPredicate(this),
                    RawData = this.RawData,
                    Exception = this.Exception
				};
			}
			else
			{
				return new ResponseResult<T>(this.IsSuccess, this.Message)
				{
					Data = dataPredicate(this),
                    RawData = this.RawData,
                    Exception = this.Exception
				};
			}
		}

		#endregion
	}
}