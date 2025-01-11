using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductService
{
    public class ProductService : IProductService
    {

        private readonly IMongoCollection<Product> _products;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _products.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(id);
        }

        public async Task<List<ResultProductDto>> GetAllAsync()
        {
            var products = await _products.Find(a => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(products);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var product = await _products.Find<Product>(a => a.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(product);
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _products.FindOneAndReplaceAsync(a => a.Id == productDto.Id, product);
        }
    }
}
