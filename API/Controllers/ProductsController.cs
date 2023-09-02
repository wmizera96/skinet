using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepository;
    private readonly IGenericRepository<ProductBrand> _productBrandsRepository;
    private readonly IGenericRepository<ProductType> _productTypesRepository;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productsRepository,
        IGenericRepository<ProductBrand> productBrandsRepository,
        IGenericRepository<ProductType> productTypesRepository,
        IMapper mapper)
    {
        _productsRepository = productsRepository;
        _productBrandsRepository = productBrandsRepository;
        _productTypesRepository = productTypesRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(CancellationToken cancellationToken)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        
        var products = await _productsRepository.ListAsync(spec, cancellationToken);

        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(new ProductId(id));
        var product = await _productsRepository.GetEntityWithSpecAsync(spec, cancellationToken);

        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyCollection<ProductBrand>>> GetProductBrands(CancellationToken cancellationToken)
    {
        return Ok(await _productBrandsRepository.ListAllAsync(cancellationToken));
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyCollection<ProductType>>> GetProductTypes(CancellationToken cancellationToken)
    {
        return Ok(await _productTypesRepository.ListAllAsync(cancellationToken));
    }
}
