using LguardaApp.RBAC.Models;
using LguardaApp.RBAC.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LguardaApp.RBAC.Action_Filters
{
    public class RBACUser
    {
        public string User_Id { get; set; }
        public string App_Id { get; set; }
        public bool IsSysAdmin { get; set; }
        public string Username { get; set; }
        private List<UserRole> Roles = new List<UserRole>();

        public RBACUser(string _userid, string _appid)
        {
            this.User_Id = _userid;
            this.App_Id = _appid;
            //this.Username = _username;
            this.IsSysAdmin = false;
            GetDatabaseUserRolesPermissions();
        }

        private void GetDatabaseUserRolesPermissions()
        {
            if (string.IsNullOrWhiteSpace(this.User_Id))
            {
                return;
            }
            string result = string.Empty;
            string url1 = ConfigurationManager.AppSettings["lgarda_server"] + "/GetBindUserByDomainId/" + this.User_Id + "?format=json";
            result = HttpWcfRequest.GetString1(url1);

            if (!string.IsNullOrWhiteSpace(result))
            {
                this.User_Id = result;
            }
            if (string.IsNullOrWhiteSpace(this.App_Id))
            {
                this.App_Id = "00";
            }

            if (HttpContext.Current.Session["Modules"] == null || HttpContext.Current.Session["PermissionDetails"] == null || HttpContext.Current.Session["Menus"] == null)
            {
                string pFUNCTION_GROUP_ID = "0";
                //string url = ConfigurationManager.AppSettings["lgarda_server"] + "/GetPermittedFunctionsByUser/" + "ilife21" + "/" + "06" + "/" + pFUNCTION_GROUP_ID + "?format=json";
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/GetPermittedFunctionsByUser/" + this.User_Id + "/" + this.App_Id + "/" + pFUNCTION_GROUP_ID + "?format=json";
                LG_USER_SETUP_PROFILE_MAP _user = HttpWcfRequest.GetObject1<LG_USER_SETUP_PROFILE_MAP>(url);

                if (_user.FIRST_LOGIN_FLAG == 0)
                {
                    UserRole _userRole = new UserRole();
                    if (_user != null)
                    {
                        //this.User_Id = _user.User_Id;
                        foreach (LG_USER_ROLE_ASSIGN_MAP _role in _user.ROLES)
                        {
                            _userRole = new UserRole { Role_Id = _role.ROLE_ID, RoleName = _role.ROLE_NAME };
                            foreach (LG_FNR_ROLE_PERMISSION_DETAILS_MAP _permission in _user.PERMISSIONS)
                            {
                                _userRole.Permissions.Add(new RolePermission { Permission_Id = _permission.PERMISSION_ID, PermissionDescription = _permission.PERMISSION_DETAILS });
                            }
                            this.Roles.Add(_userRole);

                            if (!this.IsSysAdmin)
                                this.IsSysAdmin = true;
                        }
                    }
                    HttpContext.Current.Session["Modules"] = _user.LIST_MODULES_FOR_SELECTED_ROLE;
                    HttpContext.Current.Session["PermissionDetails"] = _userRole.Permissions;
                    HttpContext.Current.Session["Menus"] = _user.LIST_MENU_MAP;
                }
            }
            else
            {
                UserRole _userRole = new UserRole();
                _userRole.Permissions = (List<RolePermission>)HttpContext.Current.Session["PermissionDetails"];
                this.Roles.Add(_userRole);
                if (!this.IsSysAdmin)
                    this.IsSysAdmin = true;
            }
        }

        public bool HasPermission(string requiredPermission)
        {
            bool bFound = false;
            foreach (UserRole role in this.Roles)
            {
                bFound = (role.Permissions.Where(p => p.PermissionDescription.ToLower() == requiredPermission.ToLower()).ToList().Count > 0);
                if (bFound)
                    break;
            }
            return bFound;
        }

        public static List<LG_MENU_MAP> GetAppMenus()
        {
            var result = (List<LG_MENU_MAP>)HttpContext.Current.Session["Menus"];
            return result;
        }

        public bool HasRole(string role)
        {
            return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
        }

        public bool HasRoles(string roles)
        {
            bool bFound = false;
            string[] _roles = roles.ToLower().Split(';');
            foreach (UserRole role in this.Roles)
            {
                try
                {
                    bFound = _roles.Contains(role.RoleName.ToLower());
                    if (bFound)
                        return bFound;
                }
                catch (Exception)
                {
                }
            }
            return bFound;
        }

        public static bool HasMenuPermission(string menu_name) //if menu name pass from layout
        {
            bool bFound = false;
            HttpContext context = HttpContext.Current;
            List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE = (List<LG_FNR_MODULE_MAP>)context.Session["Modules"];
            var match = LIST_MODULES_FOR_SELECTED_ROLE.FirstOrDefault(x => x.MODULE_NM.Contains(menu_name));
            if (match != null)
            {
                bFound = true;
            }

            return bFound;
        }

        public static List<MenuPermission> HasMenuPermission()
        {
            List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE = new List<LG_FNR_MODULE_MAP>();
            List<MenuPermission> List_MenuName = new List<MenuPermission>();
            MenuPermission Obj_MenuPermission = new MenuPermission();

            HttpContext context = HttpContext.Current;
            if ((List<LG_FNR_MODULE_MAP>)context.Session["Modules"] != null)
            {
                LIST_MODULES_FOR_SELECTED_ROLE = (List<LG_FNR_MODULE_MAP>)context.Session["Modules"];
            }

            if (LIST_MODULES_FOR_SELECTED_ROLE.Count() > 0)
            {
                foreach (LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP in LIST_MODULES_FOR_SELECTED_ROLE)
                {
                    Obj_MenuPermission = new MenuPermission();
                    Obj_MenuPermission.MenuName = OBJ_LG_FNR_MODULE_MAP.MODULE_NM;
                    List_MenuName.Add(Obj_MenuPermission);
                }
            }
            return List_MenuName;
        }

        public static List<SubMenuPermission> HasSubMenuPermission()
        {
            List<RolePermission> List_RolePermission = new List<RolePermission>();
            List<SubMenuPermission> List_SubMenuName = new List<SubMenuPermission>();
            SubMenuPermission Obj_SubMenuName = new SubMenuPermission();

            HttpContext context = HttpContext.Current;
            if (context.Session["PermissionDetails"] != null)
            {
                List_RolePermission = (List<RolePermission>)context.Session["PermissionDetails"];
            }

            if (List_RolePermission.Count() > 0)
            {
                for (int i = 0; i < List_RolePermission.Count(); i++)
                {
                    Obj_SubMenuName = new SubMenuPermission();
                    Obj_SubMenuName.SubMenuName = List_RolePermission[i].PermissionDescription.Split('-')[0];
                    var match = List_SubMenuName.FirstOrDefault(x => x.SubMenuName.Contains(Obj_SubMenuName.SubMenuName));
                    if (match == null)
                    {
                        List_SubMenuName.Add(Obj_SubMenuName);
                    }
                }
            }
            return List_SubMenuName;
        }

        public bool SysAdmin()
        {
            if (this.User_Id == "Admin1" || this.User_Id == "Admin2" || this.User_Id == "Admin3" || this.User_Id == "Admin4")
            {
                return true;
            }
            else
                return false;
        }

        /*
        public static List<LG_FNR_MODULE_MAP> HasMenuPermission()
        {
            HttpContext context = HttpContext.Current;
            List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE = (List<LG_FNR_MODULE_MAP>)context.Session["Modules"];
            return LIST_MODULES_FOR_SELECTED_ROLE;
        } */
    }

    public class UserRole
    {
        public string Role_Id { get; set; }
        public string RoleName { get; set; }
        public List<RolePermission> Permissions = new List<RolePermission>();
    }

    public class RolePermission
    {
        public string Permission_Id { get; set; }
        public string PermissionDescription { get; set; }
    }

    public class MenuPermission
    {
        public string MenuName { get; set; }
    }

    public class SubMenuPermission
    {
        public string SubMenuName { get; set; }
    }
}