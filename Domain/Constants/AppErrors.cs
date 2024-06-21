﻿namespace Domain.Constants
{
    public class AppErrors
    {
        public const string INVALID_CERTIFICATE = "Tài khoản hoặc mật khẩu không đúng";
        public const string INVALID_USER_UNACTIVE = "User không còn hoạt động";

        // User
        public const string USER_NOT_EXIST = "User không tồn tại";
        public const string USERNAME_EXIST = "Username đã tồn tại";

        // Cart
        public const string CART_NOT_EXIST = "Giỏ hàng không tồn tại";

        // Cart Item
        public const string CART_ITEM_NOT_EXIST = "Sản phẩm không tồn tại trong giỏ hàng";

        // Product
        public const string INVALID_QUANTITY = "Số lượng phải lớn hơn 0";
        public const string PRODUCT_QUANTITY_NOT_ENOUGH = "Số lượng trong giỏ hàng không được vượt quá số lượng còn lại của sản phẩm.";

        // Query
        public const string CREATE_FAIL = "Tạo mới thất bại";
        public const string UPDATE_FAIL = "Cập nhật thất bại";
        public const string RECORD_NOT_FOUND = "Đối tượng không tồn tại";

        // Order
        public const string INVALID_PAYMENT_METHOD = "Phương thức thanh toán không tồn tại hoặc chưa hổ trợ";

    }
}
