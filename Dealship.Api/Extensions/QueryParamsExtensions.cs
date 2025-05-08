using Dealship.Shared;

namespace Dealship.Api.Extensions
{
    public static class QueryParamsExtensions
    {
        public static void ResetPage(this QueryParams qp) => qp.Page = 1;

        public static void ToggleSort(this QueryParams qp, string field)
            => qp.Sort = qp.Sort == field ? $"{field}_desc"
                   : qp.Sort == $"{field}_desc" ? field
                   : field;
    }

}
