using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Infrastructure.Repositories;
using IslamicPOS.Core.Models.Sales;

namespace IslamicPOS.API.Controllers;

public class TransactionController : BaseApiController
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionController(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetById(Guid id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return NotFound();
            
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> Create([FromBody] Transaction transaction)
    {
        var created = await _transactionRepository.AddAsync(transaction);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetByDateRange(
        [FromQuery] DateTime startDate, 
        [FromQuery] DateTime endDate)
    {
        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
        return Ok(transactions);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetByCustomer(string customerId)
    {
        var transactions = await _transactionRepository.GetTransactionsByCustomerAsync(customerId);
        return Ok(transactions);
    }

    [HttpGet("payment-method/{paymentMethodId}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetByPaymentMethod(string paymentMethodId)
    {
        var transactions = await _transactionRepository.GetTransactionsByPaymentMethodAsync(paymentMethodId);
        return Ok(transactions);
    }

    [HttpGet("total-sales")]
    public async Task<ActionResult<decimal>> GetTotalSales(
        [FromQuery] DateTime startDate, 
        [FromQuery] DateTime endDate)
    {
        var total = await _transactionRepository.GetTotalSalesAsync(startDate, endDate);
        return Ok(total);
    }

    [HttpGet("{id}/items")]
    public async Task<ActionResult<IEnumerable<TransactionItem>>> GetTransactionItems(Guid id)
    {
        var items = await _transactionRepository.GetTransactionItemsAsync(id);
        return Ok(items);
    }

    [HttpGet("sales-by-payment")]
    public async Task<ActionResult<Dictionary<string, decimal>>> GetSalesByPaymentMethod(
        [FromQuery] DateTime startDate, 
        [FromQuery] DateTime endDate)
    {
        var sales = await _transactionRepository.GetSalesByPaymentMethodAsync(startDate, endDate);
        return Ok(sales);
    }
}