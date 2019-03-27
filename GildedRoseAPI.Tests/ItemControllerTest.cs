using GildedRoseAPI.BizLogic;
using GildedRoseAPI.Models;
using GildedRoseAPI.Models.Request;
using GildedRoseAPI.Models.Response;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GildedRoseAPI.Tests
{
	[TestFixture]
	class ItemControllerTest
	{
		#region Variables

		private HttpClient _client;

		private HttpResponseMessage _response;
		private const string ServiceBaseURL = "http://localhost:80/Deploy/";


		#endregion

		#region Setup
		/// <summary>
		/// Re-initializes test.
		/// </summary>
		[SetUp]
		public void ReInitializeTest()
		{
			_client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };
			_client.DefaultRequestHeaders.Add("Authorization", "bearer GpHtHTXFuKD9sTE4yT8CUE0KJBkwz7kLzEt7II94s8ronVQXPei7UDdu72jlIYevwNeLa6XKO53Qpcn0KsL0nESD4kTtPQK4b-G9JufoM1NdskGRrO7XI6pOMWWaQlxxA3HKuge_yH8whiNjGAuDCEE75C_v-Njsemx_BTmhpdrv6O6AZPh6qVl1xwi3ooH7874y2TppdfWv8W7G00lG4qIap34b7bboiIWnWkUPnHC9jLMHvHpp1_d00beh9uDrTvFWVxMsv75n7buvqcSRqgbyYgtfGqTl3oNQPKfEqv_Mj2NZKa2I_XpZsQzhrtzp");
		}
		#endregion
		public BuyItemReq GetBuyItemReqData()
		{
			var buyitemreq = new BuyItemReq()
			{
				DeliveryLocation = "Chicago",
				ItemName = "Planks",
				Quantity = 300,
				ReceiptRef = "Ref1234",
				timestamp = "1553644809379",
				HashValue = "jeB9FrZbmREq0BkvV+1Upb3huITcO9Vq2ChGTPTu1Is="
			};
			return buyitemreq;
		}

		#region Unit Test
		[Test]
		public void GetItems()
		{
			CommonLib common = new CommonLib();
			var items = common.GetAllItems();
			Assert.NotNull(items);
		}
		#endregion

		#region Integration Test

		/// <summary>
		/// Get all Items and Buy Items
		/// </summary>
		[Test]
		public void GetAllItemsTest()
		{

			//_response = _client.GetAsync("api/items/GetItemList").Result;
			_response = _client.GetAsync("api/items/GetItemList").Result;
			var responseResult =
				JsonConvert.DeserializeObject<GetItemListResponse>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseResult.result.Any(), true);
		}
		[Test]
		public void GetAllItemsTestWithoutAuthorization()
		{
			var client = new HttpClient { BaseAddress = new Uri(ServiceBaseURL) };

			_response = client.GetAsync("api/items/GetItemList/").Result;
			var responseResult =
				JsonConvert.DeserializeObject<GetItemListResponse>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.Unauthorized);
			Assert.AreEqual(responseResult.result, null);
		}
		[Test]
		public void BuyItemTestWithNoRequestBody()
		  {

			_response = _client.PostAsync("api/items/BuyItems/",null).Result;
			           var responseResult =
				JsonConvert.DeserializeObject<BuyItemRes>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.BadRequest);
			Assert.AreEqual(responseResult.ReferenceNo, null);
		}
		[Test]
		public void BuytemTestWithWrongHash()
		{
			var reqbody = GetBuyItemReqData();
			reqbody.HashValue = "WrongHashValue";
			string reqbodyjson = JsonConvert.SerializeObject(reqbody);
			HttpContent content = new StringContent(reqbodyjson, Encoding.UTF8, "application/json");
			
			_response = _client.PostAsync("api/items/BuyItems/",content).Result;
			var responseResult =
				JsonConvert.DeserializeObject<BuyItemRes>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.Forbidden);
			Assert.AreEqual(responseResult.ReferenceNo, null);
		}
		[Test]
		public void BuyItemTest()
		{
			var reqbody = GetBuyItemReqData();
			string reqbodyjson = JsonConvert.SerializeObject(reqbody);
			HttpContent content = new StringContent(reqbodyjson, Encoding.UTF8, "application/json");

			_response = _client.PostAsync("api/items/BuyItems/",content).Result;
			var responseResult =
				JsonConvert.DeserializeObject<BuyItemRes>(_response.Content.ReadAsStringAsync().Result);
			Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK);
			Assert.AreEqual(responseResult.ReferenceNo.Any(), true);
		}

		#endregion

	}
}