namespace Stock.Models.EntityModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Warehouse> warehouses;
        private ICollection<Record> records;

        public User()
        {
            this.warehouses = new HashSet<Warehouse>();
            this.records = new HashSet<Record>();
        }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string CompanyName { get; set; }

        public virtual ICollection<Warehouse> Warehouses
        {
            get { return this.warehouses; }
            set { this.warehouses = value; }
        }

        public virtual ICollection<Record> Records
        {
            get { return this.records; }
            set { this.records = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
