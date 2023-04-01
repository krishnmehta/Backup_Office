using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EmpManagement.Pages;

public class Index_Tests : EmpManagementWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
