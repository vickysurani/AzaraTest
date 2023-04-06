using System.Data;

namespace azara.api.Helpers;

public class SeedHelpers
{
    #region Constructor

    AzaraContext AzaraContext { get; set; }

    AdminConfigs AdminConfigs { get; set; }

    ICrypto Crypto { get; set; }

    public SeedHelpers(AzaraContext AzaraContext, ICrypto Crypto, IOptions<AdminConfigs> options)
    {
        this.AzaraContext = AzaraContext;
        this.Crypto = Crypto;
        AdminConfigs = options.Value;
    }

    #endregion Constructor

    public async Task Seed()
    {

        #region Admin Seed
        if (!AzaraContext.Admin.Any())
        {
            var admin = new AdminEntity
            {
                Name = AdminConfigs.Name,
                UserName = AdminConfigs.UserName,
                EmailId = AdminConfigs.EmailId,
                Mobile = AdminConfigs.Mobile,
                Password = Crypto.EncryptPassword(AdminConfigs.Password),
                IsSubAdmin = false,
                //PermissionJson = "[{\"Id\":\"f2abce76-4573-4fa7-ad4e-05518ad9cd0b\",\"Name\":\"Chat\",\"FontIconName\":\"chat\",\"PermissionCsv\":\"View\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"chat-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"4726608a-4d89-4849-b82e-0f197446bff5\",\"Name\":\"Rewards\",\"FontIconName\":\"card_membership\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"rewards\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"690b3281-fd6d-4c99-b309-12ecc384ca5d\",\"Name\":\"Dashboard\",\"FontIconName\":\"dashboard\",\"PermissionCsv\":\"View\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/dashboard\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"1309b340-2777-46e7-a6d3-382938f39e99\",\"Name\":\"Team\",\"FontIconName\":\"groups\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/teamlist\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"fe22e6c8-faa9-4b8d-9a1f-393b80900bc1\",\"Name\":\"Product-Sub-Category\",\"FontIconName\":\"category\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/product_sub_category\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"85b53593-8113-4258-8d42-4438e775571c\",\"Name\":\"Product-Category\",\"FontIconName\":\"inventory\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/product_category\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"cce20d66-83f1-4a2f-82b4-7d3eef7c47bc\",\"Name\":\"Sub-Admin\",\"FontIconName\":\"person_add\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/subadmin-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"7d978170-e32e-464e-a821-8e638cd23ad2\",\"Name\":\"User\",\"FontIconName\":\"person_2\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/user-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"481a901e-7aac-4469-b5bf-980d93479765\",\"Name\":\"Store\",\"FontIconName\":\"store\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/store\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"47391114-c02d-4bdf-95fb-a2d9fcea7261\",\"Name\":\"PointManagement\",\"FontIconName\":\"person_pin_circle\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"pointManagement\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"1d0a5cb5-73bc-4e6b-8b2a-bc9ea8852d6d\",\"Name\":\"Blog\",\"FontIconName\":\"newspaper\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/blog\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"ad667f0e-9bb3-4fff-a103-bfcd8711b40d\",\"Name\":\"Contact-Request\",\"FontIconName\":\"contact_mail\",\"PermissionCsv\":\"View\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"contact-request\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"15004055-c7d8-48e0-9363-c8ccd3c61015\",\"Name\":\"POS-User\",\"FontIconName\":\"person_2\",\"PermissionCsv\":\"View\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/user-pos-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"43c701d1-f9b6-4c1c-986a-cc0b7061145a\",\"Name\":\"Product\",\"FontIconName\":\"inventory_2\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/product\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"e369d7fe-3e3c-42d4-8ef0-cecf00d03c59\",\"Name\":\"Coupons\",\"FontIconName\":\"redeem\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"coupon\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"2e2208b8-fe4d-4d6d-accd-d5cf6f7a64b8\",\"Name\":\"Point\",\"FontIconName\":\"push_pin\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"points\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"118eeed7-1952-470b-8b05-d6cc950a0ac4\",\"Name\":\"FAQs\",\"FontIconName\":\"quiz\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"faqs-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"3038fdc7-d037-4dae-acdd-d84766d10283\",\"Name\":\"PunchCard\",\"FontIconName\":\"add_card\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"punchcard\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"5d06df1d-c5e5-4b89-918d-d96347539544\",\"Name\":\"POS-Inventory\",\"FontIconName\":\"warehouse\",\"PermissionCsv\":\"View\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/PosInventory-pos-list\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"00db4475-b376-4625-81c4-e01f990bd0c6\",\"Name\":\"Contest\",\"FontIconName\":\"stars\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/contest\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"92dc5bce-ba7f-4bd9-85b8-e0ac9bb7de0a\",\"Name\":\"Deal\",\"FontIconName\":\"article\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"deal\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"ae2fa859-4d38-4700-b45f-e5a00e4d6baf\",\"Name\":\"Banner\",\"FontIconName\":\"image\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"banner\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"4760abe6-004c-4596-a4ac-e6ba21a81a1d\",\"Name\":\"Offers\",\"FontIconName\":\"celebration\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"offers\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"a07f7cba-6a7d-4ac8-a444-ebfc3522a722\",\"Name\":\"Events\",\"FontIconName\":\"event\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/event\",\"Priority\":0,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"7868c7c8-68d7-47db-a558-f103bf2122f1\",\"Name\":\"PointAssignUsers\",\"FontIconName\":\"point\",\"PermissionCsv\":\"Add,Update,View,Delete,Status\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/pointassign-users\",\"Priority\":25,\"IsSelected\":true,\"MenuList\":null}]"
                PermissionJson = "[{\"Id\":\"ff696627-19c1-4f4f-9126-0129c5e29d0f\",\"Name\":\"Chat\",\"FontIconName\":\"chat\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"chat-list\",\"Priority\":13,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"8cd6a491-8eb9-4282-b62e-1554868efb08\",\"Name\":\"Events\",\"FontIconName\":\"event\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/event\",\"Priority\":11,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"04e8656c-f2a7-4f25-af8d-3bc8b1f91db6\",\"Name\":\"Blog\",\"FontIconName\":\"newspaper\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/blog\",\"Priority\":15,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"d3dfb996-aedb-4b3a-8c44-48e977668fa5\",\"Name\":\"Contest\",\"FontIconName\":\"stars\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/contest\",\"Priority\":12,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"ceb68424-83b4-4ecc-831a-4bc0b08499e0\",\"Name\":\"Store\",\"FontIconName\":\"store\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/store\",\"Priority\":6,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"e18a9969-a172-4358-9d09-4c95415aa2d3\",\"Name\":\"Coupons\",\"FontIconName\":\"redeem\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"coupon\",\"Priority\":9,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"7436448b-533b-41f6-8216-4ef0d4414823\",\"Name\":\"Contact-Request\",\"FontIconName\":\"contact_mail\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"contact-request\",\"Priority\":16,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"1ad716bc-3f20-4303-95cc-4f2fdd07b56a\",\"Name\":\"Dashboard\",\"FontIconName\":\"dashboard\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/dashboard\",\"Priority\":1,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"688f33ce-47bf-4ef2-9bd3-59d142822a6a\",\"Name\":\"Point\",\"FontIconName\":\"push_pin\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"points\",\"Priority\":17,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"ae872fcb-e24c-4f99-82c2-5b516e541ef6\",\"Name\":\"PointManagement\",\"FontIconName\":\"person_pin_circle\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"pointManagement\",\"Priority\":18,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"b6f2022a-9117-47b9-ab11-5c792d60f740\",\"Name\":\"Banner\",\"FontIconName\":\"image\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"banner\",\"Priority\":20,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"465b89a5-451a-44e8-82da-653e3033d757\",\"Name\":\"Sub-Category\",\"FontIconName\":\"category\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/product_sub_category\",\"Priority\":3,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"9c428f79-ff94-4a22-956d-66c517ec899a\",\"Name\":\"Category\",\"FontIconName\":\"inventory\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/product_category\",\"Priority\":2,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"7a38768a-b5ae-4127-bbba-6db3e23e494c\",\"Name\":\"User\",\"FontIconName\":\"person_2\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/user-list\",\"Priority\":7,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"e5064b5d-39c4-404f-b036-77eb93aeb12a\",\"Name\":\"POS-User\",\"FontIconName\":\"person_2\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/user-pos-list\",\"Priority\":8,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"e60fab81-9d28-4c40-b0af-9cad4775492a\",\"Name\":\"PointAssign\",\"FontIconName\":\"qr_code_scanner\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"pointassign-users\",\"Priority\":19,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"4f174405-4e74-45c5-95aa-a289792273b7\",\"Name\":\"Inventory\",\"FontIconName\":\"warehouse\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/PosInventory-pos-list\",\"Priority\":4,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"4f5a3c10-340a-4ea6-914d-c02e98addc4f\",\"Name\":\"Sub-Admin\",\"FontIconName\":\"person_add\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"/subadmin-list\",\"Priority\":5,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"f7849381-7b94-4f04-b4d3-d895a72dcb3c\",\"Name\":\"FAQs\",\"FontIconName\":\"quiz\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"faqs-list\",\"Priority\":14,\"IsSelected\":true,\"MenuList\":null},{\"Id\":\"bfa98171-e2fb-4523-ac16-ea555dbfaa10\",\"Name\":\"Rewards\",\"FontIconName\":\"card_membership\",\"PermissionCsv\":\"Add,Update,View,Delete,Status,Export\",\"ParentId\":null,\"IsChild\":false,\"PageUrl\":\"rewards\",\"Priority\":10,\"IsSelected\":true,\"MenuList\":null}]"
                //Created = SettingConsts.CurrentDateTime,
            };

            await AzaraContext.Admin.AddRangeAsync(admin);

            await AzaraContext.SaveChangesAsync();


        }
        if (!AzaraContext.Menu.Any())
        {

            //Check and Insert Menus

            //if (AzaraContext.Menu.Any()) return;  // Early Return

            var menu = new List<MenuEntity>
            {
                //new MenuEntity { Name = "Team", FontIconName = "groups", PageUrl = "/teamlist" },
                //new MenuEntity { Name = "Deal", FontIconName = "article", PageUrl = "deal" },
                //new MenuEntity { Name = "Product", FontIconName = "inventory_2", PageUrl = "/product" },
                //new MenuEntity { Name = "Offers", FontIconName = "celebration", PageUrl = "offers" },
                //new MenuEntity { Name = "PunchCard", FontIconName = "add_card", PageUrl = "punchcard" },

                new MenuEntity {Name = "Dashboard", FontIconName = "dashboard", PageUrl = "/dashboard", Priority=1,IsSelected=false, PermissionCsv="View" },
                new MenuEntity {Name = "Category", FontIconName = "inventory", PageUrl = "/product_category", Priority=2,IsSelected=false, PermissionCsv="Add,Update,View,Delete,Status,Export" },
                new MenuEntity {Name = "Sub-Category", FontIconName = "category", PageUrl = "/product_sub_category", Priority = 3, IsSelected = false, PermissionCsv="Add,Update,View,Delete,Status,Export" },
                new MenuEntity {Name = "Inventory", FontIconName = "warehouse", PageUrl = "/PosInventory-pos-list", Priority = 4, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Sub-Admin", FontIconName = "person_add", PageUrl = "/subadmin-list", Priority = 5, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Store", FontIconName = "store", PageUrl = "/store", Priority = 6, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "User", FontIconName = "person_2", PageUrl = "/user-list", Priority = 7, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "POS-User", FontIconName = "person_2", PageUrl = "/user-pos-list", Priority = 8, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Coupons", FontIconName = "redeem", PageUrl = "coupon", Priority = 9, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Rewards", FontIconName = "card_membership", PageUrl = "rewards", Priority = 10 , IsSelected = false, PermissionCsv="Add,Update,View,Delete,Status,Export" },
                new MenuEntity {Name = "Events", FontIconName = "event", PageUrl = "/event", Priority = 11, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Contest", FontIconName = "stars", PageUrl = "/contest", Priority = 12, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Chat", FontIconName = "chat", PageUrl = "chat-list", Priority=13,IsSelected=false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "FAQs", FontIconName = "quiz", PageUrl = "faqs-list", Priority = 14, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Blog", FontIconName = "newspaper", PageUrl = "/blog", Priority = 15, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Contact-Request", FontIconName = "contact_mail", PageUrl = "contact-request", Priority = 16, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Point", FontIconName = "push_pin", PageUrl = "points", Priority = 17, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "PointManagement", FontIconName = "person_pin_circle", PageUrl = "pointManagement", Priority = 18, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "PointAssign", FontIconName = "qr_code_scanner", PageUrl = "pointassign-users", Priority = 19, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},
                new MenuEntity {Name = "Banner", FontIconName = "image", PageUrl = "banner", Priority = 20, IsSelected = false, PermissionCsv = "Add,Update,View,Delete,Status,Export"},

            };

            await AzaraContext.Menu.AddRangeAsync(menu);
            await AzaraContext.SaveChangesAsync();
        }
        if (!AzaraContext.MasterRoles.Any())
        {

            var role = new List<MasterRolesEntity>
        {
            new MasterRolesEntity
            {
                Name = "admin",
                DisplayName = "Admin"
            },
            new MasterRolesEntity
            {
                Name = "sub admin",
                DisplayName = "Sub Admin"
            }
        };

            await AzaraContext.MasterRoles.AddRangeAsync(role);

            await AzaraContext.SaveChangesAsync();
        }
        #endregion Admin Seed

        #region Banner Seed
        if (!AzaraContext.Banner.Any())
        {
            var BannerImage = new List<BannerEntity>
            {
                 new BannerEntity{BannerImage1 = "https://azaradocument.blob.core.windows.net/user/DefaultIMG.png",   BannerImage2 = "https://azaradocument.blob.core.windows.net/user/DefaultIMG.png", BannerImage3 = "https://azaradocument.blob.core.windows.net/user/DefaultIMG.png", Active=true, Deleted=false, Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
            };

            await AzaraContext.Banner.AddRangeAsync(BannerImage);

            await AzaraContext.SaveChangesAsync();
        }
        #endregion Banner Seed

        #region Stores Seed
        if (!AzaraContext.Stores.Any())
        {
            var store = new List<StoreEntity>
            {
                new StoreEntity{Name = "Store1",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore01",  Address="USA", Description=null },
                new StoreEntity{Name = "Store2",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore02",  Address="USA", Description=null },
                new StoreEntity{Name = "Store3",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore03",  Address="USA", Description=null },
                new StoreEntity{Name = "Store4",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore04",  Address="USA", Description=null },
                new StoreEntity{Name = "Store5",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore05",  Address="USA", Description=null },
                new StoreEntity{Name = "Store6",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore06",  Address="USA", Description=null },
                new StoreEntity{Name = "Store7",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore07",  Address="USA", Description=null },
                new StoreEntity{Name = "Store8",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore08",  Address="USA", Description=null },
                new StoreEntity{Name = "Store9",   Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore09",  Address="USA", Description=null },
                new StoreEntity{Name = "Store10",  Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore10", Address="USA", Description=null },
                new StoreEntity{Name = "Store11",  Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore11", Address="USA", Description=null },
                new StoreEntity{Name = "Store12",  Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore12", Address="USA", Description=null },
                new StoreEntity{Name = "Stores13", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore13", Address="USA", Description=null },
                new StoreEntity{Name = "Stores14", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore14", Address="USA", Description=null },
                new StoreEntity{Name = "Stores15", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore15", Address="USA", Description=null },
                new StoreEntity{Name = "Stores16", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore16", Address="USA", Description=null },
                new StoreEntity{Name = "Stores17", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore17", Address="USA", Description=null },
                new StoreEntity{Name = "Stores18", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore18", Address="USA", Description=null },
                new StoreEntity{Name = "Stores19", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore20", Address="USA", Description=null },
                new StoreEntity{Name = "Stores20", Image = "['https://azaradocument.blob.core.windows.net/product/5d489df8-fe67-45f2-bd29-101e3c210c15.png']" , Location = "USA", EmailId="test@gmail.com", ContactNumber="123456789", Latitude="37.0902", Longitude="95.7129", Active=true, Deleted=false, Code="OnHandStore19", Address="USA", Description=null },
                            };
            await AzaraContext.Stores.AddRangeAsync(store);
            await AzaraContext.SaveChangesAsync();
        }
        else
        {
            return;
        }

        #endregion Stores Seed


    }
}