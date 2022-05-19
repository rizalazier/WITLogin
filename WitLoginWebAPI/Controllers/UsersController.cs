using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WitLoginWebAPI.Entities;
using WitLoginWebAPI.Helpers;
using WitLoginWebAPI.Models;
using WitLoginWebAPI.Repository;
using WitLoginWebAPI.Services;

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
        private IUsersService _usersService;
        private IUsersCreateNotificationService _usersCreateNotificationService;

        public UsersController(ILogger<UsersController> logger, IJWTManagerRepository jWTManager, IMapper mapper, IUsersService usersService, IUsersCreateNotificationService usersCreateNotificationService)
        {
            _logger = logger;
            _jWTManager = jWTManager;
            _mapper = mapper;
            _usersService = usersService;
            _usersCreateNotificationService = usersCreateNotificationService;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UsersLogin usersdata)
        {

            var user = _usersService.Authenticate(usersdata.Username, usersdata.Password);
            Tokens token = null;
            if (user != null)
            {
                token = _jWTManager.Authenticate(usersdata);
            }

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            //string json = System.IO.File.ReadAllText("JsonFiles/usersDefault.json");
            //var users = JsonConvert.DeserializeObject<List<Users>>(json);
            //return users;

            var users = _usersService.GetAll();
            var model = _mapper.Map<IList<Users>>(users);

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var currUrl = this.Request.GetDisplayUrl();
            _logger.LogInformation("User '" + username + "' has accessed url: " + currUrl);

            return Ok(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UsersRegister model)
        {

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var currUrl = this.Request.GetDisplayUrl();
            _logger.LogInformation("User '"+ username + "' has accessed url: " + currUrl);

            var userNew = _mapper.Map<DbUsers>(model);

            try
            {
                _usersService.Create(userNew, model.Password);

                //List<Users> users = GetUsers();
                //userNew.id = Guid.NewGuid();
                //var userAdd = _mapper.Map<Users>(userNew);
                //users.Add(userAdd);
                //string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                //System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);

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

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var currUrl = this.Request.GetDisplayUrl();
            _logger.LogInformation("User '" + username + "' has accessed url: " + currUrl);

            var userUpdate = _mapper.Map<DbUsers>(model);
            userUpdate.id = GuidConverter.ConvertToGuid(id);

            try
            {
                _usersService.Update(userUpdate, model.Password);

                //List<Users> users = GetUsers();
                //var userExist = users.FirstOrDefault(x => x.id == userUpdate.id);
                //users.Remove(userExist);
                //userExist.id = userUpdate.id;
                //userExist.Name = userUpdate.Name;
                //userExist.Username = userUpdate.Username;
                //userExist.Password = userUpdate.Password;
                //users.Add(userExist);
                //string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                //System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);

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

            string username = User.FindFirst(ClaimTypes.Name)?.Value;
            var currUrl = this.Request.GetDisplayUrl();
            _logger.LogInformation("User '" + username + "' has accessed url: " + currUrl);

            Guid idDelete = GuidConverter.ConvertToGuid(id);
            try
            {
                _usersService.Delete(GuidConverter.ConvertToGuid(id));

                //List<Users> users = GetUsers();
                //var userDelete = users.FirstOrDefault(x => x.id == idDelete);
                //users.Remove(userDelete);
                //string jsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
                //System.IO.File.WriteAllText("JsonFiles/usersDefault.json", jsonData);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("createnotification")]
        public IActionResult CreateNotification([FromBody] UsersCreateNotifications model)
        {

            string username = User.FindFirst(ClaimTypes.Name)?.Value;

            var currUrl = this.Request.GetDisplayUrl();
            _logger.LogInformation("User '" + username + "' has accessed url: " + currUrl);
            //UsersCreateNotifications notification = new UsersCreateNotifications();

            if (string.IsNullOrWhiteSpace(model.Username))
            {
                model.Username = username;
            }

            var userNotificationNew = _mapper.Map<DbUsersCreateNotification>(model);
            try
            {
                _usersCreateNotificationService.Create(userNotificationNew);

                //List<UsersCreateNotifications> notifications = new List<UsersCreateNotifications>();
                ////notification.Id = Guid.NewGuid().ToString();
                //notification.Username = User.FindFirst(ClaimTypes.Name)?.Value;
                //notification.Message = "You have created 1 notification";

                //notifications.Add(notification);

                //string jsonData = JsonConvert.SerializeObject(notifications, Formatting.Indented);
                //System.IO.File.WriteAllText("JsonFiles/usersNotifications.json", jsonData);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
