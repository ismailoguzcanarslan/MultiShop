using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailService
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> _productDetails;
        private readonly IMapper _mapper;

        public ProductDetailService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _productDetails = database.GetCollection<ProductDetail>(settings.ProductDetailCollectionName);
            _mapper = mapper;
        }


        public async Task CreateProductDetailAsync(CreateProductDetailDto ProductDetailDto)
        {
            var productDetail = _mapper.Map<ProductDetail>(ProductDetailDto);
            await _productDetails.InsertOneAsync(productDetail);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await _productDetails.DeleteOneAsync(id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllAsync()
        {
            var productDetails = await _productDetails.Find(a => true).ToListAsync();
            return _mapper.Map<List<ResultProductDetailDto>>(productDetails);
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var productDetail = await _productDetails.Find<ProductDetail>(a => a.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDetailDto>(productDetail);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto ProductDetailDto)
        {
            var productDetail = _mapper.Map<ProductDetail>(ProductDetailDto);
            await _productDetails.FindOneAndReplaceAsync(a => a.Id == productDetail.Id, productDetail);
        }
    }
}
