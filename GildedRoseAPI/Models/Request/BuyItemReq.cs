using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GildedRoseAPI.Models.Request
{
	public class BuyItemReq
	{
		public string ItemName { get; set; }
		public int Quantity { get; set; }
		public string ReceiptRef { get; set; }
		public string DeliveryLocation { get; set; }
		public string timestamp { get; set; }
		public string HashValue { get; set; }
	}
}