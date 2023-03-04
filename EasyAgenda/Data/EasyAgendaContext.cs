using EasyAgendaBase.Model;
using Microsoft.EntityFrameworkCore;

namespace EasyAgenda.Model
{
  public sealed class EasyAgendaContext : DbContext
  {
    public DbSet<Agenda> Agenda { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<People> People { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Professional> Professionals { get; set; } = null!;
    public DbSet<Schedule> Schedules { get; set; } = null!;
    public DbSet<ScheduleCancelled> SchedulesCancelled { get; set; } = null!;
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Status> Status { get; set; } = null!;
    public DbSet<State> States { get; set; } = null!;
    public DbSet<SettingMail> SettingMail { get; set; } = null!;

    public EasyAgendaContext(DbContextOptions<EasyAgendaContext> contextOptions) : base(contextOptions)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Agenda>().HasKey(agenda => agenda.Id);

      modelBuilder.Entity<Address>().HasKey(address => address.Id);

      modelBuilder.Entity<People>().HasKey(people => people.Id);

      modelBuilder.Entity<Customer>().HasKey(customer => customer.Id);

      modelBuilder.Entity<Professional>().HasKey(professional => professional.Id);

      modelBuilder.Entity<Schedule>().HasKey(schedule => new { schedule.ProfessionalId, schedule.CustomerId, schedule.AgendaId });

      modelBuilder.Entity<ScheduleCancelled>().HasKey(scheduleCancelled => scheduleCancelled.Id);

      modelBuilder.Entity<Customer>()
          .HasOne(customer => customer.People)
          .WithOne()
          .HasForeignKey<Customer>(customer => customer.PeopleId);

      modelBuilder.Entity<Professional>()
          .HasOne(professional => professional.People)
          .WithOne()
          .HasForeignKey<Professional>(professional => professional.PeopleId);
    }
  }
}
