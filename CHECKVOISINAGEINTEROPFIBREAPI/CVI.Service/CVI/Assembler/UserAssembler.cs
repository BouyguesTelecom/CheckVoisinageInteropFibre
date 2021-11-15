using CVI.Domain.Model;
using CVI.Service.CVI.DTO;
using System;

namespace CVI.Service.CVI.Assembler
{
    /// <summary>
    /// The UserAssembler class
    /// </summary>
    public static class UserAssembler
    {
        /// <summary>
        /// From UserDto to user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User ToUser(this UserDTO user)
        {
            return new User
            {
                CreationDate = DateTime.Now,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Profil = user.Profil,
                Login = user.Login,
                ID = user.UserId
            };
        }

        /// <summary>
        /// From User to UserDto
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserDTO ToUserDto(this User user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Profil = user.Profil,
                Login = user.Login,
                UserId = user.ID
            };
        }
    }
}
