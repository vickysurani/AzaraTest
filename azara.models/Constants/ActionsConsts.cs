namespace azara.models.Constants;

public class ActionsConsts
{
    public const string ApiVersion = "api/v1";

    //public const string AdminBaseRoute = "https://localhost:7155/";

    //public const string ClientBaseRoute = "https://localhost:7047/";
#if DEBUG
    public const string AdminBaseRoute = "https://localhost:44351/";
    public const string ClientBaseRoute = "https://localhost:7047/";

#elif RELEASE
    public const string AdminBaseRoute = "https://azaraadmin-production.azurewebsites.net/";
    public const string ClientBaseRoute = "https://azaraclient-production.azurewebsites.net/";

#elif UAT
    //public const string AdminBaseRoute = "http://20.69.56.199:8086";
    //public const string ClientBaseRoute = "http://20.69.56.199:8087";

    public const string AdminBaseRoute = "https://admin.azara-app.com/";
    public const string ClientBaseRoute = "https://client.azara-app.com/";
#endif

    public class User
    {
        public const string UserSignUp = "user/sign_up";

        public const string UserLogin = "user/sign_in";

        public const string ChangePassword = "user/change_password";

        public const string ForgotPassword = "user/forgot_password";

        public const string ResetPassword = "user/reset_password";

        //public const string OtpVerify = "user/otp_verify";

        public const string UserProfile = "user/profile";

        public const string UserUpdate = "user/update";

        public const string Logout = "user/logout";

        public const string UserGetList = "user/get_list";

        public const string UserGetById = "user/get_by_id";

        public const string UserDelete = "user/delete";

        public const string UesrProductRequest = "user/product_request";

        public const string IsUserValid = "user/IsUserValid";

        public const string UploadProfilePicture = "user/profile_picture/update";

        public const string UserList = "user/user_list";

        public const string UserStatusUpdate = "user/status_update";
    }

    public class Product
    {
        public const string InsertProduct = "product/insert";

        public const string UpdateProduct = "product/update";

        public const string ProductGetById = "product/get_by_id";

        public const string ProductGetList = "product/get_list";

        public const string ProductDelete = "product/delete";

        public const string UploadProductImage = "product/image";

        public const string GetStoreProductCategoryList = "product/store_productCategory";

        public const string ProductStatusUpdate = "product/status_update";

        public const string ProductList = "product/product_list";
    }

    public class ProductCategory
    {
        public const string InsertProductCategory = "product_category/insert";

        public const string UpdateProductCategory = "product_category/update";

        public const string ProductCategoryGetById = "product_category/get_by_id";

        public const string ProductCategoryGetList = "product_category/get_list";

        public const string ProductCategoryDelete = "product_category/delete";

        public const string ProductCategoryStatusUpdate = "product_category/status_update";

        public const string ProductCategoryList = "product_category/product_category_list";
    }

    public class ProductSubCategory
    {
        public const string InsertProductSubCategory = "product_sub_category/insert";

        public const string UpdateProductSubCategory = "product_sub_category/update";

        public const string ProductSubCategoryGetById = "product_sub_category/get_by_id";

        public const string ProductSubCategoryGetList = "product_sub_category/get_list";

        public const string ProductSubCategoryGetDelete = "product_sub_category/delete";

        public const string ProductSubCategoryStatusUpdate = "product_sub_category/status_update";

        public const string ProductSubCategoryListByProductCategoryId = "product_sub_category/get_by_productSubCategoryid";

        public const string ProductSubCategoryListByProductId = "product_sub_category/get_product_list";

        public const string ProductSubCategoryList = "product_sub_category/product_sub_category_list";
    }

    public class Store
    {
        public const string InsertStore = "store/insert";

        public const string UpdateStore = "store/update";

        public const string StoreGetById = "store/get_by_id";

        public const string StoreGetList = "store/get_list";

        public const string StoreDelete = "store/delete";

        public const string UploadStoreImage = "store/image";

        public const string StoreList = "store/store_list";

        public const string StoreStatusUpdate = "store/status_update";

    }

    public class Blog
    {
        public const string InsertBlog = "blog/insert";

        public const string UpdateBlog = "blog/update";

        public const string BlogGetById = "blog/get_by_id";

        public const string GetBlogList = "blog/get_list";

        public const string DeleteBlog = "blog/delete";

