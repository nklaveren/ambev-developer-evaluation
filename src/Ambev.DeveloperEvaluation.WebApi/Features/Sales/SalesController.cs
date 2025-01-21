using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.ORM.UnitOfWork;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Examples.Responses;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SalesController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SalesController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SalesController(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IMediator mediator,
        ILogger<SalesController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all sales
    /// </summary>
    /// <returns>A list of all sales</returns>
    /// <response code="200">Returns the list of sales</response>
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAllSaleResponse>))]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SaleListResponseExample))]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _unitOfWork.Sales.GetAllAsync();
        var salesResponse = _mapper.Map<IEnumerable<GetAllSaleResponse>>(sales);
        return Ok(salesResponse);
    }

    /// <summary>
    /// Retrieves a specific sale by id
    /// </summary>
    /// <param name="id">The sale id</param>
    /// <returns>The sale details</returns>
    /// <response code="200">Returns the sale</response>
    /// <response code="404">If the sale is not found</response>
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(GetAllSaleResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SaleResponseExample))]
    public async Task<IActionResult> GetSale(Guid id)
    {
        var sale = await _unitOfWork.Sales.GetByIdAsync(id);
        if (sale == null)
            return NotFound($"Sale with ID {id} not found");

        var saleResponse = _mapper.Map<GetAllSaleResponse>(sale);
        return Ok(saleResponse);
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <returns>The created sale</returns>
    /// <response code="201">Returns the newly created sale</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Sale))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SaleResponseExample))]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
    {
        try
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var createSaleCommand = _mapper.Map<CreateSaleCommand>(request);

            var result = await _mediator.Send(createSaleCommand);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSale), new { id = result.SaleId }, result);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CancelSaleResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
    {
        try
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var updateSaleCommand = _mapper.Map<UpdateSaleCommand>(request);
            updateSaleCommand.Id = id;
            var sale = await _mediator.Send(updateSaleCommand);
            await _unitOfWork.SaveChangesAsync();
            return Ok(sale);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CancelSaleResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSale(CancelSaleRequest request)
    {
        try
        {
            var validator = new CancelSaleValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var cancelSaleCommand = _mapper.Map<CancelSaleCommand>(request);
            var sale = await _mediator.Send(cancelSaleCommand);
            await _unitOfWork.SaveChangesAsync();
            return Ok(sale);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{saleId}/items/{itemId}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CancelSaleItemResult))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelSaleItem(CancelSaleItemRequest request)
    {
        try
        {
            var validator = new CancelSaleItemValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var cancelSaleItemCommand = _mapper.Map<CancelSaleItemCommand>(request);
            var sale = await _mediator.Send(cancelSaleItemCommand);
            await _unitOfWork.SaveChangesAsync();
            return Ok(sale);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
