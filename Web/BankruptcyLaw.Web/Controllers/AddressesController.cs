namespace BankruptcyLaw.Web.Controllers
{
    using System.Threading.Tasks;

    using BankruptcyLaw.Services.Data;
    using BankruptcyLaw.Web.ViewModels.Addresses;
    using Microsoft.AspNetCore.Mvc;

    public class AddressesController : BaseController
    {
        private IAddressesService addressService;

        public AddressesController(IAddressesService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("Identity/Account/ChangeAddress")]
        public IActionResult Update()
        {
            var username = this.User.Identity.Name;

            AddressViewModel model = this.addressService.GetAddressInfoByUsername(username);

            return this.View(model);
        }

        [HttpPost("Identity/Account/ChangeAddress")]
        public async Task<IActionResult> Update(AddressViewModel input)
        {
            var username = this.User.Identity.Name;

            await this.addressService.UpdateAddressInfo(username, input);

            return this.Redirect("/Identity/Account/Manage");
        }
    }
}
