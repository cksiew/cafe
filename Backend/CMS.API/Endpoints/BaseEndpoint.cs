namespace CMS.API.Endpoints
{
    public abstract class BaseEndpoint
    {
        protected static async Task<TResponse> HandleRequest<TResponse>(object query, ISender sender)
        {
            var result = await sender.Send(query!);
            return result.Adapt<TResponse>();
        }
    }
}
