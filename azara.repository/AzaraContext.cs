using azara.models;
using azara.models.Entities.POSSales;

namespace azara.repository;

public class AzaraContext : DbContext
{
    public AzaraContext(DbContextOptions<AzaraContext> options) : base(options)
    {
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity => base.Set<TEntity>();

    /**/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AdminViewEntiry>().ToView("VW_ADMIN").HasKey(t => t.Id);
        modelBuilder.Entity<CompanyviewEntity>().ToView("VW_Company").HasKey(t => t.Id);
        modelBuilder.Entity<CustomerViewEntity>().ToView("VW_Customer").HasKey(t => t.ListID);
        //modelBuilder.Entity<CustomerViewEntity>(
        //    opt =>
        //    {
        //        opt.ToView("VW_Customer");
        //        opt.HasNoKey();
        //    });
        //modelBuilder.Entity<ItemInventoryViewEntity>(
        //    opt =>
        //    {
        //        opt.ToView("VW_ItemInventory");
        //        opt.HasNoKey();
        //    });
        //modelBuilder.Entity<CustomerRewardEntity>(
        //    opt =>
        //    {
        //        //opt.ToView("VW_CustomerReward");
        //        opt.HasNoKey();
        //    });
    }

    public DbSet<AdminViewEntiry> AdminView { get; set; }
    public DbSet<CompanyviewEntity> Companyview { get; set; }
    public DbSet<CustomerViewEntity> CustomerView { get; set; }
    //public DbSet<ItemInventoryViewEntity> ItemInventoryView { get; set; }

    /**/
    public DbSet<UserEntity> User { get; set; }
    public DbSet<UserAccessTokenEntity> UserAccessToken { get; set; }
    public DbSet<ProductEntity> Product { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<StoreEntity> Stores { get; set; }
    public DbSet<PunchCardEntity> PunchCards { get; set; }
    public DbSet<CouponsEntity> Coupons { get; set; }
    public DbSet<EventsEntity> Events { get; set; }
    public DbSet<ContestsEntity> Contests { get; set; }
    public DbSet<RewardsEntity> Rewards { get; set; }
    public DbSet<MyRewardEntity> MyReward { get; set; }
    public DbSet<FacebookRewardSharingEntity> FacebookRewardSharings { get; set; }
    public DbSet<DealsEntity> Deals { get; set; }
    public DbSet<BlogsEntity> Blogs { get; set; }
    public DbSet<ContactUsEntity> ContactUs { get; set; }
    public DbSet<AdminEntity> Admin { get; set; }
    public DbSet<AdminAccessTokenEntity> AdminAccessToken { get; set; }
    public DbSet<ProductSubCategoryEntity> ProductSubCategory { get; set; }
    public DbSet<ProductRequestEntity> ProductRequest { get; set; }
    public DbSet<ReferralEntity> Referral { get; set; }
    public DbSet<FAQsEntity> FAQs { get; set; }
    public DbSet<ChatEntity> Chat { get; set; }
    public DbSet<PointsEntity> Point { get; set; }
    public DbSet<OffersEntity> Offers { get; set; }
    public DbSet<NotificationEntity> Notification { get; set; }
    public DbSet<TeamEntity> Team { get; set; }
    public DbSet<MenuEntity> Menu { get; set; }
    public DbSet<MasterRolesEntity> MasterRoles { get; set; }
    public DbSet<BannerEntity> Banner { get; set; }
    public DbSet<PointManagementEntity> PointManagement { get; set; }
    public DbSet<POSCustomerRewardsEntity> POSRewardTbl { get; set; }
    public DbSet<POSCustomerListEntity> POSCustomer { get; set; }
    public DbSet<POSInventoryEntity> POSInventory { get; set; }
    public DbSet<POSDepartmentEntity> POSDepartments { get; set; }
    public DbSet<POSStoreEntity> POSStores { get; set; }
    public DbSet<POSSalesReceipt> POSSalesReceipt { get; set; }
    public DbSet<POSSalesReceiptTMP> POSSalesReceiptTMP { get; set; }
    public DbSet<POSStore> POSStore { get; set; }
    public DbSet<POSCustomerDemoListEntity> POSCustomerDemo { get; set; }
    public DbSet<POSInventoryDemoEntity> POSInventoryDemo { get; set; }
    public DbSet<UserEventsEntity> UserEvents { get; set; }
    public DbSet<UserContestEntity> UserContests { get; set; }
    public DbSet<LastSalesDate> LastSalesDate { get; set; }
    public DbSet<POSDepartmentDemoEntity> POSDepartmentDemo { get; set; }
    public DbSet<POSSubCategoryEntity> POSSubCategory { get; set; }
    public DbSet<AboutUsEntity> AboutUs { get; set; }
    public DbSet<PrivacyPolicyEntity> PrivacyPolicy { get; set; }
}