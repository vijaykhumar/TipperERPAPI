using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.InsuranceCompany;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InsuranceCompanyController : ControllerBase
{
	private readonly IInsuranceCompanyService _service;

	public InsuranceCompanyController(IInsuranceCompanyService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var companies = await _service.GetAllAsync();
		return Ok(new { success = true, data = companies });
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var company = await _service.GetByIdAsync(id);
		if (company == null)
			return NotFound(new { success = false, message = "Insurance company not found" });

		return Ok(new { success = true, data = company });
	}

	[HttpPost]
	public async Task<IActionResult> Create(InsuranceCompanyCreateDto dto)
	{
		var createdBy = Guid.Empty; // Replace later with JWT User Id
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, InsuranceCompanyUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(id, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(id, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
