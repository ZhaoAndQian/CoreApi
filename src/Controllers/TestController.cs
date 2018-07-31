using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Preoff.Entity;
using Preoff.Repository;

namespace Preoff.Controllers
{
    [Produces("application/json")]
    [Route("Test")]
    public class TestController : Controller
    {
        public readonly IUserRepository _userRepository;
        ILog log = LogManager.GetLogger(Startup.Logrepository.Name, typeof(Startup));
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRepository">用户仓储</param>
        public TestController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("select")]
        public IActionResult Select(int id)
        {
            _userRepository.BatchUpdateUserBirthday();
            _userRepository.Get(p => p.Id == id);
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult All()
        {
            _userRepository.BatchUpdateUserBirthday();
            //_userRepository.Get(p=>p.Id==id);
           return Ok(/*_userRepository.GetAll()*/);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] UserTable _user)
        {
            try
            {
                //_userRepository.Create(_user);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("testAdd", ex);
                return BadRequest();
            }

        }
    }
}