using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;
[ApiController]
[Route("api/search")]
public class SearchController: ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm, 
        int pageNumber = 1, int pageSize = 4)
    {
        var db = await DB.InitAsync("SearchDb");
        var query = db.PagedSearch<Item>();

        query.Sort(x => x.Ascending(x => x.Make));

        if(!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full, searchTerm)
                .SortByTextScore();
        }

        query.PageNumber(pageNumber)
            .PageSize(pageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount,
        });
    }
}
