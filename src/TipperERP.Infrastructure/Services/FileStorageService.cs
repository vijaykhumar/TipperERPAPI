using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TipperERP.Infrastructure.Services;

public class FileStorageService
{
	//private readonly IWebHostEnvironment _env;

	//public FileStorageService(IWebHostEnvironment env)
	//{
	//	_env = env;
	//}

	//public async Task<string> SaveFileAsync(IFormFile file)
	//{
	//	string folder = Path.Combine(_env.ContentRootPath, "uploads");

	//	if (!Directory.Exists(folder))
	//		Directory.CreateDirectory(folder);

	//	string fileName = $"{Guid.NewGuid()}_{file.FileName}";
	//	string filePath = Path.Combine(folder, fileName);

	//	using (var stream = new FileStream(filePath, FileMode.Create))
	//	{
	//		await file.CopyToAsync(stream);
	//	}

	//	return $"/uploads/{fileName}";
	//}
}
