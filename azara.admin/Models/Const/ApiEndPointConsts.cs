namespace azara.admin.Models.Const;

public class ApiEndPointConsts
{
    const string Version = "v1/";

    //public const string AdminBaseRoute = "https://localhost:7155/";

    //public const string ClientBaseRoute = "https://localhost:7047/";

#if DEBUG
    public const string AdminBaseRoute = "https://localhost:7155/";

    public const string ClientBaseRoute = "https://localhost:7047/";

    public const string BaseRoute = "https://localhost:7234/api/" + Version;
    public const string BaseRouteWithoutVersion = "https://localhost:7234/";
#elif RELEASE
    //public const string AdminBaseRoute = " https://azara-admin.azurewebsites.net/" + Version;

    //public const string ClientBaseRoute = " https://azara-client.azurewebsites.net/" + Version;

    public const string BaseRoute = "https://apiazara.azurewebsites.net/api/" + Version;
    public const string BaseRouteWithoutVersion = "https://apiazara.azurewebsites.net/";

#elif UAT
    //public const string BaseRoute = "http://20.69.56.199:8085/api/" + Version;

    //public const string BaseRouteWithoutVersion = "http://20.69.56.199:8085/";

    public const string BaseRoute = "https://api.azara-app.com/api/" + Version;
    public const string BaseRouteWithoutVersion = "https://api.azara-app.com/";
#endif

    //public const string BaseRoute = "https://localhost:7234/api/" + Version;

    public class Admin
    {
        public const string AdminLogin = BaseRoute + "admin/login";

        public const string ForgotPassword = BaseRoute + "admin/forgot_password";

        public const string ResetPassword = BaseRoute + "admin/reset_password";

        public const string Logout = BaseRoute + "admin/logout";

        public const string IsAdminValid = BaseRoute + "admin/IsAdminValid";

        public const string Admindetails = BaseRoute + "admin/get_profile";

        public const string AdminUpdate = BaseRoute + "admin/update_profile";

        public const string ChangePassword = BaseRoute + "admin/change_password";

        public const string MenuList = BaseRoute + "admin/default_permission_list";
        public const string PrivacyPolicyList = BaseRoute + "privacy_policy/list";
        public const string PrivacyPolicyDelete = BaseRoute + "privacy_policy/delete";
        public const string PrivacyPolicyInsert = BaseRoute + "privacy_policy/insert";
        public const string PrivacyPolicyUpdate = BaseRoute + "privacy_policy/update";

    }

    public class Product
    {
        public const string InsertProduct = BaseRoute + "product/insert";

        public const string UpdateProduct = BaseRoute + "product/update";

        public const string ProductGetById = BaseRoute + "product/get_by_id";

        public const string ProductGetList = BaseRoute + "product/get_list";

        public const string ProductDelete = BaseRoute + "product/delete";

        public const string UploadProductImage = BaseRoute + "product/image";

        public const string GetStoreProductCategoryList = BaseRoute + "product/store_productCategory";
        public const string PosINventoryGetByID = BaseRoute + "pos_inventory/get_by_id";
        public const string PosInventoryImageUpdate = BaseRoute + "pos_inventory/update";
        public const string GetRewardata = BaseRoute + "dropdown/rewards";
    }

    public class ProductCategory
    {
        public const string InsertProductCategory = BaseRoute + "product_category/insert";

        public const string UpdateProductCategory = BaseRoute + "pos_sub_category/update";

        public const string ProductCategoryGetById = BaseRoute + "product_category/get_by_id";
        public const string PosSubProductCategoryGetById = BaseRoute + "pos_sub_category/get_by_id";

        public const string PosSubCategoryGetList = BaseRoute + "pos_sub_category/get_list";
        public const string ProductCategoryGetList = BaseRoute + "product_category/get_list";

        public const string ProductCategoryDelete = BaseRoute + "product_category/delete";

        public const string ProductCategoryStatusUpdate = BaseRoute + "product_category/status_update";

        public const string POSSubCategoryStatusUpdate = BaseRoute + "pos_sub_category/status_update";

    }

    public class ProductSubCategory
    {
        public const string InsertProductSubCategory = BaseRoute + "product_sub_category/insert";

        public const string UpdateProductSubCategory = BaseRoute + "product_sub_category/update";

        public const string ProductSubCategoryGetById = BaseRoute + "product_sub_category/get_by_id";

        public const string ProductSubCategoryGetList = BaseRoute + "product_sub_category/get_list";

        public const string ProductSubCategoryDelete = BaseRoute + "product_sub_category/delete";

        public const string ProductSubCategoryStatusUpdate = BaseRoute + "product_sub_category/status_update";

        public const string ProductSubCategoryListByProductCategoryId = BaseRoute + "product_sub_category/get_by_productSubCategoryid";
    }

