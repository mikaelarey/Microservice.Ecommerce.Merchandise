namespace Merchandise.Application.Dtos.Requests.Product
{
    public class ProductUpdateResponseDto
    {
        public string Status { get; set; }
        public ProductUpdateRequestDto Product { get; set; }
        public List<string>? ErrorMessages { get; set; }
        
       
    }
}
