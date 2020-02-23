using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LowCostHousing.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassowrd { get; set; }

        public int ClientRegistrationId { get; set; }

        public int ProjectMasterId { get; set; }
    }
}