    public class Store
    {
        public const string InsertStore = BaseRoute + "store/insert";

        public const string UpdateStore = BaseRoute + "store/update";

        public const string StoreGetById = BaseRoute + "store/get_by_id";

        public const string StoreGetList = BaseRoute + "store/get_list";

        public const string StoreDelete = BaseRoute + "store/delete";

        public const string UploadStoreImage = BaseRoute + "store/image";

        public const string StoreStatusUpdate = BaseRoute + "store/status_update";
    }

    public class User
    {
        public const string UserGetList = BaseRoute + "user/get_list";

        public const string POSUserGetList = BaseRoute + "pos_customer/get_list";

        public const string UserAdd = BaseRoute + "user/sign_up";

        public const string UserUpdate = BaseRoute + "user/update";

        public const string UserDelete = BaseRoute + "user/delete";

        public const string UserGetById = BaseRoute + "user/get_by_id";

        public const string UserStatusUpdate = BaseRoute + "user/status_update";

        public const string PosUserGetById = BaseRoute + "pos_customer/get_by_id";
    }

    public class Dropdown
    {
        public const string ProductCategoryDropdown = BaseRoute + "dropdown/product_category";

        public const string StoreDropdown = BaseRoute + "dropdown/store";

        public const string ProductSubCategoryDropdown = BaseRoute + "dropdown/product_sub_category";
    }

    public class FileConst
    {
        public const string UploadFile = BaseRoute + "common/uploadfile";
    }

    public class Blog
    {
        public const string InsertBlog = BaseRoute + "blog/insert";

        public const string UpdateBlog = BaseRoute + "blog/update";

        public const string BlogGetById = BaseRoute + "blog/get_by_id";

        public const string BlogGetList = BaseRoute + "blog/get_list";

        public const string BlogDelete = BaseRoute + "blog/delete";

    }

    public class Event
    {
        public const string InsertEvent = BaseRoute + "event/insert";

        public const string UpdateEvent = BaseRoute + "event/update";

        public const string GetEvents = BaseRoute + "event/get_list";

        public const string GetEventById = BaseRoute + "event/get_by_id";

        public const string DeleteEvent = BaseRoute + "event/delete";

        public const string DeleteStatusUpdate = BaseRoute + "event/status_update";
    }

    public class Contest
    {
        public const string InsertContest = BaseRoute + "contest/insert";

        public const string UpdateContest = BaseRoute + "contest/update";

        public const string ContestGetById = BaseRoute + "contest/get_by_id";

        public const string ContestGetList = BaseRoute + "contest/get_list";

        public const string ContestDelete = BaseRoute + "contest/delete";

        public const string ContestStatusUpdate = BaseRoute + "contest/status_update";
    }

    public class ContactRequest
    {
        public const string ContactRequestGetById = BaseRoute + "contact_us/get_by_id";

        public const string ContactRequestGetList = BaseRoute + "contact_us/get_list";

        public const string ContactRequestDelete = BaseRoute + "contact_us/delete";
    }

    public class Advertisement
    {
        public const string InsertAdvertisement = BaseRoute + "deals/insert";

        public const string UpdateAdvertisement = BaseRoute + "deals/update";

        public const string AdvertisementGetById = BaseRoute + "deals/get_by_id";

        public const string GetAdvertisementList = BaseRoute + "deals/get_list";

        public const string DeleteAdvertisement = BaseRoute + "deals/delete";

        public const string AdvertisementStatusUpdate = BaseRoute + "deals/status_update";
    }

    public class FAQs
    {
        public const string InsertFAQs = BaseRoute + "faq/insert";

        public const string UpdateFAQs = BaseRoute + "faq/update";

        public const string FAQsGetList = BaseRoute + "faq/get_list";

        public const string FAQsDelete = BaseRoute + "faq/delete";

        public const string FAQsGetById = BaseRoute + "faq/get_by_id";
    }

    public class Chat
    {
        public const string ChatGetById = BaseRoute + "chat/get_by_id";

        public const string ChatInsert = BaseRoute + "chat/insert";

        public const string ChatList = BaseRoute + "chat/get_chat_list";
    }

    public class Points
    {
        public const string InsertPoints = BaseRoute + "point/insert";

        public const string UpdatePoints = BaseRoute + "point/update";

        public const string PointsGetById = BaseRoute + "point/get_by_id";

        public const string PointsList = BaseRoute + "point/get_list";

        public const string DeletePoints = BaseRoute + "point/delete";

        public const string PointAssignedByAdmin = BaseRoute + "point/assigned_by_admin";
    }

    public class Coupons
    {
        public const string InsertCoupons = BaseRoute + "coupons/insert";

        public const string InsertCSVCoupons = BaseRoute + "coupons/insert_csv";

