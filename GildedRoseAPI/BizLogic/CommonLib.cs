using GildedRoseAPI.Models;
using GildedRoseAPI.Models.Request;
using GildedRoseAPI.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static GildedRoseAPI.Util.InternalResponseCodes;

namespace GildedRoseAPI.BizLogic
{
	public class CommonLib
	{
		NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		public async Task<IList<Item>> GetAllItems()
		{
			return await Task.Run(() =>
			{
				try
				{
					//Fetch Item list
					IList<Item> itemList = new List<Item>();
					var item1 = new Item()
					{
						Description = "Log of wood",
						Name = "Planks",
						Price = 3000
					};
					var item2 = new Item()
					{
						Description = "Furniture",
						Name = "Sofa",
						Price = 500
					};
					var item3 = new Item()
					{
						Description = "Home decoration",
						Name = "Flower Vase",
						Price = 250
					};
					itemList.Add(item1);
					itemList.Add(item2);
					itemList.Add(item3);
					return itemList;
				}
				catch (Exception ex)
				{
					logger.Error(ex);
					return null;
				}
			});
		}
		
		public async Task<BuyItemRes> BuyItem(BuyItemReq request)
		{
			return await Task.Run(() =>
			{
				try
				{
					/*Will hardcode response, but in production, data could be stored in db and 
					  payment reference generated after payment is confirmed
					 */
					BuyItemRes buyItemRes = new BuyItemRes()
					{
						ReferenceNo = "XXXXXX",
						ResponseCode =$"0{((int)InternalResponseCode.Succesful).ToString()}",
						ResponseMessage = "Payment Successful"

					};

					return buyItemRes;
				}
				catch (Exception ex)
				{
					logger.Error(ex);
					return null;
				}
			});
		}

		public UserProfile GetUserProfileByMerchantName(string merchantname)
		{
			try
			{
				/*for the sake of the test, a profile will be hardcoded, but when implemented
				in production, it should be fetched from the database based on the merchants 
				created to access the API
				 */
				if(string.IsNullOrEmpty(merchantname))
				{
					return null;
				}
				UserProfile userProfile = new UserProfile();
				userProfile.ApiKey = "TWVyY2hhbnQx"; //random base64string
				userProfile.MerchantName = "Merchant1";
				userProfile.SecretKey = "R2xpZGVyUm9zZVNlY3JldEtleQ=="; //random base64 string
				return userProfile;
			}
			catch (Exception ex)
			{
				logger.Error(ex);
				return null;
			}
		}

		//public string GenerateSHA256Hash(string secretkey, string timestamp, string apiKey)
		//{
		//	try
		//	{
		//		var secretKeyByteArray = Convert.FromBase64String(secretkey);
		//		//var secretKeyByteArray = Encoding.UTF8.GetBytes(secretkey);
		//		string input = $"{apiKey}{timestamp}";
		//		byte[] signature = Encoding.UTF8.GetBytes(input);

		//		using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
		//		{
		//			byte[] signatureBytes = hmac.ComputeHash(signature);

		//			return Convert.ToBase64String(signatureBytes);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		logger.Error(ex);
		//		return "";
		//	}
		//}

		public  string GenerateSHA256Hash(string secretkey, string timestamp, string apikey, string recieptref)
		{
			string input = $"{secretkey}{timestamp}{apikey}{recieptref}";
			var shama = new System.Security.Cryptography.SHA256Managed();
			byte[] crypto = shama.ComputeHash(Encoding.UTF8.GetBytes(input));
			return Convert.ToBase64String(crypto);

		}
	}
}