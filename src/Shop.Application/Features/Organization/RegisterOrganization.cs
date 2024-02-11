namespace Shop.Application.Features.Organization;

[ExtendObjectType<Mutation>]
public sealed class RegisterOrganizationMutation
{
    [Authorize]
    public async Task<MutationResult> RegisterOrganization(
        [Service] IPasswordHasher passwordHasher,
        DataContext dataContext,
        RegisterOrganizationInput input)
    {
        await using var transaction = await dataContext.Database.BeginTransactionAsync();

        // TODO When the user is logged in, input.InitialAccount should be null and we use the userId of the logged in user
        Guid userId = Guid.Empty;
        if (input.InitialAccount is not null)
            userId = await CreateUser();

        var newOrganization = await CreateOrganization();
        await LinkAdminToOrganization(newOrganization, userId);

        await transaction.CommitAsync();

        return MutationResult.ForSubject(newOrganization.Id);

        async Task<Guid> CreateUser()
        {
            // TODO There's a unique index on email, if the email is already taken, we'll have to ask the user to login instead
            var user = new Entities.User
            {
                Email = input.InitialAccount.Email,
                EmailConfirmed = true, // TODO Set to false & send confirmation email instead
                PasswordHash = passwordHasher.HashPassword(input.InitialAccount.Password),
                FirstName = input.InitialAccount.FirstName,
                LastName = input.InitialAccount.LastName,
                AcceptsTermsAndConditions = input.InitialAccount.AcceptsTermsAndConditions,
            };

            await dataContext.AddAsync(user);
            await dataContext.SaveChangesAsync();
            return user.Id;
        }

        async Task<Entities.Organization> CreateOrganization()
        {
            var organization = new Entities.Organization
            {
                Name = input.Name,
                StreetAddress = input.StreetAddress,
                City = input.City,
                PostalCode = input.PostalCode,
                CountryCode = input.CountryCode,
                Url = input.Url,
                CompanyInformation = new Entities.CompanyInformation(),
                OrganizationUsers = new List<Entities.OrganizationUser>(),
            };

            await dataContext.AddAsync(organization);
            await dataContext.SaveChangesAsync();
            return organization;
        }

        async Task LinkAdminToOrganization(Entities.Organization organization, Guid adminUserId)
        {
            var organizationUser = new Entities.OrganizationUser
            {
                OrganizationId = organization.Id,
                UserId = adminUserId,
                IsAdmin = true,
            };

            await dataContext.AddAsync(organizationUser);
            await dataContext.SaveChangesAsync();
        }
    }
}

public sealed record RegisterOrganizationInput
{
    public required string Name { get; init; }

    public required string StreetAddress { get; init; }

    public required string City { get; init; }

    public required string PostalCode { get; init; }

    public required string CountryCode { get; init; }

    public required Uri? Url { get; init; }

    public required OrganizationInitialAccountInput? InitialAccount { get; init; }
}

public sealed record OrganizationInitialAccountInput
{
    public required string Email { get; init; }

    public required string Password { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required bool AcceptsTermsAndConditions { get; init; }
}

internal sealed class RegisterOrganizationInputValidator : AbstractValidator<RegisterOrganizationInput>
{
    public RegisterOrganizationInputValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Entities.Organization.MaxNameLength);
        RuleFor(x => x.StreetAddress).NotEmpty().MaximumLength(Entities.Organization.MaxStreetAddressLength);
        RuleFor(x => x.City).NotEmpty().MaximumLength(Entities.Organization.MaxCityLength);
        RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(Entities.Organization.MaxPostalCodeLength);
        RuleFor(x => x.CountryCode).NotEmpty().MaximumLength(Entities.Organization.MaxCountryCodeLength);
        RuleFor(x => x.Url).Must(x => x is null || x.IsAbsoluteUri).WithMessage("The URL must be an absolute URI");
        RuleFor(x => x.InitialAccount).NotNull().SetValidator(new OrganizationInitialAccountInputValidator()!);
    }
}

internal sealed class OrganizationInitialAccountInputValidator : AbstractValidator<OrganizationInitialAccountInput>
{
    public OrganizationInitialAccountInputValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(Entities.User.MaxFirstNameLength);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(Entities.User.MaxLastNameLength);
        RuleFor(x => x.AcceptsTermsAndConditions).Equal(true);
    }
}
