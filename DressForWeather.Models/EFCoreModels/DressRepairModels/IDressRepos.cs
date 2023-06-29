using DressForWeather.Models.EFCoreModels;
namespace DressForWeather.Models.EFCoreModels.DressRepairModels;

public interface IDressRepos 
{
    public async Task<DressReport> Create(DressReport model);
    public async Task<DressReport> Delete(DressReport model);
    public async Task<DressReport> Update(DressReport model)

}
p