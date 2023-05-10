using EasyAgenda.Data.Contracts;
using EasyAgenda.Model;
using EasyAgendaBase.Model;
using EasyAgendaService.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EasyAgenda.Data.DAL
{
  public class SettingMailDAL : ISettingMailDAL
  {
    private readonly EasyAgendaContext _context;
    public SettingMailDAL(EasyAgendaContext context)
    {
      _context = context;
    }

    public async Task<SettingMail> Get()
    {
      return await _context.SettingMail
      .AsNoTracking()
      .DefaultIfEmpty()
      .FirstAsync();
    }

    public async Task Configure(SettingMail settingMail)
    {
      try
      {
        var existsSetting = await Get();
        if (existsSetting == null)
        {
          _context.SettingMail.Add(settingMail);
          await _context.SaveChangesAsync();
        }
        else
        {
          settingMail.Id = existsSetting.Id;
          _context.SettingMail
              .Update(settingMail);
          await _context.SaveChangesAsync();
        }
      }
      catch (DbUpdateException error)
      {
        throw new DbUpdateException(error.Message, error.InnerException);
      }
      catch (Exception error)
      {
        throw new Exception(error.Message);
      }
    }
  }
}
