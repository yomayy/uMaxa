﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Cart
{
	public class GetCustomerInformation
	{
		private ISession _session;

		public GetCustomerInformation(ISession session) {
			_session = session;
		}

		public class Response
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }
		}

		public Response Do() {

			var stringObj = _session.GetString("customer-info");
			if (string.IsNullOrEmpty(stringObj)) {
				return null;
			}
			var response = JsonConvert.DeserializeObject<Response>(stringObj);
			return response;
		}
	}
}
