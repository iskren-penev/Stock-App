namespace WIT.Models.ViewModels.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserListViewModel
    {
        public UserListViewModel()
        {
            this.Roles = new List<string>();
            this.Comments = new List<string>();
        }

        public string Id { get; set; }

        public string Email { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        

        public ICollection<string> Roles { get; set; }

        public ICollection<string> Comments { get; set; }
    }
}
