using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GildedRoseAPI.Models
{
	public class UserProfile
	{
		public string MerchantName { get; set; }
		public string SecretKey { get; set; }
		public string ApiKey { get; set; }
	}
}