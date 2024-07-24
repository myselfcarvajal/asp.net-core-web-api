namespace AwpaAcademic.Api.Data;

public abstract class BaseRepository : IBaseRepository
{
    private readonly AwpaAcademicDbContext _awpaAcademicDbContext;
    public BaseRepository(AwpaAcademicDbContext awpaAcademicDbContext)
    {
        _awpaAcademicDbContext = awpaAcademicDbContext;
    }

    public void SaveChanges()
    {
        _awpaAcademicDbContext.SaveChanges();
    }
}
