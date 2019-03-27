using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GildedRoseAPI.Models.Response
{
	public class GetItemListResponse
	{
		public string responseCode { get; set; }
		public string responseMessage { get; set; }
		public IEnumerable<Item> result { get; set; }
	}
}