        public const string InsertBulkCoupons = BaseRoute + "coupons/import";

        public const string UpdateCoupons = BaseRoute + "coupons/update";

        public const string CouponsGetById = BaseRoute + "coupons/get_by_id";

        public const string GetCouponsList = BaseRoute + "coupons/get_list";

        public const string DeleteCoupons = BaseRoute + "coupons/delete";

        public const string CouponsStatusUpdate = BaseRoute + "coupons/status_update";
    }

    public class Offers
    {
        public const string InsertOffers = BaseRoute + "offer/insert";

        public const string UpdateOffers = BaseRoute + "offer/update";

        public const string OffersGetById = BaseRoute + "offer/get_by_id";

        public const string GetOffersList = BaseRoute + "offer/get_list";

        public const string DeleteOffers = BaseRoute + "offer/delete";

        public const string OffersStatusUpdate = BaseRoute + "offer/status_update";
    }

    public class Reward
    {
        public const string InsertRewards = BaseRoute + "rewards/insert";

        public const string InsertCSVReward = BaseRoute + "rewards/insert_csv";

        public const string InsertBulkReward = BaseRoute + "rewards/import";

        public const string UpdateRewards = BaseRoute + "rewards/update";

        public const string RewardsGetById = BaseRoute + "rewards/get_by_id";

        public const string GetRewardsList = BaseRoute + "rewards/get_list";

        public const string DeleteRewards = BaseRoute + "rewards/delete";

        public const string RewardsStatusUpdate = BaseRoute + "rewards/status_update";
    }

    public class POSCustomerData
    {
        public const string POSCustomerList = BaseRoute + "customer/select";
    }

    public class PosInventory
    {
        public const string PosInventoryList = BaseRoute + "customer/select";
        public const string PosInventoryListNew = BaseRoute + "pos_inventory/get_list";
        public const string InventoryStatusUpdate = BaseRoute + "pos_inventory/status_update";
    }

    public class PosCategory
    {
        public const string GetByIdDepartmentPos = BaseRoute + "pos_department/get_by_id";

        public const string GetListDepartmentPos = BaseRoute + "pos_department/get_list";

        public const string UpdateDepartmentPos = BaseRoute + "pos_department/update";
        public const string StatusUpdateDepartmentPos = BaseRoute + "pos_department/status_update";
    }

    public class Dashboard
    {
        public const string Dashboarddata = BaseRoute + "dashboard/retrive";
    }

    public class Punchcard
    {
        public const string InsertPunchcard = BaseRoute + "punchcard/insert";

        public const string InsertCSVPunchcard = BaseRoute + "punchcard/insert_csv";

        public const string InsertBulkPunchcard = BaseRoute + "punchcard/import";

        public const string UpdatePunchcard = BaseRoute + "punchcard/update";

        public const string PunchcardGetById = BaseRoute + "punchcard/get_by_id";

        public const string GetPunchcardList = BaseRoute + "punchcard/get_list";

        public const string DeletePunchcard = BaseRoute + "punchcard/delete";

        public const string PunchcardStatusUpdate = BaseRoute + "punchcard/status_update";
    }

    public class TeamManagement
    {
        public const string DefaultPermissionList = BaseRoute + "admin/default_permission_list";

        public const string TeamUpdate = BaseRoute + "team/update";

        public const string TeamInsert = BaseRoute + "team/insert";

        public const string TeamList = BaseRoute + "team/get_list";

        public const string TeamGetById = BaseRoute + "team/get_by_id";


    }

    public class Subadmin
    {
        public const string SubadminUpdate = BaseRoute + "sub_admin/update";
        public const string SubadminInsert = BaseRoute + "sub_admin/insert";
        public const string SubadminDelete = BaseRoute + "sub_admin/delete";
        public const string SubadminStatusUpdate = BaseRoute + "sub_admin/status_update";
        public const string SubadminGetById = BaseRoute + "sub_admin/get_by_id";
        public const string SubadminList = BaseRoute + "sub_admin/list";
        public const string TeamDromdown = BaseRoute + "dropdown/team_menu";
    }

    public class BannerImage
    {
        public const string BannerImageInsert = BaseRoute + "banner/insert";
        public const string BannerImageList = BaseRoute + "banner/get_list";
        public const string BannerUpdateImage = BaseRoute + "banner/update_image";

    }

    public class PointManagement
    {
        public const string PointManagementInsert = BaseRoute + "point_management/insert";
        public const string PointManagementUpdate = BaseRoute + "point_management/update";
        public const string PointManagementDelete = BaseRoute + "point_management/delete";
        public const string PointManagementStatusUpdate = BaseRoute + "point_management/status_update";
        public const string PointManagementGetById = BaseRoute + "point_management/get_by_id";
        public const string PointManagementList = BaseRoute + "point_management/get_list";
    }

}