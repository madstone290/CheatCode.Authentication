using Microsoft.AspNetCore.Identity;

namespace IdentityServer4B.Server.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
            DisplayUserName = userName;
        }

        /// <summary>
        /// 사용자 이름
        /// </summary>
        public string DisplayUserName { get; set; }

        /// <summary>
        /// 사용자 등급
        /// </summary>
        public string UserGrade { get; set; }

        /// <summary>
        /// 소프트 삭제여부
        /// </summary>
        public bool Deleted { get; set; }
    }
}
