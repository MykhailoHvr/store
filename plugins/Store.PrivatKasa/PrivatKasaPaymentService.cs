using Microsoft.AspNetCore.Http;
using Store.Contractors;
using Store.Web.Contractors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.PrivatKasa
{
    public class PrivatKasaPaymentService : IPaymentService, IWebContractorService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PrivatKasaPaymentService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpRequest Request => httpContextAccessor.HttpContext.Request;

        public string Name => "PrivatKasa";

        public string Title => "Payment by credit card";

        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                       .AddParameter("orderId", order.Id.ToString());
        }

        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid Privat.Kasa payment step.");

            return Form.CreateLast(Name, step + 1, values);
        }

        public OrderPayment GetPayment(Form form)
        {
            if (form.ServiceName != Name || !form.IsFinal)
                throw new InvalidOperationException("Invalid payment form.");

            return new OrderPayment(Name, "payment card", form.Parameters);
        }

        public Uri StartSession(IReadOnlyDictionary<string, string> parameters, Uri returnUri)
        {
            var queryString = QueryString.Create(parameters);
            queryString += QueryString.Create("returnUri", returnUri.ToString());

            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = "PrivatKasa/",
                Query = queryString.ToString(),
            };

            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;

            return builder.Uri;
        }

        public Task<Uri> StartSessionAsync(IReadOnlyDictionary<string, string> parameters, Uri returnUri)
        {
            var uri = StartSession(parameters, returnUri);

            return Task.FromResult(uri);
        }
    }
}