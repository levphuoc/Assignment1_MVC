namespace Assignment1_MVC.Extensions
{
    public static class SessionExtensions
    {
        public static bool IsAdmin(this ISession session)
        {
            return session.GetInt32("AccountRole") == 0;
        }

        public static bool IsStaff(this ISession session)
        {
            return session.GetInt32("AccountRole") == 1;
        }

        public static bool IsLecturer(this ISession session)
        {
            return session.GetInt32("AccountRole") == 2;
        }

        public static bool IsStaffOrLecturerOrAdmin(this ISession session)
        {
            var role = session.GetInt32("AccountRole");
            return role == 0 || role == 1 || role == 2;
        }
    }
}
