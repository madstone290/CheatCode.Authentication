using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4A.MvcApp.Data
{
    //# IdentityDbContext는 User에 관련된 기본 테이블을 포함하고 있다.
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        } 
    }
}
