namespace MyBooks
{
    public static class ProgramSettings
    {
		// Internal encription/decryption uses RijndaelManaged
		// with Rfc2898DeriveBytes
        public const string ENC_HASH = "ENCRYPTION_HASH";
        public const string ENC_SALT = "ENCRYPTION_SALT_KEY";
        public const string ENC_VIKEY = "ENCRYPTION_VI_KEY";

        // MySQL Database credentials
        public const string DB_HOST = "##";
        public const string DB_BASE = "##";
        public const string DB_USER = "ENCRYPTED_DB_USER_NAME";
        public const string DB_PASS = "ENCRYPTED_DB_USER_PASSWORD";
		
		// Use MyBooks.exe ARG_AUTOLOGIN
		// to autologin user_id=1
		// sha1(ARG_AUTOLOGIN) here
        public const string CMD_KEY = "SHA1_HASH_ARG_AUTOLOGIN_USER_ID=1";

        // Hashing users passwords delimiter
        public const string AUTH_KEY = "USER_PASSWORD_DELIMITER";

        // Bug report parameters
        public const string BUG_FROM_MAIL = "SOFTWARE_EMAIL";
        public const string BUG_FROM_NAME = "SOFTWARE_NAME";
        public const string BUG_RCPT_MAIL = "DEVELOPER_EMAIL";
        public const string BUG_RCPT_NAME = "DEVELOPER_NAME";
    }
}