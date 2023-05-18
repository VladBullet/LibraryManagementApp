namespace LibraryManagementApi
{
    public static class AuthorizationPolicies
    {
        public const string User = "User";
        public const string Admin = "Admin";
        public const string Author = "Author";
    }
}


// TODO: add RentService logic : rent and return
// TODO: add background worker that checks book rentals and updates user BookRentFlag if user is past overdue with a return
// TODO: implement error logging and handling
