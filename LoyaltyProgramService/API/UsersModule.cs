using LoyaltyProgramService.Model;
using Nancy;
using Nancy.ModelBinding;

namespace LoyaltyProgramService.API
{
    public class UsersModule: NancyModule
    {
        public UsersModule(IUsersStore usersStore): base("/users")
        {
            Post("/", _ =>
            {
                var newUser = this.Bind<LoyaltyProgramUser>();
                usersStore.Save(newUser);
                return this.CreatedResponse(newUser);
            });

            Put("/{userId:int}", parameters =>
            {
                int userId = parameters.userId;
                bool isNewUser = usersStore.Get(userId) == null;

                var updatedUser = this.Bind<LoyaltyProgramUser>();
                usersStore.Save(updatedUser);

                return this.CreatedResponse(updatedUser, isNewUser);
            });
        }

        private dynamic CreatedResponse(LoyaltyProgramUser user, bool isNewUser = true)
        {
            return this
                .Negotiate
                .WithStatusCode(isNewUser? HttpStatusCode.Created : HttpStatusCode.NoContent)
                .WithHeader("Content-Location", $"{this.Request.Url.SiteBase}/users/{user.Id}")
                .WithModel(user);
        }
    }
}
