
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GildedRoseAPI.Util
{
	public class InternalResponseCodes
	{
		public enum InternalResponseCode
		{
			Succesful = 0,
			Exception = 96,
			InvalidRequestBody = 92,
			InvalidCredential = 91,
			InvalidHash = 93


		}
	}
}