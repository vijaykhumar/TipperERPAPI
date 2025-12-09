using System.Threading.Tasks;

namespace TipperERP.Application.Auth;

public interface IAuthService
{
	Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
}
