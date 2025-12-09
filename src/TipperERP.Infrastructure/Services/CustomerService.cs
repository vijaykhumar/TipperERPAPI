using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Customer;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class CustomerService : ICustomerService
{
	private readonly TipperErpDbContext _db;

	public CustomerService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<CustomerListDto>> GetAllAsync()
	{
		try
		{
			return await _db.Customers
				.Where(x => !x.IsDeleted)
				.OrderBy(x => x.CustomerName)
				.Select(x => new CustomerListDto
				{
					CustomerId = x.CustomerId,
					CustomerCode = x.CustomerCode,
					CustomerName = x.CustomerName,
					ContactPerson = x.ContactPerson,
					ContactNo = x.ContactNo,
					PaymentTerms = x.PaymentTerms,
					CreditLimit = x.CreditLimit,
					GstRegistrationNo = x.GstRegistrationNo,
					IsActive = x.IsActive
				}).ToListAsync();
		}
		catch (Exception ex)
		{
			// Log the exception (logging mechanism not shown here)
			throw new ApplicationException("An error occurred while retrieving customers.", ex);
		}
	}

	public async Task<CustomerListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Customers
			.Where(x => x.CustomerId == id && !x.IsDeleted)
			.Select(x => new CustomerListDto
			{
				CustomerId = x.CustomerId,
				CustomerCode = x.CustomerCode,
				CustomerName = x.CustomerName,
				ContactPerson = x.ContactPerson,
				ContactNo = x.ContactNo,
				PaymentTerms = x.PaymentTerms,
				CreditLimit = x.CreditLimit,
				GstRegistrationNo = x.GstRegistrationNo,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(CustomerCreateDto dto, Guid createdBy)
	{
		var entity = new CustomerMaster
		{
			CustomerId = Guid.NewGuid(),
			CompanyId = dto.CompanyId,
			BranchId = dto.BranchId,
			CustomerCode = dto.CustomerCode,
			CustomerName = dto.CustomerName,
			AddressLine1 = dto.AddressLine1,
			AddressLine2 = dto.AddressLine2,
			City = dto.City,
			State = dto.State,
			Country = dto.Country,
			PostalCode = dto.PostalCode,
			ContactPerson = dto.ContactPerson,
			ContactNo = dto.ContactNo,
			PaymentTerms = dto.PaymentTerms,
			CreditLimit = dto.CreditLimit,
			GstRegistrationNo = dto.GstRegistrationNo,
			Email = dto.Email,
			IsActive = dto.IsActive,
			CreatedBy = createdBy,
			CreatedDate = DateTime.UtcNow
		};
		_db.Customers.Add(entity);
		await _db.SaveChangesAsync();
		return entity.CustomerId;
	}

	public async Task UpdateAsync(Guid id, CustomerUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == id && !x.IsDeleted);
		if (entity == null) throw new KeyNotFoundException("Customer not found");

		entity.CompanyId = dto.CompanyId;
		entity.BranchId = dto.BranchId;
		entity.CustomerCode = dto.CustomerCode;
		entity.CustomerName = dto.CustomerName;
		entity.AddressLine1 = dto.AddressLine1;
		entity.AddressLine2 = dto.AddressLine2;
		entity.City = dto.City;
		entity.State = dto.State;
		entity.Country = dto.Country;
		entity.PostalCode = dto.PostalCode;
		entity.ContactPerson = dto.ContactPerson;
		entity.ContactNo = dto.ContactNo;
		entity.Email = dto.Email;
		entity.PaymentTerms = dto.PaymentTerms;
		entity.CreditLimit = dto.CreditLimit;
		entity.GstRegistrationNo = dto.GstRegistrationNo;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
