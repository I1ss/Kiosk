namespace Kiosk.AuthenticationWebApi.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    using Kiosk.Core.Enums;

    /// <summary>
    /// Модель "пользователя" для базы данных.
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("User_Id")]
        public int UserId { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [Column("User_Name")]
        public string Name { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        [Column("User_Password")]
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        [Column("User_Role")]
        public UserRoleEnum Role { get; set; }
    }
}
