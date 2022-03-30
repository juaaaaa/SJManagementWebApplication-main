namespace SJManagementWebApplication.Code
{
    public static class NumDefine
    {
        //sql
        public const byte UPDATE_QRY = 0;
        public const byte INSERT_QRY = 1;
        public const byte DELETE_QRY = 2;

        //중복체크
        public const byte NOT_EXIST = 0;
        public const byte EXIST_ID_AND_NAME = 1;
        public const byte EXIST_ID = 2;
        public const byte EXIST_NAME = 3;

        //patent Tag
        public const byte CLEAR = 0;
        public const byte OVERLAY = 1;
        public const byte DELETE = 2;
        public const byte FILE_DELETE = 3;

        //join Tag
        public const byte FAIL = 0;
        public const byte SUCCESS = 1;

        //searchPopup Tag
        public const byte TEXT = 0;
        public const byte CAPTION = 1;

        public const string TEXTBOX = "0";
        public const string COMBOBOX = "1";
        public const string DATEEDIT = "2";

        //patentInput 이미지 경로
        public const string ADD_IMAGE = "~/Content/icon/button_add.svg";
        public const string CANCEL_IMAGE = "~/Content/icon/button_cancel.svg";

        //root master 이미지 경로
        public const string HAMBURGER_CLEAR = "~/Content/icon/button_hambuger_clear.svg";
        public const string HAMBURGER_CLICK = "~/Content/icon/button_hambuger_deepblue.svg";

        //색상표
        public const string DEEPBLUE = "#005B99";
        public const string MAINMENU_BLUE = "#0071BC";

        //focus value
        public const int NONE_FOCUSE = -1;
    }
}