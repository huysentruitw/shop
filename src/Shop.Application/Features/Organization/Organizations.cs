namespace Shop.Application.Features.Organization;

[ExtendObjectType<Query>]
public sealed class OrganizationsQuery
{
    [Authorize]
    [UsePaging]
    [UseProjection]
    public IExecutable<OutputTypes.Organization> Organizations(DataContext dataContext, ClaimsPrincipal user)
        => dataContext.Set<Entities.OrganizationUser>()
            .Where(x => x.UserId == user.GetUserId())
            .OrderBy(x => x.Organization.Name)
            .Select(x => new OutputTypes.Organization
            {
                Id = x.Organization.Id,
                Name = x.Organization.Name,
                IsAdmin = x.IsAdmin,
            })
            .AsExecutable();
}
