using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Infrastructure
{
	public class EmailManager
	{
		private readonly IOrderManager _orderManager;

		public EmailManager(IOrderManager orderManager) {
			_orderManager = orderManager;
		}

		public Task DoAsync(Guid orderId) {
			return SendEmail(CreateEmailMessage(orderId));
		}

		private MimeMessage CreateEmailMessage(Guid orderId) {
			Response order = _orderManager.GetOrderById(orderId, Projection);
			OrderStatus orderStatus = GetOrderStatus(order?.Status);
			string listOfProducts = "";
			foreach (var productName in order?.Products?.Select(p => p.Name)) {
				listOfProducts += $"{productName}, ";
			}
			listOfProducts = listOfProducts.Substring(0, listOfProducts.Length - 2);
			var email = new MimeMessage();
			email.Sender = MailboxAddress.Parse("m.pust.k@gmail.com");
			email.To.Add(MailboxAddress.Parse(order.Email));
			email.Subject = $"Order {order?.OrderRef}";
			var builder = new BodyBuilder();
			builder.HtmlBody = $"Your purchase <b>({listOfProducts})</b> is in <b>{order.Status}</b> status</br>";
			email.Body = builder.ToMessageBody();
			return email;
		}

		private async Task SendEmail(MimeMessage email) {
			using (var smtp = new SmtpClient()) {
				smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
				smtp.Authenticate("m.pust.k@gmail.com", "Vfrcbvedx522535");
				await smtp.SendAsync(email);
				smtp.Disconnect(true);
			}
		}

		private static readonly Func<Order, Response> Projection = (order) =>
			new Response {
				OrderRef = order.OrderRef,
				FirstName = order.FirstName,
				LastName = order.LastName,
				Email = order.Email,
				PhoneNumber = order.PhoneNumber,
				Address1 = order.Address1,
				Address2 = order.Address2,
				City = order.City,
				PostCode = order.PostCode,
				Status = order.Status.ToString(),
				Products = order.OrderStocks.Select(y => new Product {
					Name = y.Stock.Product.Name,
					Description = y.Stock.Product.Description,
					Value = $"{y.Stock.Product.Value.ToString("N2")} $",
					Quantity = y.Quantity,
					StockDescription = y.Stock.Description,
				}),

				TotalValue = order.OrderStocks.Sum(y => y.Stock.Product.Value).ToString("N2")
			};

		private OrderStatus GetOrderStatus(string status) {
			return (OrderStatus)Enum.Parse(typeof(OrderStatus), status);
		}

		public class Response
		{
			public string OrderRef { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Email { get; set; }
			public string PhoneNumber { get; set; }
			public string Address1 { get; set; }
			public string Address2 { get; set; }
			public string City { get; set; }
			public string PostCode { get; set; }
			public string Status { get; set; }
			public IEnumerable<Product> Products { get; set; }

			public string TotalValue { get; set; }
		}

		public class Product
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public string Value { get; set; }
			public int Quantity { get; set; }
			public string StockDescription { get; set; }
		}

		public enum OrderStatus
		{
			Pending,
			Packed,
			Shipped
		}
	}
}
