using Microsoft.EntityFrameworkCore;
using TipperERP.Domain.Entities;

namespace TipperERP.Infrastructure.Data;

public class TipperErpDbContext : DbContext
{
	public TipperErpDbContext(DbContextOptions<TipperErpDbContext> options) : base(options)
	{
	}

	public DbSet<CompanyMaster> Companies => Set<CompanyMaster>();
	public DbSet<BranchMaster> Branches => Set<BranchMaster>();
	public DbSet<RoleMaster> Roles => Set<RoleMaster>();
	public DbSet<UserMaster> Users => Set<UserMaster>();
	public DbSet<CustomerMaster> Customers => Set<CustomerMaster>();
	public DbSet<SiteMaster> Sites => Set<SiteMaster>();
	public DbSet<DumpingMaster> DumpingSites => Set<DumpingMaster>();
	public DbSet<RouteMaster> Routes => Set<RouteMaster>();
	public DbSet<MaterialTypeMaster> MaterialTypes => Set<MaterialTypeMaster>();
	public DbSet<TripRateMaster> TripRates => Set<TripRateMaster>();
	public DbSet<TipperMaster> Tippers => Set<TipperMaster>();
	public DbSet<FinancierMaster> Financiers => Set<FinancierMaster>();
	public DbSet<TipperDocumentMaster> TipperDocuments => Set<TipperDocumentMaster>();
	public DbSet<DocumentTypeMaster> DocumentTypes => Set<DocumentTypeMaster>();
	public DbSet<DriverMaster> Drivers => Set<DriverMaster>();
	public DbSet<DriverDocumentMaster> DriverDocuments => Set<DriverDocumentMaster>();
	public DbSet<InsuranceCompanyMaster> InsuranceCompanies => Set<InsuranceCompanyMaster>();
	public DbSet<LoanEmiMaster> LoanEmis => Set<LoanEmiMaster>();
	public DbSet<PaymentModeMaster> PaymentModes => Set<PaymentModeMaster>();
	public DbSet<ExpenseTypeMaster> ExpenseTypes => Set<ExpenseTypeMaster>();
	public DbSet<IncomeTypeMaster> IncomeTypes => Set<IncomeTypeMaster>();
	public DbSet<TaxMaster> Taxes => Set<TaxMaster>();
	public DbSet<AuditLogMaster> AuditLogs => Set<AuditLogMaster>();
	public DbSet<ActionMaster> Actions => Set<ActionMaster>();
	public DbSet<AttachmentMaster> Attachments => Set<AttachmentMaster>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<CompanyMaster>(entity =>
		{
			entity.ToTable("CompanyMaster");
			entity.HasKey(e => e.CompanyId);
			entity.Property(e => e.CompanyCode).HasMaxLength(20).IsRequired();
			entity.Property(e => e.CompanyName).HasMaxLength(200).IsRequired();
			entity.HasIndex(e => e.CompanyCode).IsUnique().HasFilter("[IsDeleted] = 0");
		});
		modelBuilder.Entity<BranchMaster>(entity =>
		{
			entity.ToTable("BranchMaster");
			entity.HasKey(e => e.BranchId);
			entity.Property(e => e.BranchCode).HasMaxLength(20).IsRequired();
			entity.Property(e => e.BranchName).HasMaxLength(200).IsRequired();
			entity.HasIndex(e => e.BranchCode).IsUnique().HasFilter("[IsDeleted] = 0");

			entity.HasOne(b => b.Company)
				  .WithMany(c => c.Branches)
				  .HasForeignKey(b => b.CompanyId)
				  .OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<RoleMaster>(entity =>
		{
			entity.ToTable("RoleMaster");
			entity.HasKey(e => e.RoleId);
			entity.Property(e => e.RoleName).HasMaxLength(50).IsRequired();
			entity.HasIndex(e => e.RoleName).IsUnique().HasFilter("[IsDeleted] = 0");
		});
		modelBuilder.Entity<UserMaster>(entity =>
		{
			entity.ToTable("UserMaster");
			entity.HasKey(e => e.UserId);
			entity.Property(e => e.FullName).HasMaxLength(100).IsRequired();
			entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
			entity.Property(e => e.PasswordHash).HasMaxLength(200).IsRequired();
			entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
			entity.HasIndex(e => e.Username).IsUnique().HasFilter("[IsDeleted] = 0");

			entity.HasOne(u => u.Company)
				  .WithMany(c => c.Users)
				  .HasForeignKey(u => u.CompanyId)
				  .OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.Branch)
				  .WithMany(b => b.Users)
				  .HasForeignKey(u => u.BranchId)
				  .OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(u => u.Role)
				  .WithMany(r => r.Users)
				  .HasForeignKey(u => u.RoleId)
				  .OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<CustomerMaster>(entity =>
		{
			entity.ToTable("CustomerMaster");
			entity.HasKey(e => e.CustomerId);
			entity.Property(e => e.CustomerId).HasColumnName("CustomerId");

			entity.Property(e => e.CustomerCode).HasMaxLength(50).IsRequired();
			entity.Property(e => e.CustomerName).HasMaxLength(200).IsRequired();
			entity.Property(e => e.Email).HasMaxLength(100);

			entity.HasIndex(e => e.CustomerCode)
				  .IsUnique()
				  .HasFilter("[IsDeleted] = 0");

			entity.HasOne(e => e.Company)
				  .WithMany()
				  .HasForeignKey(e => e.CompanyId)
				  .OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Branch)
				  .WithMany()
				  .HasForeignKey(e => e.BranchId)
				  .OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<SiteMaster>(entity =>
		{
			entity.ToTable("SiteMaster");
			entity.HasKey(e => e.SiteId);

			entity.Property(e => e.SiteName)
				  .HasMaxLength(200)
				  .IsRequired();

			entity.Property(e => e.Address)
				  .HasMaxLength(300);

			entity.HasOne(e => e.Customer)
				  .WithMany()
				  .HasForeignKey(e => e.CustomerId)
				  .OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<DumpingMaster>(entity =>
		{
			entity.ToTable("DumpingMaster");
			entity.HasKey(e => e.DumpingId);

			entity.Property(e => e.DumpingName)
				  .HasMaxLength(200)
				  .IsRequired();

			entity.Property(e => e.Address)
				  .HasMaxLength(300);

			entity.HasOne(e => e.Customer)
				  .WithMany()
				  .HasForeignKey(e => e.CustomerId)
				  .OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<RouteMaster>(entity =>
		{
			entity.ToTable("RouteMaster");
			entity.HasKey(e => e.RouteId);

			entity.Property(e => e.RouteName)
				.HasMaxLength(200)
				.IsRequired();

			entity.Property(e => e.ApproxDistanceKm)
				.HasColumnType("decimal(10,2)");

			entity.HasOne(e => e.Customer)
				.WithMany()
				.HasForeignKey(e => e.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Site)
				.WithMany()
				.HasForeignKey(e => e.SiteId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Dumping)
				.WithMany()
				.HasForeignKey(e => e.DumpingId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<MaterialTypeMaster>(entity =>
		{
			entity.ToTable("MaterialTypeMaster");
			entity.HasKey(e => e.MaterialTypeId);

			entity.Property(e => e.MaterialCode)
				.HasMaxLength(20)
				.IsRequired();

			entity.Property(e => e.MaterialName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.Description)
				.HasMaxLength(200);

			entity.HasIndex(e => e.MaterialCode)
				.IsUnique()
				.HasFilter("[IsDeleted] = 0");
		});
		modelBuilder.Entity<TripRateMaster>(entity =>
		{
			entity.ToTable("TripRateMaster");
			entity.HasKey(e => e.RateId);

			entity.Property(e => e.RateType)
				.HasMaxLength(20)
				.IsRequired();

			entity.Property(e => e.RateValue)
				.HasColumnType("decimal(18,2)");

			entity.Property(e => e.Currency)
				.HasMaxLength(10);

			entity.HasOne(e => e.Customer)
				.WithMany()
				.HasForeignKey(e => e.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Site)
				.WithMany()
				.HasForeignKey(e => e.SiteId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Dumping)
				.WithMany()
				.HasForeignKey(e => e.DumpingId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Route)
				.WithMany()
				.HasForeignKey(e => e.RouteId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.MaterialType)
				.WithMany()
				.HasForeignKey(e => e.MaterialTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<TipperMaster>(entity =>
		{
			entity.ToTable("TipperMaster");
			entity.HasKey(e => e.TipperId);

			entity.Property(e => e.TipperNumber)
				.IsRequired()
				.HasMaxLength(50);

			entity.Property(e => e.RegistrationNumber).HasMaxLength(50);
			entity.Property(e => e.ChassisNumber).HasMaxLength(50);
			entity.Property(e => e.EngineNumber).HasMaxLength(50);
			entity.Property(e => e.Make).HasMaxLength(100);
			entity.Property(e => e.Model).HasMaxLength(100);
			entity.Property(e => e.OwnershipType).HasMaxLength(50);

			entity.Property(e => e.PurchaseAmount)
				.HasColumnType("decimal(18,2)");

			entity.HasOne(e => e.Company)
				.WithMany()
				.HasForeignKey(e => e.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Branch)
				.WithMany()
				.HasForeignKey(e => e.BranchId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.Financier)
				.WithMany()
				.HasForeignKey(e => e.FinancierId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<FinancierMaster>(entity =>
		{
			entity.ToTable("FinancierMaster");
			entity.HasKey(e => e.FinancierId);

			entity.Property(e => e.FinancierName)
				.HasMaxLength(200)
				.IsRequired();

			entity.Property(e => e.ContactPerson).HasMaxLength(100);
			entity.Property(e => e.Phone).HasMaxLength(50);
			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.Address).HasMaxLength(300);
		});
		modelBuilder.Entity<TipperDocumentMaster>(entity =>
		{
			entity.ToTable("TipperDocumentMaster");
			entity.HasKey(e => e.TipperDocumentId);

			entity.Property(e => e.DocumentNumber).HasMaxLength(100);
			entity.Property(e => e.Remarks).HasMaxLength(200);

			entity.HasOne(e => e.Tipper)
				.WithMany()
				.HasForeignKey(e => e.TipperId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.DocumentType)
				.WithMany()
				.HasForeignKey(e => e.DocumentTypeId)
				.OnDelete(DeleteBehavior.Restrict);

			entity.HasOne(e => e.InsuranceCompany)
				.WithMany()
				.HasForeignKey(e => e.InsuranceCompanyId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<DocumentTypeMaster>(entity =>
		{
			entity.ToTable("DocumentTypeMaster");
			entity.HasKey(e => e.DocumentTypeId);

			entity.Property(e => e.DocumentTypeName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.Category)
				.HasMaxLength(50);
		});
		modelBuilder.Entity<DriverMaster>(entity =>
		{
			entity.ToTable("DriverMaster");
			entity.HasKey(e => e.DriverId);

			entity.Property(e => e.DriverCode)
				.HasMaxLength(20)
				.IsRequired();

			entity.Property(e => e.DriverName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.MobileNo).HasMaxLength(20);
			entity.Property(e => e.AlternateMobileNo).HasMaxLength(20);
			entity.Property(e => e.Address).HasMaxLength(300);

			entity.Property(e => e.LicenseNumber).HasMaxLength(100);

			entity.HasOne(e => e.Company)
				.WithMany()
				.HasForeignKey(e => e.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<DriverDocumentMaster>(entity =>
		{
			entity.ToTable("DriverDocumentMaster");
			entity.HasKey(e => e.DriverDocumentId);

			entity.Property(e => e.DocumentNumber).HasMaxLength(100);
			entity.Property(e => e.Remarks).HasMaxLength(200);

			entity.HasOne(e => e.Driver)
				.WithMany()
				.HasForeignKey(e => e.DriverId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.DocumentType)
				.WithMany()
				.HasForeignKey(e => e.DocumentTypeId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<InsuranceCompanyMaster>(entity =>
		{
			entity.ToTable("InsuranceCompanyMaster");
			entity.HasKey(e => e.InsuranceCompanyId);

			entity.Property(e => e.CompanyName)
				.HasMaxLength(200)
				.IsRequired();

			entity.Property(e => e.ContactNumber)
				.HasMaxLength(50);

			entity.Property(e => e.Email)
				.HasMaxLength(100);
		});
		modelBuilder.Entity<LoanEmiMaster>(entity =>
		{
			entity.ToTable("LoanEmiMaster");
			entity.HasKey(e => e.EmiId);

			entity.Property(e => e.MonthlyEmiAmount)
				.HasColumnType("decimal(18,2)");

			entity.Property(e => e.Remarks)
				.HasMaxLength(200);

			entity.HasOne(e => e.Tipper)
				.WithMany()
				.HasForeignKey(e => e.TipperId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(e => e.Financier)
				.WithMany()
				.HasForeignKey(e => e.FinancierId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		modelBuilder.Entity<PaymentModeMaster>(entity =>
		{
			entity.ToTable("PaymentModeMaster");
			entity.HasKey(e => e.PaymentModeId);

			entity.Property(e => e.PaymentModeName)
				.HasMaxLength(50)
				.IsRequired();

			entity.Property(e => e.Description)
				.HasMaxLength(200);
		});
		modelBuilder.Entity<ExpenseTypeMaster>(entity =>
		{
			entity.ToTable("ExpenseTypeMaster");
			entity.HasKey(e => e.ExpenseTypeId);

			entity.Property(e => e.ExpenseTypeName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.Category)
				.HasMaxLength(50);

			entity.Property(e => e.Description)
				.HasMaxLength(200);
		});
		modelBuilder.Entity<IncomeTypeMaster>(entity =>
		{
			entity.ToTable("IncomeTypeMaster");
			entity.HasKey(e => e.IncomeTypeId);

			entity.Property(e => e.IncomeTypeName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.Description)
				.HasMaxLength(200);
		});
		modelBuilder.Entity<TaxMaster>(entity =>
		{
			entity.ToTable("TaxMaster");
			entity.HasKey(e => e.TaxId);

			entity.Property(e => e.TaxName)
				.HasMaxLength(50)
				.IsRequired();

			entity.Property(e => e.Country)
				.HasMaxLength(50);

			entity.Property(e => e.TaxPercentage)
				.HasColumnType("decimal(5,2)");
		});
		modelBuilder.Entity<AuditLogMaster>(entity =>
		{
			entity.ToTable("AuditLogMaster");
			entity.HasKey(e => e.AuditId);

			entity.Property(e => e.EntityName)
				.HasMaxLength(100)
				.IsRequired();

			entity.Property(e => e.Action)
				.HasMaxLength(20)
				.IsRequired();
		});
		modelBuilder.Entity<ActionMaster>(entity =>
		{
			entity.ToTable("ActionMaster");
			entity.HasKey(e => e.ActionId);

			entity.Property(e => e.ActionName)
				.HasMaxLength(50)
				.IsRequired();

			entity.Property(e => e.Description)
				.HasMaxLength(200);
		});
		modelBuilder.Entity<AttachmentMaster>(entity =>
		{
			entity.ToTable("AttachmentMaster");
			entity.HasKey(e => e.AttachmentId); // Define the primary key
		});
	}
}