        public const string UploadBlogImage = "blog/image";

        public const string BlogList = "blog/blog_list";
    }

    public class Deals
    {
        public const string InsertDeals = "deals/insert";

        public const string UpdateDeals = "deals/update";

        public const string GetDealsList = "deals/get_list";

        public const string DeleteDeals = "deals/delete";

        public const string DealsGetById = "deals/get_by_id";

        public const string DealsStatusUpdate = "deals/status_update";

        public const string DealsList = "deals/deals_list";
    }

    public class ContactUs
    {
        public const string AddContactUs = "contact_us/add";

        public const string ContactGetById = "contact_us/get_by_id";

        public const string GetContactList = "contact_us/get_list";

        public const string DeleteContact = "contact_us/delete";

        public const string ContactList = "contact_us/contact_us_list";
    }

    public class AdminAccount
    {
        public const string AdminLogin = "admin/login";

        public const string ForgotPassword = "admin/forgot_password";

        public const string ResetPassword = "admin/reset_password";

        public const string ChangePassword = "admin/change_password";

        public const string Logout = "admin/logout";

        public const string IsAdminValid = "admin/IsAdminValid";

        public const string GetAdminProfile = "admin/get_profile";

        public const string UpdateProfile = "admin/update_profile";

        public const string DefaultPermissionList = "admin/default_permission_list";


        // SubAdmin

        public const string SubAdminAdd = "sub_admin/insert";

        public const string SubAdminUpdate = "sub_admin/update";

        public const string SubAdminDelete = "sub_admin/delete";

        public const string SubAdminList = "sub_admin/list";

        public const string GetSubAdminById = "sub_admin/get_by_id";

        public const string SubAdminStatusUpdate = "sub_admin/status_update";

        public const string SubAdminRestore = "sub_admin/delete_restore";
    }

    public class AdminPanel
    {
        public const string AdminPanelDashboard = "home/dashboard";
        //public const string AdminProfile = "home/profile";
    }

    public class Dropdown
    {
        public const string ProductCategoryDropdown = "dropdown/product_category";

        public const string StoreDropdown = "dropdown/store";

        public const string ProductSubCategoryDropdown = "dropdown/product_sub_category";

        public const string OfferLabelDropdown = "dropdown/offer_label";

        public const string RewardsDropdown = "dropdown/rewards";

        public const string Team = "dropdown/team_menu";
    }

    public class Event
    {
        public const string InsertEvent = "event/insert";

        public const string UpdateEvent = "event/update";

        public const string GetEvents = "event/get_list";

        public const string GetEventById = "event/get_by_id";

        public const string DeleteEvent = "event/delete";

        public const string DeleteStatusUpdate = "event/status_update";

        public const string EventList = "event/event_list";

        public const string InsertUserEvents = "event/user_events_insert";

        public const string EventsDetailsGetByUserId = "event_details/get_by_user_id";
    }

    public class Contest
    {
        public const string InsertContest = "contest/insert";

        public const string UpdateContest = "contest/update";

        public const string ContestGetById = "contest/get_by_id";

        public const string GetContestList = "contest/get_list";

        public const string DeleteContest = "contest/delete";

        public const string ContestStatusUpdate = "contest/status_update";

        public const string ContestList = "contest/contest_list";

        public const string InsertUserContest = "contest/user_contest_insert";

        public const string ContestDetailsGetByUserId = "contest_details/get_by_user_id";

    }

    public class Referral
    {
        public const string InsertReferral = "referral/insert";

        public const string UpdateReferral = "referral/update";

        public const string ReferralGetById = "referral/get_by_id";

        public const string ReferralList = "referral/get_list";

        public const string ReferralByMail = "referral/email";

    }

    public class FaceBook
    {
        public const string FacebookLogin = "facebook/facebook_login";
    }

    public class FAQs
    {
        public const string InsertFAQs = "faq/insert";

        public const string UpdateFAQs = "faq/update";

        public const string FAQsGetById = "faq/get_by_id";

        public const string GetFAQsList = "faq/get_list";

        public const string DeleteFAQs = "faq/delete";

        public const string FAQsList = "faq/faq_list";
    }

    public class Chat
    {
        public const string InsertChat = "chat/insert";

        public const string ChatGetById = "chat/get_by_id";

        public const string ChatList = "chat/get_chat_list";

