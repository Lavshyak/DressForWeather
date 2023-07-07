[ApiController]
[Route("[controller]")]
[Authorize]
public class DressReportController : ControllerBase
{
	private readonly ILogger<DressReportController> _logger;
	private readonly MainDbContext _mainDbContext;
	public DressReportController(ILogger<DressReportController> logger, MainDbContext mainDbContext)
	{
		_logger = logger;
		_mainDbContext = mainDbContext;
	}

	[Authorize]
	[HttpGet(Name = "GetDressReportById")]
	public async Task<DressReport?> GetDressReportById(long id)
	{
		var report = await _mainDbContext.DressReports.FirstOrDefaultAsync(dr => dr.Id == id);
		return report;
	}
}
