﻿namespace Shop.Infrastructure.GraphQL;

public sealed record MutationResult
{
    public required Guid? SubjectId { get; init; }

    public static MutationResult WithoutSubject()
        => new MutationResult { SubjectId = null };

    public static MutationResult ForSubject(Guid subjectId)
        => new MutationResult { SubjectId = subjectId };
}