        public const string AdminMessageCount = "chat/admin_message_count";
    }

    public class Points
    {
        public const string InsertPoints = "point/insert";

        public const string UpdatePoints = "point/update";

        public const string PointsGetById = "point/get_by_id";

        public const string PointsList = "point/get_list";

        public const string DeletePoints = "point/delete";

        public const string PointList = "point/point_list";

        public const string PointAssignedByAdmin = "point/assigned_by_admin";

        public const string PointsForRewardsAndCoupon = "point/for_rewards_coupon";

    }

    public class PointManagement
    {
        public const string PointInsert = "point_management/insert";

        public const string PointUpdate = "point_management/update";

        public const string PointGetById = "point_management/get_by_id";

        public const string PointList = "point_management/get_list";

        public const string PointDelete = "point_management/delete";

        public const string PointLists = "point_management/point_list";

    }

    public class Coupons
    {
        public const string InsertCoupons = "coupons/insert";

        public const string InsertCSVCoupons = "coupons/insert_csv";

        public const string UpdateCoupons = "coupons/update";

        public const string CouponsGetById = "coupons/get_by_id";

        public const string CouponsAddIntoMyRewardsById = "coupons_into_my_rewards/get_by_id";

        public const string GetCouponsList = "coupons/get_list";

        public const string DeleteCoupons = "coupons/delete";

        public const string CouponsStatusUpdate = "coupons/status_update";

        public const string CouponsCSVImport = "coupons/import";

        public const string CouponsList = "coupons/coupons_list";
    }

    public class Offers
    {
        public const string InsertOffer = "offer/insert";

        public const string UpdateOffer = "offer/update";

        public const string OfferGetById = "offer/get_by_id";

        public const string GetOfferList = "offer/get_list";

        public const string DeleteOffer = "offer/delete";

        public const string OfferStatusUpdate = "offer/status_update";
    }

    public class Rewards
    {
        public const string InsertRewards = "rewards/insert";

        public const string UpdateRewards = "rewards/update";

        public const string RewardsGetById = "rewards/get_by_id";

        public const string RewardsAddIntoMyRewardsById = "rewards_into_my_rewards/get_by_id";

        public const string GetRewardsList = "rewards/get_list";

        public const string DeleteRewards = "rewards/delete";

        public const string RewardsStatusUpdate = "rewards/status_update";

        public const string InsertCSVRewards = "rewards/insert_csv";

        public const string RewardsCSVImport = "rewards/import";

        public const string RewardsList = "rewards/rewards_list";
    }


    public class MyRewards
    {
        public const string InsertMyRewards = "my_rewards/insert";

        public const string GetMyRewardsList = "my_rewards/get_list";

        public const string MyRewardGetById = "my_rewards/get_by_id";
    }

    public class Dashboard
    {
        public const string DashboardCount = "dashboard/retrive";
    }

    public class PunchCard
    {
        public const string InsertPunchCard = "punchcard/insert";

        public const string UpdatePunchCard = "punchcard/update";

        public const string PunchCardGetById = "punchcard/get_by_id";

        public const string GetPunchCardList = "punchcard/get_list";

        public const string DeletePunchCard = "punchcard/delete";

        public const string PunchCardStatusUpdate = "punchcard/status_update";

        public const string PunchCardAddIntoMyRewardsById = "punchcard_into_my_rewards/get_by_id";

        public const string InsertCSVPunchCard = "punchcard/insert_csv";

        public const string PunchCardCSVImport = "punchcard/import";

        public const string PunchCardList = "punchcard/punchcard_list";
    }
    public class FileConst
    {
        public const string UploadFile = "common/uploadfile";
    }

    public class Team
    {
        public const string TeamInsert = "team/insert";

        public const string TeamUpdate = "team/update";

        public const string TeamGetById = "team/get_by_id";

        public const string TeamGetList = "team/get_list";

        public const string TeamDelete = "team/delete";

        public const string TeamStatusUpdate = "team/status_update";
    }

    public class Banner
    {
        public const string BannerInsertUpdate = "banner/insert_update";

        public const string BannerGetById = "banner/get_by_id";

        //public const string BannerImageGetById = "banner_image/get_by_id";

        public const string BannerGetList = "banner/get_list";

        public const string BannerDelete = "banner/delete";

        public const string BannerStatusUpdate = "banner/status_update";

