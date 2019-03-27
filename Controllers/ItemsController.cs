using GildedRoseAPI.BizLogic;
using GildedRoseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GildedRoseAPI.Controllers
{
	[RoutePrefix("api/Items")]
    public class ItemsController : ApiController
    {

		public async Task<HttpResponseMessage> GetItemList()
		{
			try
			{
				CommonLib commonLib = new CommonLib();
				IEnumerable<Item> itemList = await commonLib.GetAllItems();
				return Request.CreateResponse(HttpStatusCode.OK, itemList);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.ExpectationFailed) ;
			}
		}

		public async Task<HttpResponseMessage> BuyItem()
		{
			try
			{
				CommonLib commonLib = new CommonLib();
				IEnumerable<Item> itemList = await commonLib.GetAllItems();
				return Request.CreateResponse(HttpStatusCode.OK, itemList);
			}
			catch (Exception)
			{
				return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
			}
		}
	}
}
