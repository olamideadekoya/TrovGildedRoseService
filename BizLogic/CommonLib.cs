using GildedRoseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GildedRoseAPI.BizLogic
{
	public class CommonLib
	{
		public async Task<IEnumerable<Item>> GetAllItems()
		{
			return await Task.Run(() =>
			{
				try
				{
					//Fetch Item list
					IEnumerable<Item> itemList = null;
					return itemList;
				}
				catch (Exception ex)
				{
					return null;
				}
			});
		}
	}
}