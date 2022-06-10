using Microsoft.AspNetCore.Authorization;

namespace CheatCode.Authentication.IdServer4.MvcApp.AuthorizationRequirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; }

        public AgeRequirement(int age)
        {
            Age = age;
        }
    }

    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            AgeRequirement requirement)
        {
            var ageCliam = context.User.Claims.FirstOrDefault(x => x.Type == "Age");
            if(ageCliam == null)
                return Task.CompletedTask;

            int.TryParse(ageCliam.Value, out int age);
            if (requirement.Age <= age)
            {
                //# 현재 요구사항 검증에 성공
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
