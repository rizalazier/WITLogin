using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitLoginWebAPI.Models;
using WitLoginWebAPI.Repository;

namespace WitLoginWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IMapper _mapper;

        public UsersController(IJWTManagerRepository jWTManager, IMapper mapper)
        {
            _jWTManager = jWTManager;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UsersLogin usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet]
        public List<Users> GetUsers()
        {
            string json = System.IO.File.ReadAllText("JsonFiles/usersDefault.json");
            var users = JsonConvert.DeserializeObject<List<Users>>(json);

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UsersRegister model)
        {
            var userNew = _mapper.Map<Users>(model);

            try
            {
                List<Users> users = GetUsers();
                userNew.id = Guid.NewGuid().ToString();
                users.Add(userNew);
                string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Update(string id, [FromBody] UsersUpdate model)
        {
            var userUpdate = _mapper.Map<Users>(model);
            userUpdate.id = id;

            try
            {
                List<Users> users = GetUsers();
                var userExist = users.FirstOrDefault(x => x.id == userUpdate.id);

                users.Remove(userExist);
                userExist.id = userUpdate.id;
                userExist.Name = userUpdate.Name;
                userExist.Username = userUpdate.Username;
                userExist.Password = userUpdate.Password;
                users.Add(userExist);

                string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                List<Users> users = GetUsers();
                var userDelete = users.FirstOrDefault(x => x.id == id);
                users.Remove(userDelete);
                string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
