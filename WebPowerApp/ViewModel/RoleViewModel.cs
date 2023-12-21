using WebPowerApp.Models;

namespace WebPowerApp.ViewModel
{
    public class RoleViewModel
    {
        public int TotalRow { get; set; }
        public string ErrorMsg { get; set; } = string.Empty;

        public RoleModel Role { get; set; } = new RoleModel();
        private List<RoleModel> roles = new List<RoleModel>();
        public List<RoleModel> RoleList
        {
            get { return roles; }
            set { roles = value; }
        }
    }
}
