namespace azara.client.Models.Const;

public class ApiEndPointConsts
{
    const string Version = "v1/";

#if DEBUG
    public const string BaseRoute = "https://localhost:7234/api/" + Version;

    public const string BaseRouteWithoutVersion = "https://localhost:7234/";
#elif RELEASE
    public const string BaseRoute = "https://apiazara.azurewebsites.net/api/" + Version;
    public const string BaseRouteWithoutVersion = "https://apiazara.azurewebsites.net/";
#elif UAT
    //public const string BaseRoute = "http://20.69.56.199:8085/api/" + Version;
    //public const string BaseRouteWithoutVersion = "http://20.69.56.199:8085/";

    public const string BaseRoute = "https://api.azara-app.com/api/" + Version;
    public const string BaseRouteWithoutVersion = "https://api.azara-app.com/";

#endif

    public class Account
    {
        public const string UserLogin = BaseRoute + "user/sign_in";

        public const string UserSignUp = BaseRoute + "user/sign_up";

        public const string ChangePassword = BaseRoute + "user/change_password";

        public const string ForgotPassword = BaseRoute + "user/forgot_password";

        public const string ContactUs = BaseRoute + "contact_us/add";

        public const string Logout = BaseRoute + "user/logout";

        public const string GetProfile = BaseRoute + "user/profile";

        public const string GetReferalData = BaseRoute + "referral/get_by_id";

        public const string ResetPassword = BaseRoute + "user/reset_password";

        public const string ProfileUpdate = BaseRoute + "user/update";

        public const string UesrProductRequest = BaseRoute + "user/product_request";

        public const string IsUserValid = BaseRoute + "user/IsUserValid";

        public const string ImageUpload = BaseRoute + "user/profile_picture/update";

        public const string PrivacyPolicyList = BaseRoute + "privacy_policy/list";
    }

    public class Product
    {
        public const string ShopProductList = BaseRoute + "product/product_list";

        public const string ProductCategoryList = BaseRoute + "product_category/product_category_list";

        public const string ProductSubCategoryList = BaseRoute + "product_sub_category/get_by_productSubCategoryid";

        public const string ProductListBySubCategoryId = BaseRoute + "product_sub_category/get_product_list";

        public const string ProductGetById = BaseRoute + "product/get_by_id";

        public const string PosInventoryListNew = BaseRoute + "pos_inventory/get_list";

    }

    public class PosCategory
    {
        public const string GetByIdDepartmentPos = BaseRoute + "pos_department/get_by_id";
        public const string ProductSubcategory = BaseRoute + "pos_sub_category/list";

        public const string GetListDepartmentPos = BaseRoute + "pos_department/get_list";

        public const string UpdateDepartmentPos = BaseRoute + "pos_department/update";
    }

    public class Store
    {
        public const string StoreList_Get = BaseRoute + "store/store_list";

        public const string StoreList = BaseRoute + "store/store/get_list";

        public const string StoreGetById = BaseRoute + "store/get_by_id";
    }

    public class Blog
    {
        public const string BlogList = BaseRoute + "blog/blog_list";

        public const string BlogGetById = BaseRoute + "blog/get_by_id";
    }

    public class Coupons
    {
        public const string CouponsList = BaseRoute + "coupons/coupons_list";

        public const string CouponsGetById = BaseRoute + "coupons/get_by_id";
    }

    public class Banner
    {
        public const string BannerImageList = BaseRoute + "banner/get_list";

    }

    public class Contest
    {
        public const string ContestList = BaseRoute + "contest/contest_list";

        public const string ContestGetById = BaseRoute + "contest/get_by_id";

        public const string InsertUserContest = BaseRoute + "contest/user_contest_insert";

        public const string ContestGetForUser = BaseRoute + "contest_details/get_by_user_id";

    }

    public class Event
    {
        public const string EventList = BaseRoute + "event/event_list";

        public const string EventGetById = BaseRoute + "event/get_by_id";

        public const string EventAddForUSer = BaseRoute + "event/user_events_insert";

        public const string EventsGetForUsre = BaseRoute + "event_details/get_by_user_id";
    }

    public class Deals
    {
        public const string DealsList = BaseRoute + "deals/deals_list";

        public const string DealsGetById = BaseRoute + "deals/get_by_id";
    }

    public class FaceBook
    {
        public const string FacebookLogin = BaseRoute + "facebook/facebook_login";
    }

    public class FAQs
    {
        public const string FAQsList = BaseRoute + "faq/faq_list";
    }

    public class Points
    {
        public const string PointsGetById = BaseRoute + "point/get_by_id";
    }
    public class PosInventory
    {
        public const string PosInventoryList = BaseRoute + "customer/select";
        public const string PosInventoryListNew = BaseRoute + "pos_inventory/list";
        public const string GetInventoryPosList = BaseRoute + "get_pos_inventory/list";

        public const string PosINventoryGetByID = BaseRoute + "pos_inventory/get_by_id";

    }

    public class Chat
    {
        public const string ChatGetById = BaseRoute + "chat/get_by_id";

        public const string ChatInsert = BaseRoute + "chat/insert";

        public const string AdminMessageCount = BaseRoute + "chat/admin_message_count";
    }

    public class Reward
    {
        public const string RewardGetById = BaseRoute + "rewards/rewards_list";

        public const string RewardGetList = BaseRoute + "rewards/rewards_list";

        public const string POSRewardGetList = BaseRoute + "pos_customer_rewards/select";



    }
    public class Punchcard
    {
        public const string PunchcardGetById = BaseRoute + "punchcard/punchcard_list";

        public const string PunchcardGetList = BaseRoute + "punchcard/punchcard_list";
    }

    public class MyReward
    {
        public const string AddMyReward = BaseRoute + "my_rewards/insert";

        public const string MyRewardList = BaseRoute + "rewards_into_my_rewards/get_by_id";

        public const string MyRewarddetails = BaseRoute + "rewards/get_by_id";

        public const string UseMyReward = BaseRoute + "point/for_rewards_coupon";

        public const string MyRewardGetById = BaseRoute + "my_rewards/get_by_id";


        //public const string MyCouponsdetails = BaseRoute + "coupons_into_my_rewards/get_by_id";
    }

    public class POSCustomerData
    {
        public const string POSCustomerInsert = "customer/insert";

        public const string POSCustomerList = BaseRoute + "customer/select";
        public const string POSCompanyList = BaseRoute + "company/select";
        public const string POSInventoryList = "Inventory/select";

    }

    public class POSDepartment
    {
        public const string POSDepartmentList = BaseRoute + "pos_department/list";
    }

    public class FileConst
    {
        public const string UploadFile = BaseRoute + "common/uploadfile";
    }

    public class Notification
    {
        public const string GetNotificationByUserId = BaseRoute + "notification/get_by_userid";

    }

    public class Aboutus
    {
        public const string AboutUsList = BaseRoute + "aboutus/list";

    }
}