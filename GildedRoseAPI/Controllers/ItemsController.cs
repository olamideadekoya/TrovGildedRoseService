using GildedRoseAPI.BizLogic;
using GildedRoseAPI.Models;
using GildedRoseAPI.Models.Request;
using GildedRoseAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using static GildedRoseAPI.Util.InternalResponseCodes;

namespace GildedRoseAPI.Controllers
{
	[RoutePrefix("api/Items")]
    public class ItemsController : ApiController
    {
		NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		[Authorize]
		[Route("GetItemList")]
		[HttpGet]
		public async Task<HttpResponseMessage> GetItemList()
		{
			GetItemListResponse responsedata = new GetItemListResponse();

			try
			{
				CommonLib commonLib = new CommonLib();
				var itemList = await commonLib.GetAllItems();
				responsedata.responseCode =$"0{((int)InternalResponseCode.Succesful).ToString()}";
				responsedata.responseMessage = "Successful";
				responsedata.result = itemList;
				return Request.CreateResponse(HttpStatusCode.OK, responsedata);
			}
			catch (Exception ex)
			{
				responsedata.responseCode = ((int)InternalResponseCode.Exception).ToString();
				responsedata.responseMessage = "Exception";
				responsedata.result = null;
				logger.Error(ex);

				return Request.CreateResponse(HttpStatusCode.ExpectationFailed,responsedata) ;
			}
		}

		[Authorize]
		[Route("BuyItems")]
		[HttpPost]
		public async Task<HttpResponseMessage> BuyItem(BuyItemReq req) 
		     {
			BuyItemRes res = new BuyItemRes();
			try
			{
				if(req == null)
				{
					res.ResponseCode = ((int)InternalResponseCode.InvalidRequestBody).ToString();
					
					res.ResponseMessage = "Request body cannot be null";

					return Request.CreateResponse(HttpStatusCode.BadRequest, res);
				}
				CommonLib commonLib = new CommonLib();
				
				var identity = (ClaimsIdentity)User.Identity;
				//get merchantname from identity
				var merchantname = identity.Name;
				//get merchant profile from name
				var profile = commonLib.GetUserProfileByMerchantName(merchantname);
				if(profile == null)
				{
					//invalid profile
					res.ResponseCode = ((int)InternalResponseCode.InvalidCredential).ToString();
					res.ResponseMessage = "Invalid Credential";

					return Request.CreateResponse(HttpStatusCode.Forbidden, res);
				}
				//generate hash and compare with what was included in the requestbody
				var mygeneratedhash = commonLib.GenerateSHA256Hash(profile.SecretKey, req.timestamp, profile.ApiKey,req.ReceiptRef);
				//compare with hash sent by client
				if(mygeneratedhash != req.HashValue || string.IsNullOrEmpty(req.HashValue) )
				{
					//unrecognised user
					res.ResponseCode = ((int)InternalResponseCode.InvalidHash).ToString();
					res.ResponseMessage = "Invalid Hash";
					
					return Request.CreateResponse(HttpStatusCode.Forbidden, res);
				}
				res = await commonLib.BuyItem(req);
				return Request.CreateResponse(HttpStatusCode.OK, res);
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				res.ResponseCode = ((int)InternalResponseCode.Exception).ToString();
				res.ResponseMessage = "Exception Occurred";
				return Request.CreateResponse(HttpStatusCode.ExpectationFailed, res);
			}
		}
	}
}
