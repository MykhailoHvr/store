using Store.Contractors;
using Store.Web.Contractors;
using System.Collections.Generic;

namespace Store.PrivatKasa
{
    public class PrivatKasaPaymentService : IPaymentService, IWebContractorService
    {
        public string UniqueCode => "PrivatKasa";

        public string Title => "Payment by credit card";

        public string GetUri => "/PrivatKasa/";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "payment card", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}