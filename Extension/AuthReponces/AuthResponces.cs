using System.ComponentModel.DataAnnotations;
using LivMoneyAPI.Dtos;
using LivMoneyAPI.Model.Authentication;

namespace LivMoneyAPI.Extension.AuthReponces {
    public class AuthResponces {
        public int? Status { get; set; }
        public bool Success { get; set; }

        [Display (Name = "Status_Message")]
        public string Status_Message { get; set; }
        public UserDtos data { get; set; }
    }
}