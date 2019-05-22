namespace RStore.Models.ViewModels {
    public class ProductDetailViewModel {
        public Product Product { get; set; }
        public int? PrevProductID { get; set; }
        public int? NextProductID { get; set; }
        public int CurrentIndex { get; set; }
        public int TotalProduct { get; set; }
    }
}