        public const string BannerImageUpload = "banner/upload";

        public const string BannerUpdateImage = "banner/update_image";

        public const string BannerInsertImage2 = "banner/insert_image_2";

        public const string BannerInsertImage3 = "banner/insert_image_3";


    }
    public class POSCustomerData
    {
        public const string POSCustomerList = "customer/select";

        public const string POSCompanyList = "company/select";

        public const string POSInventoryList = "Inventory/select";

        public const string POSCustomerRewardList = "pos_customer_rewards/select";

        public const string POSCustomernewList = "pos_customer_new/select";
        public const string POSInventoryListNew = "pos_inventory_new/select";

        public const string CustomerInsert = "customer/insert";

        public const string POSCustomerAddUpdate = "pos_customer_cronjob/add_update";



    }
    public class TaskScheduler
    {
        public const string GetCustomerRewardPos = "customer_rewards_pos/get";

        public const string GetCustomerPos = "customer_pos/get";

        public const string GetInventoryPos = "inventory_pos/get";

        public const string GetDepartmentPos = "department_pos/get";

        public const string GetSalesReceiptPos = "salesreceipt_pos/get";

    }

    public class POSInventory
    {
        public const string GetByIdInventoryPos = "pos_inventory/get_by_id";

        public const string UpdateInventoryPos = "pos_inventory/update";

        public const string GetListInventoryPos = "pos_inventory/get_list";

        public const string InventoryPosList = "pos_inventory/list";

        public const string GetInventoryPosList = "get_pos_inventory/list";

        public const string DistinctSubCategoryList = "pos_inventory/distinct_subcategory";

        public const string POSInventoryAddUpdate = "pos_inventory_cronjob/add_update";

        public const string POSInventoryStatusUpdate = "pos_inventory/status_update";
    }



    public class POSDepartment
    {
        public const string GetByIdDepartmentPos = "pos_department/get_by_id";

        public const string GetListDepartmentPos = "pos_department/get_list";

        public const string UpdateDepartmentPos = "pos_department/update";

        public const string DepartmentPosList = "pos_department/list";

        public const string POSDepartmentAddUpdate = "pos_depaertment_cronjob/add_update";

        public const string POSDepartmentStatusUpdate = "pos_department/status_update";

    }

    public class Notification
    {
        public const string GetCustomerRewardPos = "customer_reward_pos/get";
        
        public const string GetNotificationByUserId = "notification/get_by_userid";
    }

    public class POSCustomer
    {
        public const string GetByIdPOSCustomer = "pos_customer/get_by_id";

        public const string GetListCustomerPos = "pos_customer/get_list";
    }

    public class POSStore
    {
        public const string GetByIdStorePos = "pos_store/get_by_id";

        public const string GetListStorePos = "pos_store/get_list";

        public const string UpdateStorePos = "pos_store/update";

        public const string StorePosList = "pos_store/list";
    }

    public class POSSalesReceipt
    {
        public const string POSSalesReceiptAddUpdate = "pos_sales_receipt_cronjob/add_update";

        public const string POSSalesReceiptCustomerRewards = "pos_sales_receipt/customer_rewards";
    }
    public class POSSubCategory
    {
        public const string POSSubCategoryInsertAndUpdate = "pos_sub_category/insert_update";
        
        public const string POSSubCategoryList = "pos_sub_category/list";

        public const string POSSubCategoryGetList = "pos_sub_category/get_list";

        public const string POSSubCategoryGetById = "pos_sub_category/get_by_id";
        
        public const string POSSubCategoryUpdate = "pos_sub_category/update";

        public const string POSSubCategoryStatusUpdate = "pos_sub_category/status_update";

    }

    public class AboutUs
    {
        public const string AboutUsInsert = "aboutus/insert";

        public const string AboutUsUpdate = "aboutus/update";

        public const string AboutUsList = "aboutus/list";

        public const string AboutUsDelete = "aboutus/delete";
        
        public const string AboutUsGetById = "aboutus/get_by_id";

        

    }

    public class PrivacyPolicy
    {
        public const string PrivacyPolicyInsert = "privacy_policy/insert";

        public const string PrivacyPolicyUpdate = "privacy_policy/update";

        public const string PrivacyPolicyList = "privacy_policy/list";

        public const string PrivacyPolicyDelete = "privacy_policy/delete";
    }
}


