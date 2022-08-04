namespace Common.Extensions
{
    public static class Permission
    {
        public static class Users
        {
            public const string View = "Permission.USERS-USERS.View";
            public const string Create = "Permission.USERS-USERS.Create";
            public const string Edit = "Permission.USERS-USERS.Edit";
            public const string Delete = "Permission.USERS-USERS.Delete";
        }

        public static class Roles
        {
            public const string View = "Permission.USERS-ROLES.View";
            public const string Create = "Permission.USERS-ROLES.Create";
            public const string Edit = "Permission.USERS-ROLES.Edit";
            public const string Delete = "Permission.USERS-ROLES.Delete";
        }
        public static class Apps
        {
            public const string View = "Permission.USERS-APPLICATIONS.View";
            public const string Create = "Permission.USERS-APPLICATIONS.Create";
            public const string Edit = "Permission.USERS-APPLICATIONS.Edit";
            public const string Delete = "Permission.USERS-APPLICATIONS.Delete";
        }
        public static class Pages
        {
            public const string View = "Permission.USERS-PAGES.View";
            public const string Create = "Permission.USERS-PAGES.Create";
            public const string Edit = "Permission.USERS-PAGES.Edit";
            public const string Delete = "Permission.USERS-PAGES.Delete";
        }
        public static class Permissions
        {
            public const string View = "Permission.USERS-PERMISSIONS.View";
            public const string Create = "Permission.USERS-PERMISSIONS.Create";
            public const string Edit = "Permission.USERS-PERMISSIONS.Edit";
            public const string Delete = "Permission.USERS-PERMISSIONS.Delete";
        }
        public class CustomClaimTypes
        {
            public const string Permission = "permission";
        }
    }
}
