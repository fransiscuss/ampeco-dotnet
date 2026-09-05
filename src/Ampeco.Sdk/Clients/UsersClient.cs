using Ampeco.Sdk.Internal;
using Ampeco.Sdk.Models;

namespace Ampeco.Sdk;

/// <summary>
/// User operations (users v1.1).
/// </summary>
public sealed class UsersClient : ResourceClient
{
    internal UsersClient(ApiConnection connection) : base(connection)
    {
    }

    private const string ResourceBase = "resources/users/v1.1";

    /// <summary>Fetches a user by id.</summary>
    public Task<User> GetAsync(long userId, CancellationToken cancellationToken = default)
        => GetAsync<User>($"{ResourceBase}/{userId}", cancellationToken: cancellationToken);

    /// <summary>Fetches a single page of users.</summary>
    /// <param name="filter">Optional filters (serialized as deepObject query parameters).</param>
    /// <param name="pageRequest">Paging options; defaults to cursor pagination from the first page.</param>
    public Task<Page<User>> GetPageAsync(UserFilter? filter = null, PageRequest? pageRequest = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return GetPageAsync<User>(ResourceBase, query, pageRequest, cancellationToken);
    }

    /// <summary>Iterates over all users matching the filter, transparently following cursor pagination.</summary>
    public IAsyncEnumerable<User> StreamAsync(UserFilter? filter = null, int? perPage = null, CancellationToken cancellationToken = default)
    {
        var query = new QueryBuilder();
        query.AddFilter(filter);
        return StreamAsync<User>(ResourceBase, query, new PageRequest { PerPage = perPage }, cancellationToken);
    }

    /// <summary>Creates a user. <see cref="UserWrite.Email"/> and <see cref="UserWrite.Password"/> are required.</summary>
    public Task<User> CreateAsync(UserWrite user, CancellationToken cancellationToken = default)
        => PostAsync<User>(ResourceBase, user, cancellationToken);

    /// <summary>Updates a user. Only set properties are sent.</summary>
    public Task<User> UpdateAsync(long userId, UserWrite user, CancellationToken cancellationToken = default)
        => Connection.PatchAsync<User>($"{ResourceBase}/{userId}", user, cancellationToken: cancellationToken);

    /// <summary>Deletes a user.</summary>
    public Task DeleteAsync(long userId, CancellationToken cancellationToken = default)
        => base.DeleteAsync($"{ResourceBase}/{userId}", cancellationToken);
}
