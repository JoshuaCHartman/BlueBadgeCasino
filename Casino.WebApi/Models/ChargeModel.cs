
namespace Casino.WebApi.Models
{
    public class ChargeModel
    {
        public string CardNumber { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Zip { get; set; }
        public int Value { get; set; }



    }
}